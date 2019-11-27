using System;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Model
{
    public partial class ProductOrder
    {
        public ProductOrder()
        {
            ProductOrderItems = new HashSet<ProductOrderItem>();
        }

        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }

        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public virtual ICollection<ProductOrderItem> ProductOrderItems { get; set; }
    }
}
