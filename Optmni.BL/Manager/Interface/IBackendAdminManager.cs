using System.Threading.Tasks;

namespace Optmni.BL.Manager.Interface
{
    public interface IBackendAdminManager
    {
        Task CreateTempAccount();
        Task MigrateDatabases();
    }
}
