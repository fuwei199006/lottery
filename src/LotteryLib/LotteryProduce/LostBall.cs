using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryLib.LotteryProduce
{
    public class LostBall : BaseModual, ILotteryProduce
    {
        public void GetForecastResult(int periodCount, int count, bool isMulti = false)
        {
            var nextPeriod = GetNextPeriod();
            if (isMulti)
            {
                var isHad = ProduceData.GetForecastByperiodAndType(nextPeriod, "LostBall");
                if (isHad > 0) return;
            }
            var allRedBall = new List<string>();
            var allBlueBall = new List<string>();
            for (int i = 1; i < 34; i++)
            {
                if (i < 10)
                {
                    allRedBall.Add("0"+i);
                }
                allRedBall.Add(i.ToString());
            }
            for (int i = 1; i < 17; i++)
            { 
                if (i < 10)
                {
                    allBlueBall.Add("0"+i);
                }
                allBlueBall.Add(i.ToString());
               
            }
            var dicRedLost = new Dictionary<string, int>();
            var dicBlueLost = new Dictionary<string, int>();
            var redSql = @"SELECT top 1 * FROM  dbo.V_RedBall WHERE redBall='{0}' ORDER BY periodId DESC
 ";
            foreach (var i in allRedBall)
            {
               var _redSql = string.Format(redSql, i);
                var db = sqlHelper.ExecReturnDataSet(_redSql).Tables[0];
                if (db != null && db.Rows.Count > 0)
                {
                    dicRedLost.Add(i, int.Parse(db.Rows[0]["periodId"].ToString()));
                }
            }

            var blueSql = @"SELECT * FROM dbo.lottery WHERE blueBall1='{0}' ORDER  BY periodId DESC ";
            foreach (var j in allBlueBall)
            {
                var _blueSql = string.Format(blueSql, j);
                var db = sqlHelper.ExecReturnDataSet(_blueSql).Tables[0];
                if (db != null && db.Rows.Count > 0)
                {
                    dicBlueLost.Add(j, int.Parse(db.Rows[0]["periodId"].ToString()));
                }
            }

            //获得红球
            var redList = dicRedLost.OrderBy(r => r.Value).Take(6).OrderBy(r=>r.Key).ToList();
            var blueList = dicBlueLost.OrderBy(r => r.Value).Take(1).ToList();
            var forecast=new Forecast()
            {
                periodId = nextPeriod,
                redBall1 = redList[0].Key.ToString(),
                redBall2 = redList[1].Key.ToString(),
                redBall3 = redList[2].Key.ToString(),
                redBall4 = redList[3].Key.ToString(),
                redBall5 = redList[4].Key.ToString(),
                redBall6 = redList[5].Key.ToString(),
                benifitCount = 0,
                blueBall1 = blueList[0].Key.ToString(),
                forecastType = "LostBall",
                CostMoney = 2

            };
            var list=new List<Forecast>();
            list.Add(forecast);
            ProduceData.CreateForecast(list);
        




        }
    }
}
