using Autofac;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Repository.UnitOfWork.Concrete;
using PCS.Service.Services.Concrete;
using System.Reflection;

namespace PCS.Api.AutofacModules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetAssembly(typeof(AccountService)))
                .Where(x => x.Name.EndsWith("Service"))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<CheckableEntityHelper>().As<ICheckableEntityHelper>().InstancePerLifetimeScope();
            base.Load(builder);
        }
    }
}
