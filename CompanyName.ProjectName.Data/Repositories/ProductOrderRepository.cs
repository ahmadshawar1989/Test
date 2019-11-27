using CompanyName.ProjectName.Data.Infrastructure;
using CompanyName.ProjectName.Domain.Contracts.Repositories;
using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CompanyName.ProjectName.Data.Repositories
{
    public class ProductOrderRepository : RepositoryBase<ProductOrder>, IProductOrderRepository
    {
        public ProductOrderRepository(AppDbContext dbContext)
            : base(dbContext) { }
    }
}
