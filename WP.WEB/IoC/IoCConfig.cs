

using Autofac;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using WP.BusinessLayer.Interfaces;
using WP.BusinessLayer.IoC;
using WP.BusinessLayer.Services;

namespace WP.WEB.IoC
{
    public class IoCConfig
    {
        public static void ConfigurationContainer() 
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(typeof(MvcApplication).Assembly);

            builder.RegisterModule(new ContainerModuleBL("ModelDb"));

            builder.RegisterType<UserServiceBL>().As<IUserBL>();
            builder.RegisterType<UserAuthenticationBL>().As<IAuthentificationBL>();

            builder.RegisterType<CommentServiceBL>().As<ICommentBL>();
            builder.RegisterType<RatingServiceBL>().As<IRatingBL>();
            builder.RegisterType<WorkServiceBL>().As<IWorkBL>();
            builder.RegisterType<GenreServiceBL>().As<IGenreBL>();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}