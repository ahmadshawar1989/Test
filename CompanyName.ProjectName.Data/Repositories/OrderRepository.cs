using CompanyName.ProjectName.Data.Infrastructure;
using CompanyName.ProjectName.Domain.Contracts.Repositories;
using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CompanyName.ProjectName.Data.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public List<ProductLineItem> GetAvailableItems(int productId, int count)
        {
            return (from a in DbContext.ProductLineItems
                    join b in DbContext.ProductOrderItems
                    on a.Id equals b.ProductLineItemId into leftJoin
                    from c in leftJoin.DefaultIfEmpty()
                    where c == null && a.ProductLine.ProductId == productId
                    orderby a.CreatedOn ascending
                    select a).Take(count).ToList();
        }

        public int GetAvailableItemsCount(int productId)
        {
            return (from a in DbContext.ProductLineItems
                    join b in DbContext.ProductOrderItems
                    on a.Id equals b.ProductLineItemId into leftJoin
                    from c in leftJoin.DefaultIfEmpty()
                    where c == null && a.ProductLine.ProductId == productId
                    select a).Count();
        }

        public void AddProductOrderItems(List<ProductOrderItem> productOrderItems)
        {
            DbContext.ProductOrderItems.AddRange(productOrderItems);
        }
    }
}
