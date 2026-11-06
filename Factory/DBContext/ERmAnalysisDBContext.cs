using DotNet8.WebApi.Factory.Model;
using Microsoft.EntityFrameworkCore;

namespace DotNet8.WebApi.Factory.DBContext
{
    public class ERmAnalysisDBContext : DbContext
    {
        public DbSet<ErmAnalysisHeader>         ErmAnalysisHeader { get; set; }
        public DbSet<ErmAnalysisSummaryModel>   dbc_ermAnalysisSummary { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=RED-SQL-T3-01;Database=Fixed Pricing Portal;Integrated Security=SSPI;User=CD14\\Senthil.Dhesign;Password=GardenTree88;Connect Timeout=60;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ErmAnalysisHeader>().Property(v => v.id).ValueGeneratedNever();
        }

    }
}
