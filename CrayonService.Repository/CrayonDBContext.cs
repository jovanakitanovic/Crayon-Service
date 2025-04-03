using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.EntityFrameworkCore;
using CrayonService.Repository.Models;

namespace CrayonService.Repository
{
    public class CrayonDBContext : DbContext
    {
        public CrayonDBContext(DbContextOptions<CrayonDBContext> options) : base(options) { }

        public DbSet<AccountCustomerLink> AccountCustomerLink { get; set; }
        public DbSet<Accounts> Account { get; set; }

        public DbSet<ServiceOrder> ServiceOrder { get; set; }

    }
}
