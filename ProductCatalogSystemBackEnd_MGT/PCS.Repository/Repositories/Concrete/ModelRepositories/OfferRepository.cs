using Microsoft.EntityFrameworkCore;
using PCS.Database.Context.Concrete;
using PCS.Entity.Enums;
using PCS.Entity.Models;
using PCS.Repository.Repositories.Abstract;
using PCS.Repository.Repositories.Concrete.GenericRepositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PCS.Repository.Repositories.Concrete.ModelRepositories
{
    public class OfferRepository : SoftDeletableRepository<Offer, Guid>, IOfferRepository
    {
        public OfferRepository(PcsDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Offer>> GetOffersMadeByUserAsync(Guid userId)
        {
            return await GetListByExpression(x => x.OffererId == userId && x.OfferStatus != OfferStatus.Withdrew)
                .Include(x => x.Product).ThenInclude(y => y.ProductImages)
                .ToListAsync();
        }

        public async Task<IEnumerable<Offer>> GetOffersMadeToUserAsync(Guid userId)
        {
            return await GetListByExpression(x => x.ProductOwnerId == userId && x.OfferStatus != OfferStatus.Withdrew)
                .Include(x => x.Product).ThenInclude(y => y.ProductImages)
                .ToListAsync();
        }
    }
}
