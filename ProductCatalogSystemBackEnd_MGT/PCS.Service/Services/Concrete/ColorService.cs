using AutoMapper;
using Microsoft.Extensions.Caching.Distributed;
using PCS.Core.Extensions;
using PCS.Core.ResultTypes.Abstract;
using PCS.Core.Utils.Abstract;
using PCS.Core.Utils.Concrete;
using PCS.Entity.Dtos.ColorDtos.Request;
using PCS.Entity.Dtos.ColorDtos.Response;
using PCS.Entity.Models;
using PCS.Repository.UnitOfWork.Abstract;
using PCS.Service.Services.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Service.Services.Concrete
{
    public class ColorService : IColorService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IDistributedCache distributedCache;
        public ColorService(IUnitOfWork unitOfWork, IMapper mapper, IDistributedCache distributedCache)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.distributedCache = distributedCache;
        }

        public async Task<IResult> GetAllColorsAsync()
        {
            var cachedAllColors = await distributedCache.GetDataAsync<IEnumerable<Color>>(CacheKeys.AllColors);
            if (cachedAllColors is not null)
            {
                var cachedColorDefaultDtos = mapper.Map<IEnumerable<ColorDefaultDto>>(cachedAllColors);
                return ResultGenerator.Ok("From Cache.", cachedColorDefaultDtos);
            }

            var allColors = await unitOfWork.Colors.GetAllAsync();
            if (!allColors.Any())
            {
                return ResultGenerator.NoContent();
            }

            await distributedCache.SetDataAsync(CacheKeys.AllColors, allColors, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(20));
            var colorDefaultDtos = mapper.Map<IEnumerable<ColorDefaultDto>>(allColors);
            return ResultGenerator.Ok(colorDefaultDtos);
        }

        public async Task<IResult> AddColorAsync(ColorCreateDto colorCreateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var doesColorExist = await unitOfWork.Colors.AnyAsync(x => x.ColorName == colorCreateDto.ColorName);
            if (doesColorExist)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E7);
            }

            var newColor = mapper.Map<Color>(colorCreateDto);
            checkableEntityHelper.AddMainUserAsCreator(newColor);
            unitOfWork.Colors.Add(newColor);
            await unitOfWork.SaveAsync();
            var newColorDefaultDto = mapper.Map<ColorDefaultDto>(newColor);
            return ResultGenerator.Created(newColorDefaultDto);
        }

        public async Task<IResult> UpdateColorAsync(ColorUpdateDto colorUpdateDto, ICheckableEntityHelper checkableEntityHelper)
        {
            var existingColor = await unitOfWork.Colors.GetByIdAsync(colorUpdateDto.Id);
            if (existingColor is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            mapper.Map(colorUpdateDto, existingColor);
            checkableEntityHelper.AddMainUserAsUpdater(existingColor);
            unitOfWork.Colors.Update(existingColor);
            await unitOfWork.SaveAsync();
            var updatedColorDefaultDto = mapper.Map<ColorDefaultDto>(existingColor);
            return ResultGenerator.Ok(updatedColorDefaultDto);
        }

        public async Task<IResult> DeleteColorAsync(int colorId)
        {
            var existingColor = await unitOfWork.Colors.GetByIdAsync(colorId, x => x.Products);

            if (existingColor is null)
            {
                return ResultGenerator.BadRequest(ErrorMessages.E8);
            }

            if (existingColor.Products.Any(x => !x.IsDeleted))
            {
                return ResultGenerator.BadRequest(ErrorMessages.E9);
            }

            unitOfWork.Colors.Delete(existingColor);
            await unitOfWork.SaveAsync();
            return ResultGenerator.Ok();
        }
    }
}
