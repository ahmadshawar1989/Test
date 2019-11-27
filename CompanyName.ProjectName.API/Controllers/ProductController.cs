using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CompanyName.ProjectName.API.DTOs;
using CompanyName.ProjectName.Domain.Contracts.Services;
using CompanyName.ProjectName.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CompanyName.ProjectName.API.Controllers
{
    /// <summary>
    /// This API controller contains APIs supporting the membership of the products who will fill the survey.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IProductService productService, IMapper mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        // POST: api/product/order
        /// <summary>
        /// Makes order by the customer.
        /// </summary>
        /// <returns>Feedback status</returns>
        [HttpPost("order")]
        public ActionResult MakeOrder(MakeOrderDTO model)
        {
            var order = _mapper.Map<MakeOrder>(model);
            _productService.MakeOrder(order);

            return Ok();
        }

        // GET: api/product/{productId}/line/status/{status}
        /// <summary>
        /// Gets product lines by product and status.
        /// </summary>
        /// <returns>Product lines</returns>
        [HttpGet("{productId}/line/status/{status}")]
        public ActionResult<IEnumerable<ProductLineGetDTO>> GetProductLines(int productId, ProductLineStatus status)
        {
            IEnumerable<ProductLine> productLines = _productService.GetProductLines(productId, status);
            var result = _mapper.Map<IEnumerable<ProductLineGetDTO>>(productLines);

            return Ok(result);
        }

        // GET: api/product/{productId}/items/available/count
        /// <summary>
        /// Gets available items count by product ID.
        /// </summary>
        /// <returns>Available items count</returns>
        [HttpGet("{productId}/items/available/count")]
        public ActionResult GetAvailableItemsCount(int productId)
        {
            int count = _productService.GetAvailableItemsCount(productId);
            return Ok(new { Count = count });
        }

        // POST: api/product/check
        /// <summary>
        /// Do logic hourly.
        /// </summary>
        /// <returns>Feedback status</returns>
        [HttpPost("order")]
        public ActionResult CheckProductsLines()
        {
            _productService.CheckProductsLines();
            return Ok();
        }
    }
}
