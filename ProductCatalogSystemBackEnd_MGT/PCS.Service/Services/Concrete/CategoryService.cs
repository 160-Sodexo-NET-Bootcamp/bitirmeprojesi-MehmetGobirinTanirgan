using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PCS.Core.Extensions;
using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.CategoryDtos.Request;
using PCS.Entity.Dtos.CategoryDtos.Response;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task<IResult> GetAllCategoriesAsync()
        {   
            //Redis cache control
            var cachedAllCategories = await distributedCache.GetDataAsync<IEnumerable<Category>>(CacheKeys.AllCategories);
            if (cachedAllCategories is not null)
            {
                var cachedCategoryDefaultDtos = mapper.Map<IEnumerable<CategoryDefaultDto>>(cachedAllCategories);
                return ResultGenerator.Ok("From Cache.", cachedCategoryDefaultDtos);
            }

            //If it's not cached get all from db
            var allCategories = await unitOfWork.Categories.GetAllAsync();
            if (!allCategories.Any())
            {
                return ResultGenerator.NoContent(ErrorMessages.E10);
            }

            //Caching data and returning it
            await distributedCache.SetDataAsync(CacheKeys.AllCategories, allCategories, TimeSpan.FromSeconds(30), TimeSpan.FromMinutes(2));
            var categoryDefaultDtos = mapper.Map<IEnumerable<CategoryDefaultDto>>(allCategories);
            return ResultGenerator.Ok(categoryDefaultDtos);
        }

        //Adding new category
        public async Task<IResult> AddCategoryAsync(CategoryCreateDto categoryCreateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var doesCategoryExist = await unitOfWork.Categories.AnyAsync(x => x.CategoryName == categoryCreateDto.CategoryName);
            if (doesCategoryExist)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E7);
            }

            var newCategory = mapper.Map<Category>(categoryCreateDto);
            checkableEntityHelper.AddMainUserAsCreator(newCategory);
            unitOfWork.Categories.Add(newCategory);
            await unitOfWork.SaveAsync();
            var newCategoryDefaultDto = mapper.Map<CategoryDefaultDto>(newCategory);
            return ResultGenerator.Created(newCategoryDefaultDto);
        }

        //Updating category
        public async Task<IResult> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingCategory = await unitOfWork.Categories.GetByIdAsync(categoryUpdateDto.Id);
            if (existingCategory is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            mapper.Map(categoryUpdateDto, existingCategory);
            checkableEntityHelper.AddMainUserAsUpdater(existingCategory);
            unitOfWork.Categories.Update(existingCategory);
            await unitOfWork.SaveAsync();
            var updatedCategoryDefaultDto = mapper.Map<CategoryDefaultDto>(existingCategory);
            return ResultGenerator.Ok(updatedCategoryDefaultDto);
        }

        //Deleting category
        public async Task<IResult> DeleteCategoryAsync(Guid categoryId, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingCategory = await unitOfWork.Categories.GetByIdAsync(categoryId, x=> x.Products);
            if (existingCategory is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            if (existingCategory.Products.Any(x => !x.IsDeleted))
            {
                return ResultGenerator.BadRequest(ErrorMessages.E9);
            }

            checkableEntityHelper.AddMainUserAsUpdater(existingCategory);
            unitOfWork.Categories.Delete(existingCategory);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }
    }
}
