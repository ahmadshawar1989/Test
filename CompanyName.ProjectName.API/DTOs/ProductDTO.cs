using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.API.DTOs
{
    public class MakeOrderDTO
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public List<OrderedProductDTO> OrderedProducts { get; set; }
    }

    public class OrderedProductDTO
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Count { get; set; }
    }

    public class ProductLineGetDTO
    {
        public int Id { get; set; }
        public int ItemsPerHour { get; set; }
    }
}
