using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resturant_DAL.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
 

namespace Resturant_DAL.DataBase
{
    public partial class ResturantContext: IdentityDbContext<User>
    {
        public ResturantContext()
        {
        }

        public ResturantContext(DbContextOptions<ResturantContext> options)
            : base(options)
        {
        }
        public virtual DbSet<ReservedTable> ReservedTable { get; set; }
        public virtual DbSet<Reservation> Reservation { get; set; }
        public virtual DbSet<MenueItem> MenueItem { get; set; }
        public virtual DbSet<OrderItem> OrderItem { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<table> Table { get; set; }
        public virtual DbSet<Review> Review { get; set; }
        public virtual DbSet<Branch> Branch { get; set; }
        public virtual DbSet<Chief> Chief { get; set; }
        public virtual DbSet<Payment> Payment { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
