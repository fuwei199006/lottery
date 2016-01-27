using System;

namespace LotteryLib
{
    public partial class Lottery
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
        public string releaseDate { get; set; }
    }
}
