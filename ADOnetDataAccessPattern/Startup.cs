using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ADOnetDataAccessPattern.Startup))]
namespace ADOnetDataAccessPattern
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
