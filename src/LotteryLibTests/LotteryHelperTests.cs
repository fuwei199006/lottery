using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotteryLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LotteryLib.Tests
{
    [TestClass()]
    public class LotteryHelperTests
    {
        [TestMethod()]
        public void GetUrlContenTest()
        {
            //var helper=new LotteryHelper();
            //helper.Url = "http://baidu.lecai.com/lottery/draw/list/50";
            //var content=helper.GetUrlContent();
            //Console.WriteLine(content);
             
        }

        [TestMethod()]
        public void GetNearLotteryTest()
        {
            //var helper = new LotteryHelper();
            //helper.Url = "http://baidu.lecai.com/lottery/draw/list/50";
            //var content = helper.GetNearLottery();

        }

        [TestMethod()]
        public void CoreLogicTest()
        {
            var helper = new LotteryHelper();
            helper.Url = "http://baidu.lecai.com/lottery/draw/list/50";
            helper.CoreLogic();
        }
    }
}
