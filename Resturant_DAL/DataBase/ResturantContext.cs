using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resturant_DAL.DataBase
{
    public partial class ResturantContext: DbContext
    {
        public ResturantContext()
        {
        }

        public ResturantContext(DbContextOptions<ResturantContext> options)
            : base(options)
        {
        }

    }
}
