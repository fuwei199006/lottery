using System;

namespace LotteryLib
{
    public partial class Forecast
    {
        public int id { get; set; }
        public string periodId { get; set; }
        public string redBall1 { get; set; }
        public string redBall2 { get; set; }
        public string redBall3 { get; set; }
        public string redBall4 { get; set; }
        public string redBall5 { get; set; }
        public string redBall6 { get; set; }
        public string blueBall1 { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public string forecastType { get; set; }
        public LotteryType isLottery { get; set; }
        public int CostMoney { get; set; }
        public int benifitCount { get; set; }
        public string hitLottery { get; set; }
        public string hitRed { get; set; }
        public string hitBlue { get; set; }
        public string totalRedCount { get; set; }
        
    }
}
