using System;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Model
{
    public partial class Order
    {
        public Order()
        {
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
