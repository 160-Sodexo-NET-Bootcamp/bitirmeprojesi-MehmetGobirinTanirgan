using PCS.Core.Repositories.Abstract.GenericRepositories;
using PCS.Entity.Models;
using PCS.Repository.Repositories.Abstract;
using System;
using System.Threading.Tasks;

namespace PCS.Repository.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IGenericRepository<User, Guid> Users { get; }
        IGenericRepository<Product, Guid> Products { get; }
        IGenericRepository<ProductImage,Guid> ProductImages { get; }
        IOfferRepository Offers { get; }
        IGenericRepository<Category, Guid> Categories { get; }
        IGenericRepository<Brand, Guid> Brands { get; }
        IGenericRepository<Color, int> Colors { get; }
        IGenericRepository<LockoutRecord, Guid> LockoutRecords { get; }
        IGenericRepository<Email, Guid> Emails { get; }
        IGenericRepository<FailedEmail, Guid> FailedEmails { get; }
        IGenericRepository<Order, Guid> Orders { get; }
        IGenericRepository<PaymentType, int> PaymentTypes { get; }
        Task SaveAsync();
    }
}
