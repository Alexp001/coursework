using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using DataAccessLevel.Repositories;

namespace DataAccessLevel
{
    public class DataAccessAutofacModule : Module
    {
        private readonly string _connectionString;
        public DataAccessAutofacModule(string connectionString)
        {
            _connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClientRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<AccountingRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();

            builder.RegisterType<CommentRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            builder.RegisterType<OrderRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            builder.RegisterType<PizzaRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            builder.RegisterType<EmployeeRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            builder.RegisterType<UserRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            builder.RegisterType<RoleRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
            
            builder.RegisterType<AccountingRolesRepository>().WithParameter("connectionString", _connectionString)
                .AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
