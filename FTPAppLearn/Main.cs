using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using FTPAppLearn;
using Timer = System.Windows.Forms.Timer;

public partial class Main : Form
{
	private Listener listener;
	private TransferClient client;
	private string outputFolder;
	private Timer overallProgressTimer;
	private bool bRunningServer;
    public Main()
    {
        InitializeComponent();
        
        listener = new Listener();
        listener.Accepted += listener_Accepted;

        overallProgressTimer = new Timer();
        overallProgressTimer.Interval = 1000;
        overallProgressTimer.Tick += overallProgTimer_Tick;
        
        outputFolder = "Transfers";
        if(!Directory.Exists(outputFolder)) Directory.CreateDirectory(outputFolder);
        
        btnConnect.Click += new EventHandler(btnConnect_Click);
        btnStartServer.Click += new EventHandler(btnStartServer_Click);
        btnStopServer.Click += new EventHandler(btnStopServer_Click);
        btnSendFile.Click += new EventHandler(btnSendFile_Click);
        btnPauseTransfer.Click += new EventHandler(btnPauseTransfer_Click);
        btnStopTransfer.Click += new EventHandler(btnStopTransfer_Click);
        btnOpenDir.Click += new EventHandler(btnOpenDir_Click);
        btnClearComplete.Click += new EventHandler(btnClearComplete_Click);

        btnStopServer.Enabled = false;
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
	    DeregisterEvent();
	    base.OnFormClosing(e);
    }
    
    private void overallProgTimer_Tick(object? sender, EventArgs e)
    {
	    if (client == null) return;
	    progressOverall.Value = client.GetOverallProgress();
    }

    private void listener_Accepted(object sender, SocketAcceptedEventArgs e)
    {
	    if (InvokeRequired)
	    {
		    Invoke(new SocketAcceptedHandler(listener_Accepted), sender, e);
		    return;
	    }
	    listener.Stop();
	    client = new TransferClient(e.Accepted)
	    {
		    OutputFolder = outputFolder
	    };//Accept socket
	    RegisterEvent();
	    client.Run();
	    overallProgressTimer.Start();
	    SetConnectionStatus(client.EndPoint.Address.ToString());
    }

    private void RegisterEvent()
    {
	    client.Complete += client_Complelete;
	    client.Disconnected += client_Disconnected;
	    client.ProgressChanged += client_ProgressChanged;
	    client.Queued += client_Queued;
	    client.Stopped += client_Stopped;
    }

    private void DeregisterEvent()
    {
	    if(client == null) return;
	    client.Complete -= client_Complelete;
	    client.Disconnected -= client_Disconnected;
	    client.ProgressChanged -= client_ProgressChanged;
	    client.Queued -= client_Queued;
	    client.Stopped -= client_Stopped;
    }

    private void client_Queued(object sender, TransferQueue queue)
    {
	    if (InvokeRequired)
	    {
		    Invoke(new TransferEventHandler(client_Queued), sender, queue);
	    }
	    ListViewItem items = new ListViewItem();
	    items.Text = queue.ID.ToString();
	    items.SubItems.Add(queue.FileName);
	    items.SubItems.Add(queue.Type == QueueType.Download ? "Download" : "Upload");
	    items.SubItems.Add("0%");
	    items.Tag = queue;
	    items.Name = queue.ID.ToString();//=> can use lstTransfers.Items[id]
	    lstTransfers.Items.Add(items);
	    items.EnsureVisible();
	    if (queue.Type == QueueType.Download)
	    {
		    client.StartTransfer(queue);
	    }
    }

    private void client_ProgressChanged(object sender, TransferQueue queue)
    {
	    if (InvokeRequired) 
	    {
		    Invoke(new TransferEventHandler(client_ProgressChanged), sender, queue);
		    return;
	    }
	    lstTransfers.Items[queue.ID.ToString()].SubItems[3].Text = queue.Progress + "%";
    }

    private void client_Disconnected(object? sender, EventArgs e)
    {
	    if (InvokeRequired)
	    {
		    Invoke(new EventHandler(client_Disconnected), sender, e);
		    return;
	    }
	    DeregisterEvent();
	    foreach (ListViewItem item in lstTransfers.Items)
	    {
		    var queue = (TransferQueue) item.Tag;
		    queue.Close();
	    }
	    lstTransfers.Clear();
	    progressOverall.Value = 0;
	    client = null;
	    SetConnectionStatus("No Connection");

	    if (bRunningServer)
	    {
		    listener.Start(int.Parse(txtCntPort.Text.Trim()));
		    SetConnectionStatus("Waiting...");
	    }
	    else
	    {
		    btnConnect.Text = @"Connect";
	    }
    }

