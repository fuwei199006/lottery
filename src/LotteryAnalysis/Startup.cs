using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LotteryAnalysis.Startup))]
namespace LotteryAnalysis
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
