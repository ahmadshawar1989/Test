using System;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Model
{
    public partial class Product
    {
        public Product()
        {
            ProductLines = new HashSet<ProductLine>();
            ProductOrders = new HashSet<ProductOrder>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int MinAmount { get; set; }

        public virtual ICollection<ProductLine> ProductLines { get; set; }
        public virtual ICollection<ProductOrder> ProductOrders { get; set; }
    }
}
