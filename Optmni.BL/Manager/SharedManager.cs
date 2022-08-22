using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Optmni.BL.Manager.Interface;
using Optmni.DAL.Model;
using Optomni.Utilities.Settings;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Optmni.BL.Manager
{
    public class SharedManager : ISharedManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly OptmniSettings _settings;
        private readonly string _baseUrl;

        public SharedManager(
            IOptions<OptmniSettings> settings,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _settings = settings.Value;;
        }
        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.jwt.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> userClaims = new List<Claim>();

          

            var claims = await _userManager.GetClaimsAsync(user);
            userClaims.AddRange(claims);

            // Get User Role 
            var roles = await _userManager.GetRolesAsync(user);// await _roleManager.user.Where(x => x.UserId == user.Id && !x.Role.IsDeleted).Select(x => x.Role.Name).ToListAsync();
            // var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                var r = await _roleManager.FindByNameAsync(role);
                var roleClaims = await _roleManager.GetClaimsAsync(r);
                userClaims.AddRange(roleClaims);
            }

            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()));
            int userType = (int)user.UserType;
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Typ, userType.ToString()));

            foreach (var role in roles)
            {
                var roleValue = await _roleManager.FindByNameAsync(role);
                userClaims.Add(new Claim("role", roleValue.Name));
            }

            var token = new JwtSecurityToken
            (
                _settings.jwt.Issuer,
                _settings.jwt.Audience,
                userClaims.GroupBy(x => x.Value).Select(y => y.First()).Distinct(),
                DateTime.Now,
                DateTime.Now.AddSeconds(_settings.System.Access_Token_Life_Time),
                credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
