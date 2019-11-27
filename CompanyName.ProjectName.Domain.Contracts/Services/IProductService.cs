using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Domain.Contracts.Services
{
    /// <summary>
    /// Product service interface that exposes the relevant operations.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Makes order by the customer.
        /// </summary>
        /// <param name="orderObj">Order object</param>
        void MakeOrder(MakeOrder orderObj);

        /// <summary>
        /// Gets product lines by product and status.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <param name="status">Status</param>
        /// <returns>Product line objects</returns>
        IEnumerable<ProductLine> GetProductLines(int productId, ProductLineStatus status);

        /// <summary>
        /// Gets available items count by product ID.
        /// </summary>
        /// <param name="productId">Product ID</param>
        /// <returns>Count</returns>
        int GetAvailableItemsCount(int productId);

        /// <summary>
        /// Do logic that operates all products lines.
        /// </summary>
        void CheckProductsLines();
    }
}
