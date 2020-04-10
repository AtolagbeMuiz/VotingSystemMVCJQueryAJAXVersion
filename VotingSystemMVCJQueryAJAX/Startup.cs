using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VotingSystemMVCJQueryAJAX.Startup))]
namespace VotingSystemMVCJQueryAJAX
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
