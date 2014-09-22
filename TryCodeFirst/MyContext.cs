using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCodeFirst
{
    public class MyContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>().MapToStoredProcedures();

            base.OnModelCreating(modelBuilder);
        }
    }
}
