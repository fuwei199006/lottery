using System.Collections.Generic;

namespace LotteryLib.LotteryProduce
{
    public interface ILotteryProduce
    {


        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="periodCount">根据多少期来预测</param>
        /// <param name="count">买多少注</param>
        /// <param name="isMulti">是否能多个预测</param>
        void GetForecastResult(int periodCount,int count,bool isMulti=false);
        
 

    }
}
