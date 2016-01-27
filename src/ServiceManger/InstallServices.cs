using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Windows.Forms;

namespace ServiceManger
{
    public partial class InstallServices : Form
    {
        string ServerName=string.Empty;
        string ServerPath=string.Empty;
        public InstallServices()
        {
            InitializeComponent();
            ServerName = GetWebSetting("servieName");
            ServerPath = GetWebSetting("servicesPath");
        }

        //安装服务
        private void btnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                IDictionary dictionary = new Hashtable();
                Windows.InstallmyService(dictionary, ServerPath);
                if (Windows.isServiceIsExisted(ServerName))
                {
                    this.label1.Text = "服务已经安装。。";
                    this.btnInstall.Enabled = false;
                    this.btnUnInstall.Enabled = true;
                    this.btnStart.Enabled = true;
                    this.btnStop.Enabled = false;
                    MessageBox.Show("服务安装成功！");
                }
            }
            catch (Exception exp)
            {
                this.label1.Text = "服务安装失败。。";
                MessageBox.Show("服务安装失败，ErrorCode:" + exp.Message);
            }
          
        }

        //卸载服务
        private void btnUnInstall_Click(object sender, EventArgs e)
        {
            try
            {
                IDictionary dictionary = new Hashtable();
                Windows.UnInstallmyService(dictionary, ServerPath);
                if (!Windows.isServiceIsExisted(ServerName))
                {
                    this.label1.Text = "服务已经卸载。。";
                    this.btnInstall.Enabled = true;
                    this.btnUnInstall.Enabled = false;
                    this.btnStart.Enabled = false;
                    this.btnStop.Enabled = false;
                    MessageBox.Show("服务卸载成功！");
                }
            }
            catch (Exception exp)
            {
                this.label1.Text = "服务卸载失败。。";
                MessageBox.Show("服务卸载失败，ErrorCode:" + exp.Message);
            }
        }
        //启动服务
        private void btnStart_Click(object sender, EventArgs e)
        {
            try
            {
                if (Windows.StarmyService(ServerName))
                {
                    this.label1.Text = "服务启动中。。";
                    if (Windows.IsRunning(ServerName))
                    {
                        this.label1.Text = "服务正在运行。。";
                        this.btnInstall.Enabled = false;
                        this.btnUnInstall.Enabled = false;
                        this.btnStart.Enabled = false;
                        this.btnStop.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                this.label1.Text = "服务启动失败。。";
                MessageBox.Show("服务启动失败，ErrorCode:" + exp.Message);
            }
        }
        //停止服务
        private void btnStop_Click(object sender, EventArgs e)
        {
            try
            {
                if (Windows.StopmyService(ServerName))
                {
                    this.label1.Text = "服务停止中。。";
                    if (!Windows.IsRunning(ServerName))
                    {
                        this.label1.Text = "服务已停止。。";
                        this.btnInstall.Enabled = false;
                        this.btnUnInstall.Enabled = true;
                        this.btnStart.Enabled = true;
                        this.btnStop.Enabled = false;
                    }
                }
            }
            catch (Exception exp)
            {
                this.label1.Text = "服务停止失败。。";
                MessageBox.Show("服务停止失败，ErrorCode:" + exp.Message);
            }
        }

        private void InstallServices_Load(object sender, EventArgs e)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (var serviceController in services)
            {
                this.cmbServiceList.Items.Add(serviceController.ServiceName);
            }
            if (Windows.isServiceIsExisted(ServerName))
            {
                this.btnInstall.Enabled = false;
                this.btnUnInstall.Enabled = true;
                this.btnStop.Enabled = false;
                this.btnStart.Enabled = false;
                this.label1.Text = "检测服务已经存在。。";
                if (Windows.IsRunning(ServerName))
                {
                    this.btnInstall.Enabled = false;
                    this.btnUnInstall.Enabled = false;
                    this.btnStart.Enabled = false;
                    this.btnStop.Enabled = true;
                    this.label1.Text = "检测服务已经启动。。";
                }
                else
                {
                    this.btnInstall.Enabled = false;
                    this.btnUnInstall.Enabled = true;
                    this.btnStop.Enabled = false;
                    this.btnStart.Enabled = true;
                    this.label1.Text = "检测服务已经停止。。";
                }
            }

            else
            {
                this.btnInstall.Enabled = true;
                this.btnUnInstall.Enabled = false;
                this.btnStop.Enabled = false;
                this.btnStart.Enabled = false;
                this.label1.Text = "检测服务不存在。。";
            }
        }


        public string GetWebSetting(string key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                return System.Configuration.ConfigurationSettings.AppSettings[key].ToString();
            }
            else
            {
                return string.Empty;
            }
        }

        private void btnOpendlg_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile=new OpenFileDialog();
            openFile.Filter = @"服务文件(*.exe)|*.exe"; 
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                ServerPath = openFile.FileName;
                this.txtPath.Text = openFile.FileName;
            }
        }

        private void chkStatus_Click(object sender, EventArgs e)
        {
            ServerName = this.cmbServiceList.Text;
            if (!Windows.isServiceIsExisted(ServerName))
            {
                this.label1.Text = "服务不存在..";
            }
            else if (Windows.IsRunning(ServerName))
            {
                this.label1.Text = "服务正在运行..";
                this.btnInstall.Enabled = false;
                this.btnUnInstall.Enabled = false;
                this.btnStart.Enabled = false;
                this.btnStop.Enabled = true;
            }
            else
            {
                this.label1.Text = "服务已经停止..";
                this.btnInstall.Enabled = false;
                this.btnUnInstall.Enabled = true;
                this.btnStop.Enabled = false;
                this.btnStart.Enabled = true;
            }

        }


        protected override void OnSizeChanged(EventArgs e)
        {
            //if (this.WindowState == FormWindowState.Minimized)
            //{
            //    this.Hide();
            //    this.notifyIcon1.Visible = true;
            //}
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

           //this.Show();

           // this.WindowState = FormWindowState.Normal;

           // this.notifyIcon1.Visible = false;

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.btnInstall.Enabled = true;
            this.btnUnInstall.Enabled = true;
            this.btnStop.Enabled = true;
            this.btnStart.Enabled = true;
            this.cmbServiceList.Items.Clear();
            ServiceController[] services = ServiceController.GetServices();
            foreach (var serviceController in services)
            {
                this.cmbServiceList.Items.Add(serviceController.ServiceName);
            }
        }

       
    }
}
