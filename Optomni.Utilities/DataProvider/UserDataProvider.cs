using Microsoft.AspNetCore.Http;
using Optomni.Utilities.Model;
using System;
using System.Linq;
using System.Security.Claims;

namespace Optomni.Utilities.DataProvider
{
    public class UserDataProvider: IUserDataProvider
    {
        private readonly IHttpContextAccessor _accessor;

        public UserDataProvider(
            IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        public UserInfo GetUserInfo()
        {
            UserInfo info = new UserInfo();

            if (_accessor.HttpContext != null)
            {
                var user = _accessor.HttpContext.User;

                if (user != null)
                {
                    var claim = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
                    var claim2 = user.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                    if (claim is not null)
                    {
                        info.Id = Guid.Parse(claim.Value);
                    }

                    if (claim2 is not null)
                    {
                        info.Role = claim2.Value;
                    }
                }
            }

            return info;
        }
    }
}
