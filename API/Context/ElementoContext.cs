using Microsoft.EntityFrameworkCore;
using ModelsShared;

namespace API.Context
{
    public class ElementoContext:DbContext
    {
        const string connectionString = "MSSQL";
        public ElementoContext(IConfiguration config)
        {
            _config = config;
        }
        public DbSet<Elemento> Elementos { get; set; }

        private readonly IConfiguration _config;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_config.GetConnectionString(connectionString));

        }
    }
}
