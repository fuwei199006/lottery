using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LotteryLib;
using Timer = System.Threading.Timer;


namespace LotteryServices
{
    public partial class LotteryService : ServiceBase
    {
        
        public LotteryService()
        {
            InitializeComponent();
          
        }

        void timer_Tick()
        {
             
            var helper = new LotteryHelper {Url = "http://baidu.lecai.com/lottery/draw/list/50"};
            helper.CoreLogic();
            this.Stop();
            
        }

        protected override void OnStart(string[] args)
        {
            var timer = new Timer(state =>
            {
                timer_Tick();
            },null,0,10000);
            
        }

        protected override void OnStop()
        {
        }
    }
}
