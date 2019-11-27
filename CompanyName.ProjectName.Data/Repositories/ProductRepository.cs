using CompanyName.ProjectName.Data.Infrastructure;
using CompanyName.ProjectName.Domain.Contracts.Repositories;
using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CompanyName.ProjectName.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(AppDbContext dbContext)
            : base(dbContext) { }

    }
}
