

using Autofac;
using WP.DataLayer.UnitOfWork;

namespace WP.BusinessLayer.IoC
{
    public class ContainerModuleBL : Module
    {
        private readonly string _connection;

        public ContainerModuleBL(string connection)
        {
            _connection = connection;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(container => new UnitOfWork(_connection)).As<IUnitOfWork>();
            base.Load(builder);
        }
    }
}
