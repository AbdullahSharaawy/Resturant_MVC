using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.Entities
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; } = "First Name";
        public string LastName { get; set; } = "Last Name";
        public string? ImagePath { get; set; } = "PersonIcon.svg";
    }
}
