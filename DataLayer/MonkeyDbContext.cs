using DataLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer {
    public class MonkeyDbContext: DbContext {
        public DbSet<ActionLog> ActionLogs { get; set; }

        public DbSet<TreeLog> TreeLogs { get; set; }

        public DbSet<MonkeyLog> MonkeyLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder) {
            builder.UseSqlServer(@"Data Source=DESKTOP-3CJB43N\SQLEXPRESS;Initial Catalog=MonkeyDb;Integrated Security=True");
        }
    }
}
