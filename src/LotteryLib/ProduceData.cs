using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LotteryLib.LotteryProduce;

namespace LotteryLib
{
    public class ProduceData : BaseModual
    {
        public int InsertLotter(List<Lottery> lotteries)
        {
            var sql = @" INSERT dbo.lottery
                        ( periodId ,
                          redBall1 ,
                          redBall2 ,
                          redBall3 ,
                          redBall4 ,
                          redBall5 ,
                          redBall6 ,
                          blueBall1 ,
                          createDate ,
                          releaseDate 
                         
                        )
                     VALUES ( '{0}' ,
                              '{1}' ,
                              '{2}' ,
                              '{3}' ,
                              '{4}' ,
                              '{5}' ,
                              '{6}' ,
                              '{7}' ,
                              GETDATE() ,
                              '{8}'
                            )";
            string lotterySql = string.Empty;
            foreach (var lottery in lotteries)
            {
                lotterySql += string.Format(sql, lottery.periodId, lottery.redBall1, lottery.redBall2, lottery.redBall3,
                    lottery.redBall4, lottery.redBall5, lottery.redBall6, lottery.blueBall1, lottery.releaseDate);
            }
            if (string.IsNullOrEmpty(lotterySql)) return 0;
            var count = sqlHelper.ExceSql(lotterySql);
            return count;
        }

        public int InsertLotter(Lottery lottery)
        {
            var sql = @" INSERT dbo.lottery
                        ( periodId ,
                          redBall1 ,
                          redBall2 ,
                          redBall3 ,
                          redBall4 ,
                          redBall5 ,
                          redBall6 ,
                          blueBall1 ,
                          createDate ,
                          releaseDate 
                         
                        )
                     VALUES ( '{0}' ,
                              '{1}' ,
                              '{2}' ,
                              '{3}' ,
                              '{4}' ,
                              '{5}' ,
                              '{6}' ,
                              '{7}' ,
                              GETDATE() ,
                              '{8}'
                            )";
            string lotterySql = string.Empty;
            lotterySql += string.Format(sql, lottery.periodId, lottery.redBall1, lottery.redBall2, lottery.redBall3,
                   lottery.redBall4, lottery.redBall5, lottery.redBall6, lottery.blueBall1, lottery.releaseDate);

            if (string.IsNullOrEmpty(lotterySql)) return 0;
            var count = sqlHelper.ExceSql(lotterySql);
            return count;
        }

        //public Lottery GetLotteryByPeriod(string period)
        //{
            
        //}

        public void WriteLog(string content, LogChannel channel)
        {
            var sql = @" INSERT dbo.Logger
                    ( logContent ,
                      createDate ,
                      logChannel
                    )
            VALUES  ( N'{0}' ,  
                    GETDATE() , 
                    {1})";
            sql = string.Format(sql, content, (int)channel);
            sqlHelper.ExceSql(sql);
        }

        public void CreateForecast(List<Forecast> forecasts)
        {
            var sql = @"INSERT  dbo.forecast
                        ( periodId ,
                            redBall1 ,
                            redBall2 ,
                            redBall3 ,
                            redBall4 ,
                            redBall5 ,
                            redBall6 ,
                            blueBall1 ,
                            createDate ,
                            forecastType ,
                            isLottery,
                            CostMoney,
                            benifitCount,
                            hitLottery,
                            hitRed,
                            hitBlue,totalRedCount

                        )
                VALUES  ( '{0}' ,
                            '{1}' ,
                            '{2}' ,
                            '{3}' ,
                            '{4}' ,
                            '{5}' ,
                            '{6}' ,
                            '{7}' ,
                            GETDATE() ,
                            '{8}' ,
                            '{9}',
                            '{10}',
                            0,'0','0','0','0' 
                        )";
            var forecastSql = string.Empty;
            foreach (var forecast in forecasts)
            {
                forecastSql += string.Format(sql, forecast.periodId, forecast.redBall1, forecast.redBall2,
                    forecast.redBall3, forecast.redBall4,
                    forecast.redBall5, forecast.redBall6,
                    forecast.blueBall1, forecast.forecastType, "-1", forecast.CostMoney);
            }
            if (!string.IsNullOrEmpty(forecastSql))
            {
                int count = sqlHelper.ExceSql(forecastSql);
                WriteLog(string.Format("当前产生了{0}条预测数据", count), LogChannel.Running);
            }
        }

