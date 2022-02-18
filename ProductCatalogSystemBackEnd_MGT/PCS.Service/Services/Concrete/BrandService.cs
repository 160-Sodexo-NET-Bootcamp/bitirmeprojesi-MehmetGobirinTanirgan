using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PCS.Core.Extensions;
using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.BrandDtos.Request;
using PCS.Entity.Dtos.BrandDtos.Response;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;

        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task<IResult> GetAllBrandsAsync()
        {
            //Redis cache control
            var cachedAllBrands = await distributedCache.GetDataAsync<IEnumerable<Brand>>(CacheKeys.AllBrands);
            if (cachedAllBrands is not null)
            {
                var cachedBrandDefaultDtos = mapper.Map<IEnumerable<BrandDefaultDto>>(cachedAllBrands);
                return ResultGenerator.Ok("From Cache.", cachedBrandDefaultDtos);
            }

            //If it's not cached get all from db
            var allBrands = await unitOfWork.Brands.GetAllAsync();
            if (!allBrands.Any())
            {
                return ResultGenerator.NoContent();
            }

            //Caching data and returning it
            await distributedCache.SetDataAsync(CacheKeys.AllBrands, allBrands, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(5));
            var brandDefaultDtos = mapper.Map<IEnumerable<BrandDefaultDto>>(allBrands);
            return ResultGenerator.Ok(brandDefaultDtos);
        }

        //Adding new brand
        public async Task<IResult> AddBrandAsync(BrandCreateDto brandCreateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var doesBrandExist = await unitOfWork.Brands.AnyAsync(x => x.BrandName == brandCreateDto.BrandName);
            if (doesBrandExist)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E7);
            }

            var newBrand = mapper.Map<Brand>(brandCreateDto);
            checkableEntityHelper.AddMainUserAsCreator(newBrand);
            unitOfWork.Brands.Add(newBrand);
            await unitOfWork.SaveAsync();
            var newBrandDefaultDto = mapper.Map<BrandDefaultDto>(newBrand);
            return ResultGenerator.Created(newBrandDefaultDto);
        }

        //Updating brand
        public async Task<IResult> UpdateBrandAsync(BrandUpdateDto brandUpdateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingBrand = await unitOfWork.Brands.GetByIdAsync(brandUpdateDto.Id);
            if (existingBrand is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            mapper.Map(brandUpdateDto, existingBrand);
            checkableEntityHelper.AddMainUserAsUpdater(existingBrand);
            unitOfWork.Brands.Update(existingBrand);
            await unitOfWork.SaveAsync();
            var updatedBrandDefaultDto = mapper.Map<BrandDefaultDto>(existingBrand);
            return ResultGenerator.Ok(updatedBrandDefaultDto);
        }

        //Deleting brand
        public async Task<IResult> DeleteBrandAsync(Guid brandId, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingBrand = await unitOfWork.Brands.GetByIdAsync(brandId, x => x.Products);
            if (existingBrand is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            if (existingBrand.Products.Any(x => !x.IsDeleted))
            {
                return ResultGenerator.BadRequest(ErrorMessages.E9);
            }

            checkableEntityHelper.AddMainUserAsUpdater(existingBrand);
            unitOfWork.Brands.Delete(existingBrand);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }
    }
}
