using System.Net;
using System.Net.Sockets;
using FTPAppLearn.Data.Enum;

namespace FTPAppLearn;

public delegate void TransferEventHandler (object sender, TransferQueue queue);
public delegate void ConnectCallback (object sender, string error);

public class TransferClient
{
    private Socket _baseSocket;
    private byte[] _buffer = new byte[8192]; //4MB
    private ConnectCallback _connectCallback;
    private Dictionary<int, TransferQueue> _transfers = new Dictionary<int, TransferQueue>();//Hold transfer
    public Dictionary<int, TransferQueue> Transfers { get { return _transfers; } }
    public bool bClose {get; private set;}
    public string OutputFolder {get; set;}
    public IPEndPoint EndPoint {get; private set;}
    
    public event TransferEventHandler Queued;
    public event TransferEventHandler ProgressChanged;
    public event TransferEventHandler Stopped;
    public event TransferEventHandler Complete;
    public event EventHandler Disconnected;

    public TransferClient()
    {
        _baseSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public TransferClient(Socket inSocket)
    {
        _baseSocket = inSocket;
        EndPoint = (IPEndPoint)_baseSocket.RemoteEndPoint; //Cái máy đang đứng ở đầu dây bên kia có địa chỉ IP va port là gì
    }
    
    public void Connect(string Hostname, int Port, ConnectCallback Callback)
    {
        _connectCallback = Callback;
        _baseSocket.BeginConnect(Hostname, Port, connectCallback, _baseSocket);
    }

    private void connectCallback(IAsyncResult ar)
    {
        string error = null;
        try
        {
            _baseSocket.EndConnect(ar);
            EndPoint = (IPEndPoint)_baseSocket.RemoteEndPoint;
        }
        catch(Exception e)
        {
            error = e.Message;
        }
        _connectCallback(this, error);
    }
    
    /// <summary>
    /// Gửi mảng dữ liệu byte thông qua kết nối Socket hiện tại.
    /// </summary>
    public void Send(byte[] data)
    {
        if(bClose)  return;
        lock (this)
        {
            try
            {
                _baseSocket.Send(BitConverter.GetBytes(data.Length), 0,4, SocketFlags.None);//Send size first
                _baseSocket.Send(data, 0, data.Length, SocketFlags.None);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Close();
            }
        }
    }

    public void Close()
    {
        bClose = true;
        _baseSocket.Close();
        _transfers.Clear();
        _transfers = null;
        _buffer = null;
        OutputFolder = null;
        if(Disconnected != null) Disconnected(this, EventArgs.Empty);
    }

    public void UploadTransferQueued(string inFileName)
    {
        try
        {
            var queue = TransferQueue.CreateUploadQueue(this,  inFileName);
            _transfers.Add(queue.GetHashCode(), queue);
            var pw = new PacketWriter();
            pw.Write((int)Headers.Queue);
            pw.Write(queue.ID);
            pw.Write(queue.FileName);
            pw.Write(queue.Length);
            Send(pw.GetBytes());
        
            if(Queued != null) Queued(this, queue);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void StartTransfer(TransferQueue queue)
    {
        var pw = new PacketWriter();
        pw.Write((int)Headers.Start);
        pw.Write(queue.ID);
        Send(pw.GetBytes());
    }

    public void StopTransfer(TransferQueue queue)
    {
        if (queue.Type == QueueType.Upload)
        {
            queue.Stop();
            return;
        }

        var pw = new PacketWriter();
        pw.Write((int)Headers.Stop);
        pw.Write(queue.ID);
        Send(pw.GetBytes());
        queue.Close();
    }

    public void PauseTransfer(TransferQueue queue)
    {
        if (queue.Type == QueueType.Upload)
        {
            queue.TogglePause();
            return;
        }

        var pw = new PacketWriter();
        pw.Write((int)Headers.Pause);
        pw.Write(queue.ID);
        Send(pw.GetBytes());
    }

    private void Process()
    {
        var pr =  new PacketReader(_buffer);
        var header = (Headers)pr.ReadByte();
        var id = pr.ReadInt32();
        switch (header)
        {
            case Headers.Queue:
                var fileName = pr.ReadString();
                var length = pr.ReadInt64();//"int64" type is "long" type
                var downloadQueue = TransferQueue.CreateDownloadQueue(this,id, fileName, length);
                _transfers.Add(id, downloadQueue);
                Queued?.Invoke(this, downloadQueue);
                break;
            case Headers.Start:
                if(_transfers.ContainsKey(id)) _transfers[id].Start();
                break;
            case Headers.Stop:
                if(!_transfers.ContainsKey(id)) break;
                var stopQueue = _transfers[id];
                stopQueue.Stop();
                stopQueue.Close();
                _transfers.Remove(id);
                break;
            case Headers.Pause:
                if(_transfers.ContainsKey(id)) _transfers[id].TogglePause();
                break;
            case Headers.Chunk:
                var index = pr.ReadInt64();
                int size = pr.ReadInt32();
                byte[] buffer = pr.ReadBytes(size);
                var chunkQueue = _transfers[id];
                chunkQueue.Write(buffer,index);
                chunkQueue.Progress = (int) (Transfers.Count / chunkQueue.Length) * 100;
                if (chunkQueue.LastProgress < chunkQueue.Progress)
                {
                    chunkQueue.LastProgress = chunkQueue.Progress;
                    ProgressChanged?.Invoke(this, chunkQueue);
                    if (chunkQueue.Progress == 100)
                    {
                        chunkQueue.Close();
                        Complete?.Invoke(this, chunkQueue);
                    }
                }
                break;
        }
        pr.Dispose();
    }
    
    public int GetOverallProgress()
    {
        int overallProgress = 0;
        foreach (var queueEle in _transfers.Values)
        {
            overallProgress += queueEle.Progress;
        }

        if (overallProgress > 0)
        {
            overallProgress = (overallProgress / _transfers.Count * 100) * 100;
        }
        return overallProgress;
    }
    
    public void Run()
    {
        try
        {
            _baseSocket.BeginReceive(_buffer,0, _buffer.Length, SocketFlags.Peek,ReceiveCallback,null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Close();
        }
    }

    private void ReceiveCallback(IAsyncResult ar)
    {
        int receiveByteAmount = _baseSocket.EndReceive(ar);
        if (receiveByteAmount > 4)
        {
            _baseSocket.Receive(_buffer, 0, 4, SocketFlags.None);
            int size = BitConverter.ToInt32(_buffer, 0);
            int read = _baseSocket.Receive(_buffer, 0, size, SocketFlags.None);
            while (read < size)
            {
                read += _baseSocket.Receive(_buffer, read, size - read, SocketFlags.None);
            }

            //process();
        }
    }

    internal void CallProgressChanged(TransferQueue queue)
    {
        if (ProgressChanged != null) ProgressChanged(this, queue);
    }
}