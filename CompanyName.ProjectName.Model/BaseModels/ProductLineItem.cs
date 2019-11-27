using System;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Model
{
    public partial class ProductLineItem
    {
        public ProductLineItem()
        {
            ProductOrderItems = new HashSet<ProductOrderItem>();
        }

        public int Id { get; set; }
        public int ProductLineId { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ProductLine ProductLine { get; set; }
        public virtual ICollection<ProductOrderItem> ProductOrderItems { get; set; }
    }
}
