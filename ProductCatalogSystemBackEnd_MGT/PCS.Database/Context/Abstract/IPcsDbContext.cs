using Microsoft.EntityFrameworkCore;
using PCS.Entity.Models;
using System.Threading;
using System.Threading.Tasks;

namespace PCS.Database.Context.Abstract
{
    public interface IPcsDbContext
    {
        DbSet<User> Users { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductImage> ProductImages { get; set; }
        DbSet<Offer> Offers { get; set; }
        DbSet<Category> Categories { get; set; }
        DbSet<Brand> Brands { get; set; }
        DbSet<Color> Colors { get; set; }
        DbSet<LockoutRecord> LockoutRecords { get; set; }
        DbSet<Email> Emails { get; set; }
        DbSet<FailedEmail> FailedEmails { get; set; }
        DbSet<Order> Orders { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
