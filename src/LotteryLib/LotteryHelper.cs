using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LotteryLib.LotteryProduce;

namespace LotteryLib
{
    public class LotteryHelper : BaseModual
    {
        public string Url { get; set; }

        public ProduceData ProduceData
        {
            get
            {
                return new ProduceData();
            }
        }

        private string GetUrlContent()
        {
            try
            {
                string strLine = String.Empty;
                var request = (HttpWebRequest)WebRequest.Create(this.Url);
                //var headers = new HttpRequestHeader();
                //request.Headers=new WebHeaderCollection();
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/49.0.2593.0 Safari/537.36";
                //request.Headers = headers;
                var response = request.GetResponse();
                var stream = response.GetResponseStream();
                if (stream != null && stream != Stream.Null)
                {
                    var streamReader = new StreamReader(stream, Encoding.UTF8);
                    StringBuilder sb = new StringBuilder();
                    while ((strLine = streamReader.ReadLine()) != null)
                    {
                        sb.Append(strLine);
                    }


                    return sb.ToString();

                }
            }
            catch (Exception e)
            {

                ProduceData.WriteLog(e.Message.ToString(), LogChannel.Exception);
            }

            return String.Empty;
        }

        private List<Lottery> GetNearLottery()
        {
            var content = GetUrlContent();
            List<string> strList = new List<string>();
            var lotteryList = new List<Lottery>();
            var lastLottery = GetLastPeriod();
            if (string.IsNullOrEmpty(content))
            {
                return lotteryList;
            }
            var regex = new Regex(@"<td>[^<>|^&nbsp]+</td>|<em>[^<>|^&nbsp]+</em>|<a[^<>]+>\d+</a>");
            var htmlRegex = new Regex("<[^<>]+>");

            var matche = regex.Matches(content);
            foreach (var group in matche)
            {
                var strNum = htmlRegex.Replace(group.ToString(), "");
                if (strNum.Contains("（") || strNum.Contains("("))
                {
                    strNum = strNum.Split('（')[0];
                    strNum = strNum.Split('(')[0];
                }
                strList.Add(strNum);
            }
            for (int i = 0; i < strList.Count; i = i + 9)
            {
                var list = strList.Skip(i).Take(9).ToArray();
                if (list.Length < 9 || list[0] == lastLottery.periodId) break;
                lotteryList.Add(new Lottery()
                {
                    periodId = list[0],
                    redBall1 = list[2],
                    redBall2 = list[3],
                    redBall3 = list[4],
                    redBall4 = list[5],
                    redBall5 = list[6],
                    redBall6 = list[7],
                    blueBall1 = list[8],
                    releaseDate = list[1],
                    createDate = DateTime.Now
                });

            }
            return lotteryList.OrderBy(r => int.Parse(r.periodId)).ToList();
        }



        public void CoreLogic()
        {
            var nearLottery = GetNearLottery();
            if (nearLottery == null || nearLottery.Count == 0)
            {
                ProduceData.WriteLog("当前的数据已经是最新！", LogChannel.Running);
                return;
            }
            var count = nearLottery.Count;
            for (int i = 0; i < count; i++)
            {
                ProduceData.InsertLotter(nearLottery[i]);
                foreach (var taskLotteryProduce in TaskLotteryProduces)
                {
                    taskLotteryProduce.GetForecastResult(5, 1, true);

                }
            }

            CheckLottery(nearLottery);
            ProduceData.WriteLog(string.Format("获得数据更新成功，共获得{0}条数据", nearLottery.Count), LogChannel.Running);



        }


        private void CheckLottery(List<Lottery> lotteries)
        {

           
            foreach (var lottery in lotteries)
            {
               
                var forecastList = ProduceData.GetForecastByPeriod(lottery.periodId);
                if (forecastList != null)
                {
                    foreach (var forecast in forecastList)
                    {
                        int totalRedCount = 0;
                        int redCount = 0;
                        int blueCount = 0;
                        var redBallList = ProduceData.GetForecastBallByPeriod(forecast.periodId);
                       
                        var redLotteryBall = new string[]
                        {
                            lottery.redBall1,
                            lottery.redBall2,
                            lottery.redBall3,
                            lottery.redBall4,
                            lottery.redBall5,
                            lottery.redBall6,
                        };
                        var redForecastBall = new string[]
                        {
                            forecast.redBall1,
                            forecast.redBall2,
                            forecast.redBall3,
                            forecast.redBall4,
                            forecast.redBall5,
                            forecast.redBall6,
                        };
                        foreach (var s in redLotteryBall)
                        {
                            if (redForecastBall.Contains(s))
                            {
                                redCount++;
                                //totalRedCount++;
                            }
                            if (redBallList.Contains(s))
                            {
                                totalRedCount++;
                            }
                        }
                        if (forecast.blueBall1 == lottery.blueBall1)
                        {
                            blueCount++;
                        }

                        forecast.hitLottery = redCount + "+" + blueCount;
                        #region 中奖规则

                        if (blueCount != 0 || redCount > 3)
                        {

                            //6
                            if (redCount < 3 && blueCount == 1)
                            {
                                forecast.isLottery = LotteryType.Six;
                                forecast.benifitCount = 5;
                            }
                            //5
                            if ((redCount == 4 && blueCount == 0) || (redCount == 3 && blueCount == 1))
                            {
                                forecast.isLottery = LotteryType.Five;
                                forecast.benifitCount = 10;
                            }
                            //4
                            if ((redCount == 5 && blueCount == 0) || (redCount == 4 && blueCount == 1))
                            {
                                forecast.isLottery = LotteryType.Four;
                                forecast.benifitCount = 200;
                            }
                            //3
                            if (redCount == 5 && blueCount == 1)
                            {
                                forecast.isLottery = LotteryType.Three;
                                forecast.benifitCount = 3000;
                            }
                            //2
                            if (redCount == 6 && blueCount == 0)
                            {
                                forecast.isLottery = LotteryType.Two;
                                forecast.benifitCount = 200000;
                            }
                            //1
                            if (redCount == 6 && blueCount == 1)
                            {
                                forecast.isLottery = LotteryType.One;
                                forecast.benifitCount = 5000000;
                            }

                        }
                        else
                        {
                            forecast.isLottery = LotteryType.None;
                            forecast.benifitCount = 0;
                        
                        }
                        #endregion

                     
                        forecast.hitRed = redCount.ToString();
                        forecast.hitBlue = blueCount.ToString();
                        forecast.totalRedCount = totalRedCount.ToString();
                        ProduceData.UpdateForecast(forecast);
                    }





                }
            }
        }




    }
}
