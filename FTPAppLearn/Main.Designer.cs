partial class Main
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
        toolStrip1 = new System.Windows.Forms.ToolStrip();
        toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
        portToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
        txtServerPort = new System.Windows.Forms.ToolStripTextBox();
        toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
        btnStartServer = new System.Windows.Forms.ToolStripMenuItem();
        btnStopServer = new System.Windows.Forms.ToolStripMenuItem();
        toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
        toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
        txtCntHost = new System.Windows.Forms.ToolStripTextBox();
        txtCntPort = new System.Windows.Forms.ToolStripTextBox();
        btnConnect = new System.Windows.Forms.ToolStripButton();
        toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
        toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
        txtSaveDir = new System.Windows.Forms.ToolStripTextBox();
        btnOpenDir = new System.Windows.Forms.ToolStripMenuItem();
        lstTransfers = new System.Windows.Forms.ListView();
        columnHeader1 = new System.Windows.Forms.ColumnHeader();
        columnHeader5 = new System.Windows.Forms.ColumnHeader();
        columnHeader6 = new System.Windows.Forms.ColumnHeader();
        columnHeader7 = new System.Windows.Forms.ColumnHeader();
        menuTransfers = new System.Windows.Forms.ContextMenuStrip(components);
        btnSendFile = new System.Windows.Forms.ToolStripMenuItem();
        btnStopTransfer = new System.Windows.Forms.ToolStripMenuItem();
        btnPauseTransfer = new System.Windows.Forms.ToolStripMenuItem();
        toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
        btnClearComplete = new System.Windows.Forms.ToolStripMenuItem();
        statusStrip1 = new System.Windows.Forms.StatusStrip();
        lblConnected = new System.Windows.Forms.ToolStripStatusLabel();
        toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
        toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
        progressOverall = new System.Windows.Forms.ToolStripProgressBar();
        toolStrip1.SuspendLayout();
        menuTransfers.SuspendLayout();
        statusStrip1.SuspendLayout();
        SuspendLayout();
        // 
        // toolStrip1
        // 
        toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripSplitButton1, toolStripSeparator1, toolStripLabel1, txtCntHost, txtCntPort, btnConnect, toolStripSeparator2, toolStripSplitButton2 });
        toolStrip1.Location = new System.Drawing.Point(0, 0);
        toolStrip1.Name = "toolStrip1";
        toolStrip1.Size = new System.Drawing.Size(532, 25);
        toolStrip1.TabIndex = 1;
        toolStrip1.Text = "toolStrip1";
        // 
        // toolStripSplitButton1
        // 
        toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { portToolStripMenuItem, toolStripMenuItem2, btnStartServer, btnStopServer });
        toolStripSplitButton1.Image = ((System.Drawing.Image)resources.GetObject("toolStripSplitButton1.Image"));
        toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripSplitButton1.Name = "toolStripSplitButton1";
        toolStripSplitButton1.Size = new System.Drawing.Size(55, 22);
        toolStripSplitButton1.Text = "Server";
        // 
        // portToolStripMenuItem
        // 
        portToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { txtServerPort });
        portToolStripMenuItem.Name = "portToolStripMenuItem";
        portToolStripMenuItem.Size = new System.Drawing.Size(98, 22);
        portToolStripMenuItem.Text = "Port";
        // 
        // txtServerPort
        // 
        txtServerPort.Font = new System.Drawing.Font("Segoe UI", 9F);
        txtServerPort.Name = "txtServerPort";
        txtServerPort.Size = new System.Drawing.Size(100, 23);
        txtServerPort.Text = "100";
        // 
        // toolStripMenuItem2
        // 
        toolStripMenuItem2.Name = "toolStripMenuItem2";
        toolStripMenuItem2.Size = new System.Drawing.Size(95, 6);
        // 
        // btnStartServer
        // 
        btnStartServer.Name = "btnStartServer";
        btnStartServer.Size = new System.Drawing.Size(98, 22);
        btnStartServer.Text = "Start";
        // 
        // btnStopServer
        // 
        btnStopServer.Name = "btnStopServer";
        btnStopServer.Size = new System.Drawing.Size(98, 22);
        btnStopServer.Text = "Stop";
        // 
        // toolStripSeparator1
        // 
        toolStripSeparator1.Name = "toolStripSeparator1";
        toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
        // 
        // toolStripLabel1
        // 
        toolStripLabel1.Name = "toolStripLabel1";
        toolStripLabel1.Size = new System.Drawing.Size(35, 22);
        toolStripLabel1.Text = "Host:";
        // 
        // txtCntHost
        // 
        txtCntHost.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtCntHost.Font = new System.Drawing.Font("Segoe UI", 9F);
        txtCntHost.Name = "txtCntHost";
        txtCntHost.Size = new System.Drawing.Size(116, 25);
        txtCntHost.Text = "localhost";
        // 
        // txtCntPort
        // 
        txtCntPort.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
        txtCntPort.Font = new System.Drawing.Font("Segoe UI", 9F);
        txtCntPort.Name = "txtCntPort";
        txtCntPort.Size = new System.Drawing.Size(35, 25);
        txtCntPort.Text = "100";
        txtCntPort.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        // 
        // btnConnect
        // 
        btnConnect.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        btnConnect.Image = ((System.Drawing.Image)resources.GetObject("btnConnect.Image"));
        btnConnect.ImageTransparentColor = System.Drawing.Color.Magenta;
        btnConnect.Name = "btnConnect";
        btnConnect.Size = new System.Drawing.Size(56, 22);
        btnConnect.Text = "Connect";
        // 
        // toolStripSeparator2
        // 
        toolStripSeparator2.Name = "toolStripSeparator2";
        toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
        // 
        // toolStripSplitButton2
        // 
        toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
        toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { txtSaveDir, btnOpenDir });
        toolStripSplitButton2.Image = ((System.Drawing.Image)resources.GetObject("toolStripSplitButton2.Image"));
        toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
        toolStripSplitButton2.Name = "toolStripSplitButton2";
        toolStripSplitButton2.Size = new System.Drawing.Size(98, 22);
        toolStripSplitButton2.Text = "Save Directory";
        // 
        // txtSaveDir
        // 
        txtSaveDir.Font = new System.Drawing.Font("Segoe UI", 9F);
        txtSaveDir.Name = "txtSaveDir";
        txtSaveDir.Size = new System.Drawing.Size(300, 23);
        txtSaveDir.Text = "...\\Transfers";
        // 
        // btnOpenDir
        // 
        btnOpenDir.Name = "btnOpenDir";
        btnOpenDir.Size = new System.Drawing.Size(360, 22);
        btnOpenDir.Text = "Choose...";
        // 
        // lstTransfers
        // 
        lstTransfers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnHeader1, columnHeader5, columnHeader6, columnHeader7 });
        lstTransfers.ContextMenuStrip = menuTransfers;
        lstTransfers.FullRowSelect = true;
        lstTransfers.Location = new System.Drawing.Point(14, 32);
        lstTransfers.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        lstTransfers.Name = "lstTransfers";
        lstTransfers.Size = new System.Drawing.Size(503, 152);
        lstTransfers.TabIndex = 2;
        lstTransfers.UseCompatibleStateImageBehavior = false;
        lstTransfers.View = System.Windows.Forms.View.Details;
        // 
        // columnHeader1
        // 
        columnHeader1.Name = "columnHeader1";
        columnHeader1.Text = "ID";
        columnHeader1.Width = 79;
        // 
        // columnHeader5
        // 
        columnHeader5.Name = "columnHeader5";
        columnHeader5.Text = "Filename";
        columnHeader5.Width = 171;
        // 
        // columnHeader6
        // 
        columnHeader6.Name = "columnHeader6";
        columnHeader6.Text = "Type";
        columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
        columnHeader6.Width = 72;
        // 
        // columnHeader7
        // 
        columnHeader7.Name = "columnHeader7";
        columnHeader7.Text = "Progress";
        columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
        columnHeader7.Width = 68;
        // 
        // menuTransfers
        // 
        menuTransfers.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { btnSendFile, btnStopTransfer, btnPauseTransfer, toolStripMenuItem1, btnClearComplete });
        menuTransfers.Name = "contextMenuStrip1";
        menuTransfers.Size = new System.Drawing.Size(157, 98);
        // 
        // btnSendFile
        // 
        btnSendFile.Name = "btnSendFile";
        btnSendFile.Size = new System.Drawing.Size(156, 22);
        btnSendFile.Text = "Send";
        // 
        // btnStopTransfer
        // 
        btnStopTransfer.Name = "btnStopTransfer";
        btnStopTransfer.Size = new System.Drawing.Size(156, 22);
        btnStopTransfer.Text = "Stop";
        // 
        // btnPauseTransfer
        // 
        btnPauseTransfer.Name = "btnPauseTransfer";
        btnPauseTransfer.Size = new System.Drawing.Size(156, 22);
        btnPauseTransfer.Text = "Pause";
        // 
        // toolStripMenuItem1
        // 
        toolStripMenuItem1.Name = "toolStripMenuItem1";
        toolStripMenuItem1.Size = new System.Drawing.Size(153, 6);
        // 
        // btnClearComplete
        // 
        btnClearComplete.Name = "btnClearComplete";
        btnClearComplete.Size = new System.Drawing.Size(156, 22);
        btnClearComplete.Text = "Clear Complete";
        // 
        // statusStrip1
        // 
        statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { lblConnected, toolStripStatusLabel2, toolStripStatusLabel3, progressOverall });
        statusStrip1.Location = new System.Drawing.Point(0, 192);
        statusStrip1.Name = "statusStrip1";
        statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
        statusStrip1.Size = new System.Drawing.Size(532, 24);
        statusStrip1.TabIndex = 3;
        statusStrip1.Text = "statusStrip1";
        // 
        // lblConnected
        // 
        lblConnected.Name = "lblConnected";
        lblConnected.Size = new System.Drawing.Size(294, 19);
        lblConnected.Spring = true;
        lblConnected.Text = "Connection: -";
        // 
        // toolStripStatusLabel2
        // 
        toolStripStatusLabel2.Name = "toolStripStatusLabel2";
        toolStripStatusLabel2.Size = new System.Drawing.Size(10, 19);
        toolStripStatusLabel2.Text = "|";
        // 
        // toolStripStatusLabel3
        // 
        toolStripStatusLabel3.Name = "toolStripStatusLabel3";
        toolStripStatusLabel3.Size = new System.Drawing.Size(92, 19);
        toolStripStatusLabel3.Text = "Overall Progress";
        // 
        // progressOverall
        // 
        progressOverall.Name = "progressOverall";
        progressOverall.Size = new System.Drawing.Size(117, 18);
        progressOverall.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
        // 
        // Main
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(532, 216);
        Controls.Add(statusStrip1);
        Controls.Add(lstTransfers);
        Controls.Add(toolStrip1);
        FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        MaximizeBox = false;
        Text = "File Transfer";
        toolStrip1.ResumeLayout(false);
        toolStrip1.PerformLayout();
        menuTransfers.ResumeLayout(false);
        statusStrip1.ResumeLayout(false);
        statusStrip1.PerformLayout();
        ResumeLayout(false);
        PerformLayout();
    }

    #endregion

    private System.Windows.Forms.ToolStrip toolStrip1;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
    private System.Windows.Forms.ToolStripMenuItem btnStartServer;
    private System.Windows.Forms.ToolStripMenuItem btnStopServer;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    private System.Windows.Forms.ToolStripLabel toolStripLabel1;
    private System.Windows.Forms.ToolStripTextBox txtCntHost;
    private System.Windows.Forms.ToolStripTextBox txtCntPort;
    private System.Windows.Forms.ToolStripButton btnConnect;
    private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
    private System.Windows.Forms.ListView lstTransfers;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ContextMenuStrip menuTransfers;
    private System.Windows.Forms.ToolStripMenuItem btnSendFile;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem btnClearComplete;
    private System.Windows.Forms.StatusStrip statusStrip1;
    private System.Windows.Forms.ToolStripStatusLabel lblConnected;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
    private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
    private System.Windows.Forms.ToolStripProgressBar progressOverall;
    private System.Windows.Forms.ToolStripMenuItem btnStopTransfer;
    private System.Windows.Forms.ToolStripMenuItem btnPauseTransfer;
    private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
    private System.Windows.Forms.ToolStripTextBox txtSaveDir;
    private System.Windows.Forms.ToolStripMenuItem btnOpenDir;
    private System.Windows.Forms.ToolStripMenuItem portToolStripMenuItem;
    private System.Windows.Forms.ToolStripTextBox txtServerPort;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ColumnHeader columnHeader5;
    private System.Windows.Forms.ColumnHeader columnHeader6;
    private System.Windows.Forms.ColumnHeader columnHeader7;
}

