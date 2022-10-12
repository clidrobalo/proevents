using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ProEvents.Domain.Enum;

namespace ProEvents.Domain.Identity
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Password { get; set; }
        public Title Title { get; set; }
        public string ImageURL { get; set; }
        public string description { get; set; }
        public Function function { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }
    }
}