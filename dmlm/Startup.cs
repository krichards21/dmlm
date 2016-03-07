using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(dmlm.Startup))]
namespace dmlm
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
