using CalculatonDAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;

namespace CalculatonDAL
{
    public class CalculationLogContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Server=calculation-log.cluster-csug06jc2ey9.us-east-1.rds.amazonaws.com;Database=calculation_log_db;User Id=postgres;Password=postgres;");

        public DbSet<CalculationItem> CalculationItems { get; set; }

        public override int SaveChanges()
        {
            int saveChg;

            try
            {
                saveChg = base.SaveChanges();
            }
            catch
            {
                throw;
            }

            return saveChg;
        }
    }
}