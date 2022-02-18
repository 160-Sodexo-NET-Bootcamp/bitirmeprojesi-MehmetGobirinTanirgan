using PCS.Core.Repositories.Abstract.GenericRepositories;
using PCS.Database.Context.Concrete;
using PCS.Entity.Models;
using PCS.Repository.Repositories.Abstract;
using PCS.Repository.Repositories.Concrete.GenericRepositories;
using PCS.Repository.Repositories.Concrete.ModelRepositories;
using PCS.Repository.UnitOfWork.Abstract;
using System;
using System.Threading.Tasks;

namespace PCS.Repository.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PcsDbContext dbContext;

        public UnitOfWork(PcsDbContext dbContext)
        {
            this.dbContext = dbContext;
            Users = new SoftDeletableRepository<User, Guid>(dbContext);
            Products = new SoftDeletableRepository<Product, Guid>(dbContext);
            ProductImages = new SoftDeletableRepository<ProductImage, Guid>(dbContext);
            Offers = new OfferRepository(dbContext);
            Categories = new SoftDeletableRepository<Category, Guid>(dbContext);
            Brands = new SoftDeletableRepository<Brand, Guid>(dbContext);
            Colors = new HardDeletableRepository<Color, int>(dbContext);
            LockoutRecords = new SoftDeletableRepository<LockoutRecord, Guid>(dbContext);
            Emails = new SoftDeletableRepository<Email, Guid>(dbContext);
            FailedEmails = new SoftDeletableRepository<FailedEmail, Guid>(dbContext);
            Orders = new SoftDeletableRepository<Order, Guid>(dbContext);
            PaymentTypes = new HardDeletableRepository<PaymentType, int>(dbContext);
        }

        public IGenericRepository<User, Guid> Users { get; private set; }
        public IGenericRepository<Product, Guid> Products { get; private set; }
        public IGenericRepository<ProductImage, Guid> ProductImages { get; private set; }
        public IOfferRepository Offers { get; private set; }
        public IGenericRepository<Category, Guid> Categories { get; private set; }
        public IGenericRepository<Brand, Guid> Brands { get; private set; }
        public IGenericRepository<Color, int> Colors { get; private set; }
        public IGenericRepository<LockoutRecord, Guid> LockoutRecords { get; private set; }
        public IGenericRepository<Email, Guid> Emails { get; private set; }
        public IGenericRepository<FailedEmail, Guid> FailedEmails { get; private set; }
        public IGenericRepository<Order, Guid> Orders { get; private set; }
        public IGenericRepository<PaymentType, int> PaymentTypes { get; private set; }

        public async Task SaveAsync()
        {
            await dbContext.SaveChangesAsync();
        }

        public async ValueTask DisposeAsync() // Implemented for specific scopes
        {
            await dbContext.DisposeAsync();
        }
    }
}
