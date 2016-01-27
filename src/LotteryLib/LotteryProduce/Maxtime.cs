using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LotteryLib.LotteryProduce
{
    public class Maxtime : BaseModual, ILotteryProduce
    {
        public void GetForecastResult(int periodCount, int count, bool isMulti = false)
        {
            var nextPeriod = GetNextPeriod();
            if (isMulti)
            {
                var isHad = ProduceData.GetForecastByperiodAndType(nextPeriod, "MaxTime");
                if (isHad > 0) return;
            }
            periodCount = periodCount * 6;
            var sql = @"SELECT TOP 6 lottery.redBall ,
                        COUNT(0) AS ballCount
                        FROM    ( SELECT TOP {0}  periodId ,redBall
                        FROM      dbo.V_RedBall
                        ORDER BY  periodId DESC) AS lottery
                        GROUP BY lottery.redBall
                        ORDER BY ballCount DESC;
                        SELECT TOP {1}  lottery.blueBall1,COUNT(0) AS count 
                        FROM (SELECT TOP {0} * FROM dbo.lottery ORDER BY periodId DESC) AS lottery
                        GROUP BY lottery.blueBall1 ORDER BY COUNT DESC";
            sql = string.Format(sql, periodCount, count);
            var ds = sqlHelper.ExecReturnDataSet(sql);
            var redBallDb = ds.Tables[0];
            var blueBallDb = ds.Tables[1];

            if (redBallDb != null && blueBallDb != null && blueBallDb.Rows.Count > 0 && redBallDb.Rows.Count > 0)
            {
                var arr = new string[]
                {
                   redBallDb.Rows[0]["redBall"].ToString(),
                   redBallDb.Rows[1]["redBall"].ToString(),
                   redBallDb.Rows[2]["redBall"].ToString(),
                   redBallDb.Rows[3]["redBall"].ToString(),
                   redBallDb.Rows[4]["redBall"].ToString(),
                   redBallDb.Rows[5]["redBall"].ToString()
                };
                arr = arr.OrderBy(r => r).ToArray();
                var maxTimeList = new List<Forecast>();
                foreach (DataRow dataRow in blueBallDb.Rows)
                {

                    maxTimeList.Add(new Forecast()
                    {
                        periodId = nextPeriod,
                        redBall1 = arr[0].ToString(),
                        redBall2 = arr[1].ToString(),
                        redBall3 = arr[2].ToString(),
                        redBall4 = arr[3].ToString(),
                        redBall5 = arr[4].ToString(),
                        redBall6 = arr[5].ToString(),
                        blueBall1 = dataRow["blueBall1"].ToString(),
                        createDate = DateTime.Now,
                        forecastType = "MaxTime",
                        isLottery = LotteryType.None,
                        CostMoney = 2 * count
                    });
                }


                ProduceData.CreateForecast(maxTimeList);
            }
        }

    }
}
