using Optmni.DAL.Model;
using System.Threading.Tasks;

namespace Optmni.BL.Manager.Interface
{
    public interface ISharedManager
    {
        Task<string> GenerateToken(ApplicationUser user);
    }
}
