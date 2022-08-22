using Optmni.DAL.Model;
using Optmni.Utilities.Enums;
using System;
using System.Collections.Generic;

namespace Optmni.DAL.Model
{
    public class Order : BaseModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string CustomerId { get; set; }    
        public string GrowerId { get; set; } 
        public DateTime OrderDate { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public string? Notes { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }

    }
}
