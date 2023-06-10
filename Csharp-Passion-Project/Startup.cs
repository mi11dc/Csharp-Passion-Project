using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Csharp_Passion_Project.Startup))]
namespace Csharp_Passion_Project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
