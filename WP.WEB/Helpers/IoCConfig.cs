

using Autofac;
using Autofac.Integration.Mvc;

namespace WP.WEB.Helpers
{
    public class IoCConfig
    {
        public static void ConfigurationContainer() 
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            //builder.RegisterType<>
        }
    }
}