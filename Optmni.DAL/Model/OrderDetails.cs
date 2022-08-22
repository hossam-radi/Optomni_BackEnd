
using Optmni.DAL.Model;
using Optmni.Utilities.Enums;
using System;

namespace Optmni.DAL.Model
{
    public class OrderDetails : BaseModel
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public ProductUnit Unit { get; set; }
        public decimal Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public Guid OrderId { get; set; }
        public Order Order { get; set; }

    }
}
