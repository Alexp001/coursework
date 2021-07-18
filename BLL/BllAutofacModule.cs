using Autofac;
using BLL.Managers;
using DataAccessLevel;

namespace BLL
{
    public class BllAutofacModule : Module
    {
        private readonly string _connactionString;
        
        public BllAutofacModule(string connectionString)
        {
            _connactionString = connectionString;
        }
        
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule(new DataAccessAutofacModule(_connactionString));

            builder.RegisterType<AccountingManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClientManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<CommentManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderItemManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<PizzaManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<OrderManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClientUserManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserRoleManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<ClientIdManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeIdManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<EmployeeUserManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<RoleManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<UserManager>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            builder.RegisterType<Report>().AsImplementedInterfaces()
                .InstancePerLifetimeScope();

        }
    }
}
