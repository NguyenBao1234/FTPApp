using FTPAppLearn.Data.Enum;

namespace FTPAppLearn;

public enum QueueType
{
    Download,
    Upload
}


public class TransferQueue
{
    public static TransferQueue CreateUploadQueue(TransferClient inClient, string inFileName)
    {
        try
        {
            var queue = new TransferQueue
            {
                Client = inClient,
                FileName = inFileName,
                Type = QueueType.Upload,
                FS = new FileStream(inFileName, FileMode.Open), //, FileAccess.Read co can cho vao k
                Thread = new Thread(new ParameterizedThreadStart(TransferProcess))
                {
                    IsBackground = true //avoid close program but still transfer
                }, //Function has a object parameter.
                ID = Program.randomInst.Next()
            };
            queue.Length = queue.FS.Length;
            return queue;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }

    public static TransferQueue CreateDownloadQueue(TransferClient inClient, int inId, string inFileName,
        long inLength)
    {
        try
        {
            var queue = new TransferQueue()
            {
                Client = inClient,
                ID = inId,
                FileName = inFileName,
                Type = QueueType.Download,
                FS = new FileStream(inFileName, FileMode.Create),
                Length = inLength
            };
            queue.FS.SetLength(inLength);
            return queue;
        }
        catch (Exception e)
        {
            MessageBox.Show(e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return null;
        }
    }
    
    private const int FILE_BUFFER_SIZE = 8175;
    private static byte[] FileBuffer = new byte[FILE_BUFFER_SIZE];
    private static ManualResetEvent PauseEvent;
    public int ID, Progress, LastProgress;
    public long Transfered, Index, Length;
    public bool bPaused, bRunning;
    public string FileName;
    public QueueType Type;

    public TransferClient? Client;
    public Thread Thread;
    public FileStream FS;

    private TransferQueue()
    {
        PauseEvent = new ManualResetEvent(false);
        bRunning = true;
    }

    public void Start()
    {
        bRunning =  true;
        Thread.Start(this);
    }

    public void Stop()
    {
        bRunning = false;
    }

    public void TogglePause()
    {
        if (bPaused) PauseEvent.Set();
        else PauseEvent.Reset();
        bPaused = !bPaused;
    }

    public void Close()
    {
        if(Client == null)  return;
        Client.Transfers.Remove(ID);
        Client = null;
        bRunning = false;
        PauseEvent.Dispose();
        FS.Close();
    }

    public void Write(byte[] buffer, long index)
    {
        lock (this) // Lock similar Checkout when using svn tortoise teamwork
        {
            FS.Position = index;
            FS.Write(buffer, 0, buffer.Length); //many buffers make one file
            Transfered += buffer.Length;
            
        }
    }

    private static void TransferProcess(object? o)
    {
        TransferQueue queue = (TransferQueue)o;
        while (queue.bRunning && queue.Index < queue.Length)
        {
            PauseEvent.WaitOne();
            if(!queue.bRunning) break;
            lock (FileBuffer)
            {
                queue.FS.Position = queue.Index;
                int read = queue.FS.Read(FileBuffer, 0, FileBuffer.Length);
                PacketWriter pw = new PacketWriter();
                pw.Write((byte)Headers.Chunk);
                pw.Write(queue.ID);// 4 bytes
                pw.Write(queue.Index); // 8 bytes
                pw.Write(read);// 
                pw.Write(FileBuffer, 0, read);
                
                queue.Transfered += read;
                queue.Index += read;
                queue.Client.Send(pw.GetBytes());
                queue.Progress = (int)((queue.Transfered / queue.Length)* 100);
                if (queue.LastProgress < queue.Progress)
                {
                    queue.LastProgress = queue.Progress;
                    queue.Client.CallProgressChanged(queue);
                }
                Thread.Sleep(10);
            }
        }
        queue.Close();
    }
}