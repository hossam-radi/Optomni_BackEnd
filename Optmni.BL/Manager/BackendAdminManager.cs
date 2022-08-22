using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Optmni.BL.Manager.Interface;
using Optmni.DAL;
using Optmni.DAL.Model;
using Optmni.DAL.UnitOfWork;
using Optmni.Utilities.Constants;
using Optmni.Utilities.Enums;
using Optomni.Utilities.Settings;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Optmni.BL.Manager
{
    public class BackendAdminManager : IBackendAdminManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly OptmniSettings _settings;
        private readonly OptmniDbContext _context;

        public BackendAdminManager(UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IUnitOfWork unitOfWork,
            OptmniDbContext context,
        IOptions<OptmniSettings> settings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _settings = settings.Value;
            _context = context;
        }


        public async Task CreateTempAccount()
        {
            foreach (var item in OptmniConstants.Users)
            {
                var accountExist = _userManager.Users
                                  .Any(a => a.Email.Equals(item.Email) &&
                                   !a.IsDeleted);

                if (!accountExist)
                {
                    ApplicationUser userModel = new ApplicationUser
                    {
                        UserName = item.Name,
                        Email = item.Email,
                        FullName = item.Name,
                        Address = "test",
                        NormalizedEmail = item.Email.ToUpper(),
                        NormalizedUserName = item.Name.ToUpper(),
                        EmailConfirmed = true,
                        ConcurrencyStamp = Guid.NewGuid().ToString(),
                        LockoutEnabled = true,
                        CreatedAt = DateTime.Now,
                        SecurityStamp = Guid.NewGuid().ToString("N").ToUpper(),
                        UserType = item.userType
                    };


                    var xx = await _userManager.CreateAsync(userModel, "User@100");
                    await _userManager.AddToRoleAsync(userModel, item.Role);
                    await _unitOfWork.SaveChangesAsync();
                }
            }
        }

        public async Task MigrateDatabases()
        {
            try
            {
                _context.Database.Migrate();
            }
            catch (Exception ex)
            {
                // _logger.LogError($"Main db migration error: {ex.Message}");
            }


        }
    }
}
