using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Model
{
    public class MakeOrder
    {
        public int CustomerId { get; set; }
        public List<OrderedProduct> OrderedProducts { get; set; }
    }

    public class OrderedProduct
    {
        public int ProductId { get; set; }
        public int Count { get; set; }
    }
}
