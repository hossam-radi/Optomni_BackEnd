using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optomni.Utilities.Model;
using System.Threading.Tasks;

namespace Optomni.BL.Manager.Interface
{
    public interface ISecurityManager
    {
        Task<GeneralResponse<LoginResponseDTO>> Login(LoginRequestDTO model);

        Task<GeneralResponse<UserInfo>> GetUserInfo();

    }
}
