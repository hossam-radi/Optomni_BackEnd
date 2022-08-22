using Optmni.Utilities.Enums;
using System;

namespace Optmni.DAL.Model
{
    public class Product:BaseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string? Picture { get; set; }
        public int? RegionId { get; set; }
        public decimal Price { get; set; }
        public Guid UserId { get; set; }
        public ProductType ProductType { get; set; }
        
    }
}
