using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryLib
{
   public enum LogChannel
    {
       Exception=1001,//异常
       Running=1002,//程序运行
       Result=1003//开奖结果
    }

    public enum LotteryType
    {
       None=0,One=1,Two=2,Three=3,Four=4,Five=5,Six=6
    }
}
