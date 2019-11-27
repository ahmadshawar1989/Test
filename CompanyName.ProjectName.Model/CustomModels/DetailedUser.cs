using System;
using System.Collections.Generic;
using System.Text;

namespace CompanyName.ProjectName.Model
{
    public class DetailedUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public EntityStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
