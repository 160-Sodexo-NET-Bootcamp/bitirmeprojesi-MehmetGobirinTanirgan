using PCS.Core.Repositories.Abstract.GenericRepositories;
using PCS.Entity.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PCS.Repository.Repositories.Abstract
{
    public interface IOfferRepository : IGenericRepository<Offer,Guid>
    {
        Task<IEnumerable<Offer>> GetOffersMadeByUserAsync(Guid userId);
        Task<IEnumerable<Offer>> GetOffersMadeToUserAsync(Guid userId);
    }
}
