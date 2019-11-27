using System;
using System.Collections.Generic;

namespace CompanyName.ProjectName.Model
{
    public partial class ProductOrderItem
    {
        public int Id { get; set; }
        public int ProductOrderId { get; set; }
        public int ProductLineItemId { get; set; }

        public virtual ProductLineItem ProductLineItem { get; set; }
        public virtual ProductOrder ProductOrder { get; set; }
    }
}
