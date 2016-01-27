using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.ServiceProcess;
using System.Windows.Forms;
using System.Configuration.Install;

namespace ServiceManger
{
    class Windows
    {

        /// <summary>
        /// 检查服务存在的存在性
        /// </summary>
        /// <param name=" NameService ">服务名</param>
        /// <returns>存在返回 true,否则返回 false;</returns>
        public static bool isServiceIsExisted(string NameService)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController s in services)
            {
                if (s.ServiceName.ToLower() == NameService.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 安装Windows服务
        /// </summary>
        /// <param name="stateSaver">集合，当传递给 Install 方法时，stateSaver 参数指定的 IDictionary 应为空。</param>
        /// <param name="filepath">程序文件路径</param>
        public static void InstallmyService(IDictionary stateSaver, string filepath)
        {
            try
            {
                AssemblyInstaller AssemblyInstaller1 = new AssemblyInstaller();
                AssemblyInstaller1.UseNewContext = true;
                AssemblyInstaller1.Path = filepath;
                stateSaver.Clear();
                AssemblyInstaller1.Install(stateSaver);
                AssemblyInstaller1.Commit(stateSaver);
                AssemblyInstaller1.Dispose();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }
        }

        /// <summary>
        /// 卸载Windows服务
        /// </summary>
        /// <param name="filepath">程序文件路径</param>
        public static void UnInstallmyService(IDictionary stateSaver,string filepath)
        {
            try
            {
                AssemblyInstaller AssemblyInstaller1 = new AssemblyInstaller();
                AssemblyInstaller1.UseNewContext = true;
                AssemblyInstaller1.Path = filepath;
                AssemblyInstaller1.Uninstall(stateSaver);
                AssemblyInstaller1.Dispose();
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message.ToString());
            }
        }

        /// <summary>
        /// 检查Windows服务是否在运行
        /// </summary>
        /// <param name="name">程序的服务名</param>
        public static bool IsRunning(string name)
        {
            bool IsRun = false;
            try
            {
                if (!isServiceIsExisted(name))
                {
                    return false;
                }
                ServiceController sc = new ServiceController(name);
                if (sc.Status == ServiceControllerStatus.StartPending ||
                    sc.Status == ServiceControllerStatus.Running)
                {
                    IsRun = true;
                }
                sc.Close();
            }
            catch
            {
                IsRun = false;
            }
            return IsRun;
        }

        /// <summary>
        /// 启动Windows服务
        /// </summary>
        /// <param name="name">程序的服务名</param>
        /// <returns>启动成功返回 true,否则返回 false;</returns>
        public static bool StarmyService(string name)
        {
            ServiceController sc = new ServiceController(name);
            if (sc.Status == ServiceControllerStatus.Stopped || sc.Status == ServiceControllerStatus.StopPending)
            {

                sc.Start();
                //sc.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 3, 0));//等待3min
            }
           
            sc.Close();
            return true;
        }

        /// <summary>
        /// 停止Windows服务
        /// </summary>
        /// <param name="name">程序的服务名</param>
        /// <returns>停止成功返回 true,否则返回 false;</returns>
        public static bool StopmyService(string name)
        {
            ServiceController sc = new ServiceController(name);
            if (sc.Status == ServiceControllerStatus.Running ||
                sc.Status == ServiceControllerStatus.StartPending)
            {
                try
                {

                    sc.Stop();
                    sc.WaitForStatus(ServiceControllerStatus.Stopped, new TimeSpan(0, 0, 10));
                }
                catch (Exception exp)
                {
                    MessageBox.Show(exp.Message.ToString());
                }
            }
         
            sc.Close();
            return true;
        }

        /// <summary>
        /// 重启Windows服务
        /// </summary>
        /// <param name="name">程序的服务名</param>
        /// <returns>重启成功返回 true,否则返回 false;</returns>
        public static bool RefreshmyService(string name)
        {
            return StopmyService(name) && StarmyService(name);
        }
    }
}
