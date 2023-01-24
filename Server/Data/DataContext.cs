using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace WDeff.Server.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Translation> Translations { get; set; }
    }
}