        public List<Forecast> GetForecastByPeriod(string period)
        {
            var sql = "SELECT * FROM dbo.forecast WHERE periodId=@period AND isLottery=-1";
            var paras = new SqlParameter[1]
            {
                new SqlParameter("@period",period)
            };
            var forecastList = new List<Forecast>();
            var db = sqlHelper.ExecReturnDataSet(sql, paras, CommandType.Text).Tables[0];
            if (db != null && db.Rows.Count > 0)
            {
                foreach (DataRow row in db.Rows)
                {
                    forecastList.Add(new Forecast()
                    {
                        id = Convert.ToInt32(row["id"]),
                        periodId = period,
                        redBall1 = row["redBall1"].ToString(),
                        redBall2 = row["redBall2"].ToString(),
                        redBall3 = row["redBall3"].ToString(),
                        redBall4 = row["redBall4"].ToString(),
                        redBall5 = row["redBall5"].ToString(),
                        redBall6 = row["redBall6"].ToString(),
                        blueBall1 = row["blueBall1"].ToString(),
                        forecastType = row["forecastType"].ToString(),
                        createDate = DateTime.Now,
                        CostMoney = int.Parse(row["CostMoney"].ToString()),
                        benifitCount = 0,
                        isLottery = 0,
                        hitLottery = "0"
                    });
                }
                return forecastList;
            }
            return null;
        }

        public int GetForecastByperiodAndType(string period, string type)
        {
            var sql = "SELECT * FROM dbo.forecast WHERE periodId=@period AND forecastType=@forecastType";
            var paras = new SqlParameter[2]
            {
                new SqlParameter("@period",period),
                new SqlParameter("@forecastType",type)
            };

            var db = sqlHelper.ExecReturnDataSet(sql, paras, CommandType.Text).Tables[0];
            if (db != null && db.Rows.Count > 0)
            {
                return db.Rows.Count;
            }
            return 0;
        }
        public int UpdateForecast(Forecast forecast)
        {
            var sql = @"UPDATE FORECAST SET benifitCount=" + forecast.benifitCount + ",isLottery=" + (int)forecast.isLottery + ",hitLottery='" + forecast.hitLottery
                + "',hitRed='" + forecast.hitRed + "',hitBlue='" + forecast.hitBlue + "',totalRedCount='"+forecast.totalRedCount + "' WHERE id=" + forecast.id;
            var count = sqlHelper.ExceSql(sql);
            if (forecast.isLottery != LotteryType.None)
            {
                WriteLog(string.Format("恭喜你,你的{0}期中{1}等奖,奖金为{2}元,预测方式为{3}", forecast.periodId, (int)forecast.isLottery, forecast.benifitCount,forecast.forecastType), LogChannel.Result);

            }
            return count;
        }

        public List<string> GetForecastBallByPeriod(string period)
        {
            var sql = " SELECT  redBall FROM dbo.V_ForeCastRedBall WHERE periodId='" + period + "' GROUP BY redBall";
            var db = sqlHelper.ExecReturnDataSet(sql).Tables[0];
            List<string> strList=new List<string>();
            if (db != null && db.Rows.Count>0)
            {
                foreach (DataRow dataRow in db.Rows)
                {
                    strList.Add(dataRow["redBall"].ToString());
                    strList.Add(dataRow["redBall"].ToString());
                    strList.Add(dataRow["redBall"].ToString());
                    strList.Add(dataRow["redBall"].ToString());
                    strList.Add(dataRow["redBall"].ToString());
                    strList.Add(dataRow["redBall"].ToString());
                }
               
            }
            return strList;
        } 

    }
}
