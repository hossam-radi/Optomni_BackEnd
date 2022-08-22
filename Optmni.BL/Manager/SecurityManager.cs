using Microsoft.AspNetCore.Identity;
using Optmni.BL.DTOs.RequestDTOs;
using Optmni.BL.DTOs.ResponseDTOs;
using Optmni.BL.Manager.Interface;
using Optmni.DAL.Model;
using Optmni.Utilities.Resources;
using Optomni.BL.Manager.Interface;
using Optomni.Utilities.DataProvider;
using Optomni.Utilities.Model;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Optmni.BL.Manager
{
    public class SecurityManager : ISecurityManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ISharedManager _sharedManager;
        private readonly IUserDataProvider _userDataProvider;

        public SecurityManager(UserManager<ApplicationUser> userManager,
            ISharedManager sharedManager,
            IUserDataProvider userDataProvider)
        {
            _userManager = userManager;
            _sharedManager = sharedManager;
            _userDataProvider = userDataProvider;
        }

        public async Task<GeneralResponse<UserInfo>> GetUserInfo()
        {
            return new GeneralResponse<UserInfo>
            {
                StatusCode = HttpStatusCode.OK,
                Data = _userDataProvider.GetUserInfo()
            };
        }

        /// <summary>
        /// Login For users
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<GeneralResponse<LoginResponseDTO>> Login(LoginRequestDTO model)
        {
            // Get 
            var user = _userManager.Users
                .FirstOrDefault(a => a.Email.Trim().ToLower().Equals(model.Email.Trim().ToLower()) &&
                !a.IsDeleted );

            if (user == null)
            {
                return new GeneralResponse<LoginResponseDTO>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    MessageKey = OptmniResources.InvalidEmailOrPassword
                };
            }

            var checkPassword = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!checkPassword)
            {
                return new GeneralResponse<LoginResponseDTO>
                {
                    StatusCode = HttpStatusCode.BadRequest,
                    MessageKey = OptmniResources.InvalidEmailOrPassword
                };
            }

            var token = await _sharedManager.GenerateToken(user);
            var loginData = new LoginResponseDTO
            {
                Token = token,
                UserName = user.UserName,
                UserType = user.UserType,
            };
            return new GeneralResponse<LoginResponseDTO>
            {
                StatusCode = HttpStatusCode.OK,
                Data = loginData
            };
        }

    }
}
