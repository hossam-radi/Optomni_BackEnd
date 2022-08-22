using Microsoft.Extensions.DependencyInjection;
using Optmni.BL.Manager;
using Optmni.BL.Manager.Interface;
using Optmni.DAL.Repository;
using Optmni.DAL.UnitOfWork;
using Optomni.BL.Manager.Interface;
using Optomni.Utilities.DataProvider;
using Optomni.Utilities.FileUpload;

namespace Optmni.BL
{
    public static class DependencyBuilder
    {
        public static void BuildDebendencyBuilder(this IServiceCollection service)
        {
            service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            service.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            service.AddScoped(typeof(ILkp_RegionManager), typeof(Lkp_RegionManager));
            service.AddScoped(typeof(IFileUpload), typeof(FileUpload));
            service.AddScoped(typeof(IOrderManager), typeof(OrderManager));
            service.AddScoped(typeof(IProductManager), typeof(ProductManager));
            service.AddScoped(typeof(ISecurityManager), typeof(SecurityManager));
            service.AddScoped(typeof(ISharedManager), typeof(SharedManager));
            service.AddScoped(typeof(IBackendAdminManager), typeof(BackendAdminManager));
            service.AddScoped(typeof(IUserDataProvider), typeof(UserDataProvider));
            service.AddScoped(typeof(IProductManager), typeof(ProductManager));
            service.AddScoped(typeof(IOrderManager), typeof(OrderManager));
        }
    }
}