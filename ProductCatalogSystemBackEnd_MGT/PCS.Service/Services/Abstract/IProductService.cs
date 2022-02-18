using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Entity.Dtos.ProductDtos.Request;
using System;
using System.Threading.Tasks;

namespace PCS.Service.Services.Abstract
{
    public interface IProductService
    {
        Task<IResult> GetProductsWithImagesByCategoryIdAsync(Guid categoryId);
        Task<IResult> GetNecessaryDataForProductCreationAsync();
        Task<IResult> AddProductAsync(ProductCreateDto productCreateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> UpdateProductAsync(ProductUpdateDto productUpdateDto, ICheckableEntityHelper checkableEntityHelper);
        Task<IResult> DeleteProductAsync(Guid productId, ICheckableEntityHelper checkableEntityHelper);
    }
}
