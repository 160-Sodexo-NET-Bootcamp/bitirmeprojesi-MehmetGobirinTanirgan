using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PCS.Core.CustomExceptions;
using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.BrandDtos.Response;
using PCS.Entity.Dtos.CategoryDtos.Response;
using PCS.Entity.Dtos.ColorDtos.Response;
using PCS.Entity.Dtos.ProductDtos.Request;
using PCS.Entity.Dtos.ProductDtos.Response;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IUploadService uploadService;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper, IUploadService uploadService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.uploadService = uploadService;
        }

        //Get products of category
        public async Task<IResult> GetProductsWithImagesByCategoryIdAsync(Guid categoryId)
        {
            var productsOfCategory = await unitOfWork.Products.GetListByExpression(x => x.CategoryId == categoryId,x => x.ProductImages)
                .AsNoTracking().ToListAsync();

            if (!productsOfCategory.Any())
            {
                return ResultGenerator.NoContent(ErrorMessages.E10);
            }

            var productDefaultDtos = mapper.Map<IEnumerable<ProductDefaultDto>>(productsOfCategory);
            return ResultGenerator.Ok(productDefaultDtos);
        }

        //Get necessary data for product creation
        public async Task<IResult> GetNecessaryDataForProductCreationAsync()
        {
            var allCategories = await unitOfWork.Categories.GetAllAsync();
            var allBrands = await unitOfWork.Brands.GetAllAsync();
            var allColors = await unitOfWork.Colors.GetAllAsync();

            if (!allCategories.Any() || !allBrands.Any() || !allColors.Any())
            {
                throw new ConflictException(ExceptionMessages.P1());
            }

            var categoryDefaultDtos = mapper.Map<IEnumerable<CategoryDefaultDto>>(allCategories);
            var brandDefaultDtos = mapper.Map<IEnumerable<BrandDefaultDto>>(allBrands);
            var colorDefaultDtos = mapper.Map<IEnumerable<ColorDefaultDto>>(allColors);

            return ResultGenerator.Ok(new ProductCreationNecessariesDto { Categories = categoryDefaultDtos, Brands = brandDefaultDtos, Colors = colorDefaultDtos });
        }

        //Add new product
        public async Task<IResult> AddProductAsync(ProductCreateDto productCreateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            if (!productCreateDto.ImageFiles.Any())
            {
                return ResultGenerator.BadRequest(ErrorMessages.E12);
            }

            var doesCategoryExist = await unitOfWork.Categories.AnyAsync(x => x.Id == productCreateDto.CategoryId);
            if (!doesCategoryExist)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E13);
            }

            if (productCreateDto.BrandId is not null)
            {
                var doesBrandExist = await unitOfWork.Brands.AnyAsync(x => x.Id == productCreateDto.BrandId);
                if (!doesBrandExist)
                {
                    return ResultGenerator.BadRequest(ErrorMessages.E13);
                }
            }

            if (productCreateDto.ColorId is not null)
            {
                var doesColorExist = await unitOfWork.Colors.AnyAsync(x => x.Id == productCreateDto.ColorId);
                if (!doesColorExist)
                {
                    return ResultGenerator.BadRequest(ErrorMessages.E13);
                }
            }

            var imagePaths = await uploadService.UploadImagesAsync(productCreateDto.ImageFiles);

            if (!imagePaths.Any() || imagePaths is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E13);
            }

            var newProduct = mapper.Map<Product>(productCreateDto);
            newProduct.ProductOwnerId = checkableEntityHelper.MainUserId;
            checkableEntityHelper.AddMainUserAsCreator(newProduct);
            newProduct.ProductImages = imagePaths.Select(x => new ProductImage { ImageUrl = x, CreatedById = checkableEntityHelper.MainUserId.ToString(), CreatedBy = checkableEntityHelper.MainUserFullname }).ToList();
            unitOfWork.Products.Add(newProduct);
            await unitOfWork.SaveAsync();
            var productDefaultDto = mapper.Map<ProductDefaultDto>(newProduct);
            return ResultGenerator.Created(productDefaultDto);
        }

        //Update product
        public async Task<IResult> UpdateProductAsync(ProductUpdateDto productUpdateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingProduct = await unitOfWork.Products.GetByIdAsync(productUpdateDto.Id, x => x.ProductImages);

            if (existingProduct is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            var doesCategoryExist = await unitOfWork.Categories.AnyAsync(x => x.Id == productUpdateDto.CategoryId);
            if (!doesCategoryExist)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E13);
            }

            if (productUpdateDto.BrandId is not null)
            {
                var doesBrandExist = await unitOfWork.Brands.AnyAsync(x => x.Id == productUpdateDto.BrandId);
                if (!doesBrandExist)
                {
                    return ResultGenerator.BadRequest(ErrorMessages.E13);
                }
            }

            if (productUpdateDto.ColorId is not null)
            {
                var doesColorExist = await unitOfWork.Colors.AnyAsync(x => x.Id == productUpdateDto.ColorId);
                if (!doesColorExist)
                {
                    return ResultGenerator.BadRequest(ErrorMessages.E13);
                }
            }

            mapper.Map(productUpdateDto, existingProduct);
            checkableEntityHelper.AddMainUserAsUpdater(existingProduct);
            unitOfWork.Products.Update(existingProduct);
            await unitOfWork.SaveAsync();
            var updatedProductDefaultDto = mapper.Map<ProductDefaultDto>(existingProduct);
            return ResultGenerator.Ok(updatedProductDefaultDto);
        }

        //Delete product
        public async Task<IResult> DeleteProductAsync(Guid productId, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingProduct = await unitOfWork.Products.GetByIdAsync(productId);

            if (existingProduct is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            checkableEntityHelper.AddMainUserAsUpdater(existingProduct);
            unitOfWork.Products.Delete(existingProduct);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }
    }
}
