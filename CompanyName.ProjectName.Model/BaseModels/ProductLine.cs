using System;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Model
{
    public partial class ProductLine
    {
        public ProductLine()
        {
            ProductLineItems = new HashSet<ProductLineItem>();
        }

        public int Id { get; set; }
        public int ProductId { get; set; }
        public bool IsRunning { get; set; }
        public int ItemsPerHour { get; set; }

        public virtual Product Product { get; set; }
        public virtual ICollection<ProductLineItem> ProductLineItems { get; set; }
    }
}
