using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LotteryLib.LotteryProduce
{
    public class BaseModual
    {
        private SqlHelper _sqlHelper;
        public SqlHelper sqlHelper
        {
            get
            {
                if (_sqlHelper == null)
                {
                    var helper = new SqlHelper { conStr = "server=.;Database=LotteryTest;uid=sa;pwd=111" };
                    return helper;
                }
                return _sqlHelper;
            }

        }
        public ProduceData ProduceData
        {
            get
            {
                return new ProduceData();
            }
        }

        public List<ILotteryProduce> TaskLotteryProduces
        {
            get
            {
                var listCls = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes().Where(t => t.GetInterfaces().Contains(typeof(ILotteryProduce)))).OrderBy(r=>r.Name).ToArray();
                var intanceList = new List<ILotteryProduce>();
                foreach (var listCl in listCls)
                {
                    var instance = Assembly.GetExecutingAssembly().CreateInstance(listCl.FullName) as ILotteryProduce;
                    intanceList.Add(instance);
                }
                return intanceList;
            }
        }



        protected Lottery GetLastPeriod()
        {
            var sql = "SELECT TOP 1 * FROM dbo.lottery ORDER BY periodId DESC";

            var db = sqlHelper.ExecReturnDataSet(sql).Tables[0];
            if (db != null && db.Rows.Count > 0)
            {
                return new Lottery()
                {
                    periodId = db.Rows[0]["periodId"].ToString(),
                    releaseDate = db.Rows[0]["releaseDate"].ToString(),
                };
            }
            return new Lottery();
        }

        protected string GetNextPeriod()
        {
            var lastLottery = GetLastPeriod();
            if (string.IsNullOrEmpty(lastLottery.periodId))
            {
                return "2016009";
            }
            var releaseDate = Convert.ToDateTime(lastLottery.releaseDate);

            if (releaseDate.AddDays(2).Year > releaseDate.Year)//跨年
            {
                return releaseDate.Year + "001";
            }
            else if(releaseDate.DayOfWeek==DayOfWeek.Thursday&&releaseDate.AddDays(3).Year>releaseDate.Year)
            {
                return releaseDate.Year + "001";
            }
            var period = int.Parse(lastLottery.periodId) + 1;
            return period.ToString();

        }
    }
}
