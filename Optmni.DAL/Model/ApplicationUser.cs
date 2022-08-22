
using Microsoft.AspNetCore.Identity;
using Optmni.DAL.Interfaces;
using Optmni.Utilities.Enums;
using System;

namespace Optmni.DAL.Model
{
    public class ApplicationUser : IdentityUser, IBaseModel
    {
        public UserTypes UserType { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }

     
    }
}
