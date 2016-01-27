using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotteryLib.LotteryProduce;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LotteryLib.LotteryProduce.Tests
{
    [TestClass()]
    public class ZGetRadomForecastTests
    {
        [TestMethod()]
        public void GetForecastResultTest()
        {
            var helpe=new ZGetRadomForecast();
            helpe.GetForecastResult(2016010,1);
        }
    }
}
