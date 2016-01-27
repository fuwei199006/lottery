using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryLib.LotteryProduce
{
   public class ZGetRadomForecast : BaseModual, ILotteryProduce
    {
        public void GetForecastResult(int periodCount, int count, bool isMulti = false)
        {
            var nextPeriod = GetNextPeriod();
          
            if (isMulti)
            {
                var isHad = ProduceData.GetForecastByperiodAndType(nextPeriod, "ZGetRadomForecast");
                if (isHad > 0) return;
            }
            var foreCastSql = "SELECT redBall FROM dbo.V_ForeCastRedBall where periodId='" + nextPeriod +
                              "' GROUP BY redBall";
            var allBlueBall = new List<string>();
         
            for (int i = 1; i < 17; i++)
            { 
                if (i < 10)
                {
                    allBlueBall.Add("0"+i);
                }
                allBlueBall.Add(i.ToString());
               
            }
             var dicBlueLost = new Dictionary<string, int>();
 
            var blueSql = @"SELECT * FROM dbo.lottery WHERE blueBall1='{0}' ORDER  BY periodId DESC";
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
             var blueList = dicBlueLost.OrderBy(r => r.Value).ToList();
            var redDb = sqlHelper.ExecReturnDataSet(foreCastSql).Tables[0];
            var listRandom = new List<Forecast>();
            if (redDb != null && redDb.Rows.Count > 0)
            {
                var dbCount = redDb.Rows.Count;
                var singalCount = dbCount/6;
                var list = redDb.AsEnumerable().Select(x => x["redBall"].ToString()).ToList();
               
              
                var random = new Random();
                for (int i = 0; i < singalCount; i++)
                {
                     var redList=new List<string>();
                    for (int a = 0; a < 6; a++)
                    {
                        int len = list.Count();
                        var r = random.Next(0,len);
                        redList.Add(list[r]);
                        list.RemoveAt(r);
                    }
                    redList = redList.OrderBy(r => r).ToList();
                    var forecast=new Forecast()
                    {
                        periodId = nextPeriod,
                        redBall1 = redList[0].ToString(),
                        redBall2 = redList[1].ToString(),
                        redBall3 = redList[2].ToString(),
                        redBall4 = redList[3].ToString(),
                        redBall5 = redList[4].ToString(),
                        redBall6 = redList[5].ToString(),
                        blueBall1 = blueList[random.Next(0,blueList.Count)].Key.ToString(),
                        forecastType = "ZGetRadomForecast",
                        CostMoney = 2,
                        benifitCount = 0,
                        hitBlue = "0",
                        hitRed = "0",
                        isLottery = 0,
                        hitLottery = "0"
                    };
                    listRandom.Add(forecast);

                  
                    
                }
                  ProduceData.CreateForecast(listRandom);
            }
        }
    }
}
