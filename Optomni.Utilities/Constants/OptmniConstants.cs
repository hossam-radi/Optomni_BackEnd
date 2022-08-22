using Optomni.Utilities.Model;
using System.Collections.Generic;

namespace Optmni.Utilities.Constants
{
    public static class OptmniConstants
    {
        public const string CustomerRole = "Customer";
        public const string GrowersRole = "Grower";
        public static string UploadProductPhotos = "Uploads/Product_photos";
        public const string Unifonic_MediaType = "application/json";
        public const string DevEnvironment = "Development";
        public static List<TempUser> Users = new List<TempUser>
        {
             new TempUser() { Email="Customer1@gmail.com" , Name="Customer1",Role=OptmniConstants.CustomerRole ,userType =Enums.UserTypes.Customer},
             new TempUser() { Email="Customer2@gmail.com" , Name="Customer2",Role=OptmniConstants.CustomerRole ,userType =Enums.UserTypes.Customer},
             new TempUser() { Email="Grower1@gmail.com" , Name="Grower1",Role=OptmniConstants.GrowersRole ,userType =Enums.UserTypes.Growers},
             new TempUser() { Email="Grower2@gmail.com" , Name="Grower2",Role=OptmniConstants.GrowersRole ,userType =Enums.UserTypes.Growers},
         };
    }
}
