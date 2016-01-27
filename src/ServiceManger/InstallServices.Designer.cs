namespace ServiceManger
{
    partial class InstallServices
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstallServices));
            this.btnInstall = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.group = new System.Windows.Forms.GroupBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.btnUnInstall = new System.Windows.Forms.Button();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.txtPath = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.chkStatus = new System.Windows.Forms.Button();
            this.cmbServiceList = new System.Windows.Forms.ComboBox();
            this.btnOpendlg = new System.Windows.Forms.Button();
            this.group.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnInstall
            // 
            this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnInstall.Location = new System.Drawing.Point(42, 34);
            this.btnInstall.Name = "btnInstall";
            this.btnInstall.Size = new System.Drawing.Size(100, 63);
            this.btnInstall.TabIndex = 0;
            this.btnInstall.Text = "安装";
            this.btnInstall.UseVisualStyleBackColor = true;
            this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(-2, 350);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "label1";
            // 
            // group
            // 
            this.group.Controls.Add(this.btnStop);
            this.group.Controls.Add(this.btnStart);
            this.group.Controls.Add(this.btnUnInstall);
            this.group.Controls.Add(this.btnInstall);
            this.group.Location = new System.Drawing.Point(0, 133);
            this.group.Name = "group";
            this.group.Size = new System.Drawing.Size(404, 203);
            this.group.TabIndex = 2;
            this.group.TabStop = false;
            this.group.Text = "服务管理";
            // 
            // btnStop
            // 
            this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStop.Location = new System.Drawing.Point(209, 101);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(100, 63);
            this.btnStop.TabIndex = 3;
            this.btnStop.Text = "停止";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(42, 101);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(100, 63);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "启动";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // btnUnInstall
            // 
            this.btnUnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnUnInstall.Location = new System.Drawing.Point(209, 34);
            this.btnUnInstall.Name = "btnUnInstall";
            this.btnUnInstall.Size = new System.Drawing.Size(100, 63);
            this.btnUnInstall.TabIndex = 1;
            this.btnUnInstall.Text = "卸载";
            this.btnUnInstall.UseVisualStyleBackColor = true;
            this.btnUnInstall.Click += new System.EventHandler(this.btnUnInstall_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.BalloonTipText = "松下薪资服务管理程序";
            this.notifyIcon1.BalloonTipTitle = "薪资管理程序";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "SalaryQuery";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(12, 12);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(317, 21);
            this.txtPath.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnRefresh);
            this.groupBox1.Controls.Add(this.chkStatus);
            this.groupBox1.Controls.Add(this.cmbServiceList);
            this.groupBox1.Controls.Add(this.btnOpendlg);
            this.groupBox1.Controls.Add(this.txtPath);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(404, 127);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(310, 74);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(57, 23);
            this.btnRefresh.TabIndex = 7;
            this.btnRefresh.Text = "复位";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // chkStatus
            // 
            this.chkStatus.Location = new System.Drawing.Point(229, 74);
            this.chkStatus.Name = "chkStatus";
            this.chkStatus.Size = new System.Drawing.Size(53, 23);
            this.chkStatus.TabIndex = 6;
            this.chkStatus.Text = "检查";
            this.chkStatus.UseVisualStyleBackColor = true;
            this.chkStatus.Click += new System.EventHandler(this.chkStatus_Click);
            // 
            // cmbServiceList
            // 
            this.cmbServiceList.FormattingEnabled = true;
            this.cmbServiceList.Location = new System.Drawing.Point(12, 77);
            this.cmbServiceList.Name = "cmbServiceList";
            this.cmbServiceList.Size = new System.Drawing.Size(182, 20);
            this.cmbServiceList.TabIndex = 5;
            // 
            // btnOpendlg
            // 
            this.btnOpendlg.Location = new System.Drawing.Point(340, 10);
            this.btnOpendlg.Name = "btnOpendlg";
            this.btnOpendlg.Size = new System.Drawing.Size(27, 23);
            this.btnOpendlg.TabIndex = 4;
            this.btnOpendlg.Text = "...";
            this.btnOpendlg.UseVisualStyleBackColor = true;
            this.btnOpendlg.Click += new System.EventHandler(this.btnOpendlg_Click);
            // 
            // InstallServices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 362);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.group);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.Name = "InstallServices";
            this.Text = "InstallServices";
            this.Load += new System.EventHandler(this.InstallServices_Load);
            this.group.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnInstall;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox group;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Button btnUnInstall;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button chkStatus;
        private System.Windows.Forms.ComboBox cmbServiceList;
        private System.Windows.Forms.Button btnOpendlg;
        private System.Windows.Forms.Button btnRefresh;
    }
}

