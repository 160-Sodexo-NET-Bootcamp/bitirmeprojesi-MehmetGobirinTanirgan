using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PCS.Core.CoreEntities.Abstract;
using PCS.Core.Settings;
using PCS.Database.Context.Abstract;
using PCS.Entity.Models;
using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace PCS.Database.Context.Concrete
{
    public class PcsDbContext : DbContext, IPcsDbContext
    {
        private readonly AppDbSettings appDbSettings;

        public PcsDbContext(IOptions<AppDbSettings> appDbSettingsOptions)
        {
            this.appDbSettings = appDbSettingsOptions.Value;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<LockoutRecord> LockoutRecords { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<FailedEmail> FailedEmails { get; set; }
        public DbSet<Order> Orders { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            _ = appDbSettings.Provider switch
            {
                "MsSql" => optionsBuilder.UseSqlServer(appDbSettings.MsSqlConStr),
                "MySql" => optionsBuilder.UseMySql(appDbSettings.MySqlConStr,
                new MySqlServerVersion(new Version(8, 0, 27))),
                _ => throw new Exception($"{nameof(PcsDbContext)} => Unsupported provider: {appDbSettings.Provider ?? "null"}")
            };
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified).ToList();
            entries.ForEach(entry =>
            {
                if (entry.Entity is IDateSign entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.UtcNow;
                            if (entry.Entity is ISoftDeletable entitySoftDeletable)
                            {
                                entitySoftDeletable.IsDeleted = false;
                            }
                            break;
                        case EntityState.Modified:
                            entity.UpdatedDate = DateTime.UtcNow;
                            break;
                        default:
                            throw new Exception($"{nameof(PcsDbContext)} => Unexpected entity state: {entry.State}");
                    }
                }
            });
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
