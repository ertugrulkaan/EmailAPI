using EmailAPI.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace EmailAPI.Models
{
    public class ModelContext : DbContext
    {
        public DbSet<EmailSetting> EmailSettings { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Receiver> Receivers { get; set; }
        public DbSet<GroupReceiver> GroupReceivers { get; set; }

        public ModelContext()
            : base()
        {
        }
        public ModelContext(DbContextOptions<ModelContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = "EmailAPI.db" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);

            optionsBuilder.UseSqlite(connection);
        }
    }
}
