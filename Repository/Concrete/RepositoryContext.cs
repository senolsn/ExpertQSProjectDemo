using Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities.Entities.Concrete;

namespace Repository.Concrete
{
    public class RepositoryContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocaldb;Database=Expert;Trusted_Connection=true");
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ChangeCode> Codes { get; set; }
        public DbSet<Change> ChangePrices { get; set; }
    }
}
