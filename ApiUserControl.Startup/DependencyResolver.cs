using ApiUserControl.Business.Services;
using ApiUserControl.Domain.Contracts.Repositories;
using ApiUserControl.Domain.Contracts.Services;
using ApiUserControl.Domain.Models;
using ApiUserControl.Infraestructure.Data;
using ApiUserControl.Infraestructure.Repositories;
using Unity;
using Unity.Lifetime;

namespace ApiUserControl.Startup
{
    public static class DependencyResolver
    {
        public static void Resolve(UnityContainer container)
        {
            container.RegisterType<AppDataContext, AppDataContext>(new HierarchicalLifetimeManager());

            container.RegisterType<IUserRepository, UserRepository>(new HierarchicalLifetimeManager());
            container.RegisterType<IUserService, UserService>(new HierarchicalLifetimeManager());

            container.RegisterType<User, User>(new HierarchicalLifetimeManager());
        }
    }
}
