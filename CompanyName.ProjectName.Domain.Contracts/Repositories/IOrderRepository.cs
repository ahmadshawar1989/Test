using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Domain.Contracts.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        int GetAvailableItemsCount(int productId);
        List<ProductLineItem> GetAvailableItems(int productId, int count);
        void AddProductOrderItems(List<ProductOrderItem> productOrderItems);
    }
}