    private void client_Complelete(object sender, TransferQueue queue)
    {
		   System.Media.SystemSounds.Exclamation.Play();
    }
    private void client_Stopped(object sender, TransferQueue queue)
    {
	    if (InvokeRequired)
	    {
		    Invoke(new TransferEventHandler(client_Stopped), sender, queue);
		    return;
	    }
		lstTransfers.Items[queue.ID.ToString()].Remove();
    }
    void SetConnectionStatus(string inStatus)
    {
	    lblConnected.Text = @"Connected: " + inStatus;
    }
    
    private void connectCallback(object sender, string error)
    {
	    if (InvokeRequired)
	    {
		    Invoke(new ConnectCallback(connectCallback), sender, error); //Lấy hàm này cùng các tham số kia, đẩy vào hàng đợi của UI Thread để nó thực thi
	    }
        Enabled = true;
	    if (error != null)
	    {
		    MessageBox.Show(error, "Connect callback Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		    client.Close();
		    client = null;
		    return;
	    }
	    RegisterEvent();
	    client.OutputFolder = outputFolder;
	    client.Run();
	    SetConnectionStatus(client.EndPoint.Address.ToString());
	    overallProgressTimer.Start();
	    btnConnect.Text = "Disconnect";
    }
    private void btnConnect_Click(object sender, EventArgs e)
    {
	    if (client == null)
	    {
		    client = new TransferClient();
		    client.Connect(txtCntHost.Text.Trim(), int.Parse(txtCntPort.Text.Trim()), connectCallback);
		    Enabled = false;
	    }
	    else
	    {
		    client = null;
	    }
    }
    private void btnStartServer_Click(object sender, EventArgs e)
    {
		if (bRunningServer) return;
		bRunningServer = true;
		try
		{
			listener.Start(int.Parse(txtCntPort.Text.Trim()));
			SetConnectionStatus("Waiting...");
			btnStartServer.Enabled = false;
			btnStopServer.Enabled = true;
		}
		catch (Exception exception)
		{
			MessageBox.Show(exception.Message + txtCntPort.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			Console.WriteLine(exception);
			throw;
		}
    }

    private void btnStopServer_Click(object sender, EventArgs e)
    {
		if(!bRunningServer) return;
		bRunningServer = false;
		client?.Close();
		listener.Stop();
		overallProgressTimer.Stop();
		SetConnectionStatus("No Connect");
		btnStartServer.Enabled = true;
		btnStopServer.Enabled = false;
    }

    private void btnClearComplete_Click(object sender, EventArgs e)
    {
	    foreach (ListViewItem item in lstTransfers.Items)
	    {
		    var queue = (TransferQueue) item.Tag;
		    if(queue.Progress == 100 || !queue.bRunning) item.Remove();
	    }
    }

    private void btnOpenDir_Click(object sender, EventArgs e)
    {
	    using (FolderBrowserDialog fbd = new FolderBrowserDialog())
	    {
		    outputFolder = fbd.SelectedPath;
		    if (client != null) client.OutputFolder = outputFolder;
		    txtSaveDir.Text = outputFolder;
	    }
    }

    private void btnSendFile_Click(object sender, EventArgs e)
    {
		if(client == null) return;
		using (OpenFileDialog ofd = new OpenFileDialog())
		{
			ofd.Filter = "All Files (*.*)|*.*";
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				foreach (var fileName in ofd.FileNames)
				{
					client.QueueTransfer(fileName);
				}
			}
		}
    }

    private void btnPauseTransfer_Click(object sender, EventArgs e)
    {
		if(client == null) return;
		foreach (ListViewItem selectedItem in lstTransfers.SelectedItems)
		{
			var queue = (TransferQueue) selectedItem.Tag;
			queue.Client.PauseTransfer(queue);
		}
    }

    private void btnStopTransfer_Click(object sender, EventArgs e)
    {
	    if(client == null) return;
	    foreach (ListViewItem selectedItem in lstTransfers.SelectedItems)
	    {
		    var queue = (TransferQueue) selectedItem.Tag;
		    queue.Client.StopTransfer(queue);
		    selectedItem.Remove();
	    }
	    progressOverall.Value = 0;
    }
}