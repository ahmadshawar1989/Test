using CompanyName.ProjectName.Data.Infrastructure;
using CompanyName.ProjectName.Domain.Contracts.Repositories;
using CompanyName.ProjectName.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace CompanyName.ProjectName.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public IEnumerable<DetailedUser> GetUsers()
        {
            return (from a in DbContext.Users
                    select new DetailedUser
                    {
                        Id = a.Id,
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email,
                        Phone = a.Phone,
                        Status = (EntityStatus)a.Status,
                        CreatedOn = a.CreatedOn
                    }).ToList();
        }

        public User GetByPhone(string phone)
        {
            return DbContext.Users.FirstOrDefault(a => a.Phone == phone);
        }

        public User Login(string phone, string email)
        {
            return DbContext.Users.FirstOrDefault(a => a.Phone == phone && a.Email == email);
        }

        public bool IsPhoneUnique(string phone)
        {
            return !DbContext.Users.Any(a => a.Phone == phone && a.SmsCodePassedOn.HasValue);
        }

        public bool IsEmailUnique(string email)
        {
            return !DbContext.Users.Any(a => a.Email == email);
        }
    }
}
