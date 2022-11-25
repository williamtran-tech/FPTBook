using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPTBook.Startup))]
namespace FPTBook
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
