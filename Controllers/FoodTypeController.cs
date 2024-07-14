using AutoMapper;
using FluentValidation;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ValidToken]
    [ApiController]
    public class FoodTypeController(IFoodTypeService foodTypeService, IMapper mapper) : ControllerBase
    {
        private readonly IFoodTypeService _foodTypeService = foodTypeService;
        private readonly IMapper _mapper = mapper;

        [HttpGet("get-list")]
        [CanAccess(Permissions = [nameof(Constants.Permissions.ADM004)])]
        public IActionResult GetList()
        {
            return Ok(_foodTypeService.GetAll());
        }

        [HttpPost("create")]
        [CanAccess(Permissions = [nameof(Constants.Permissions.ADM005)])]
        public async Task<IActionResult> CreateFoodType([FromBody] StoreUpdateFoodTypeRequest request, IValidator<StoreUpdateFoodTypeRequest> validator, CancellationToken cancellationToken)
        {
            var validated = await validator.ValidateAsync(request, cancellationToken);
            if (!validated.IsValid)
            {
                StatusResponse response = new()
                {
                    Success = false,
                    Messages = validated.Errors.Select(x => x.ErrorMessage).ToList()
                };
                return BadRequest(response);
            }
            else
            {
                FoodType foodType = _mapper.Map<FoodType>(request);
                return Ok(_foodTypeService.Create(foodType));
            }
        }

        [HttpPost("update/{id}")]
        [CanAccess(Permissions = [nameof(Constants.Permissions.ADM005)])]
        public async Task<IActionResult> UpdateFoodType(int id, StoreUpdateFoodTypeRequest request, IValidator<StoreUpdateFoodTypeRequest> validator, CancellationToken cancellationToken)
        {
            var validated = await validator.ValidateAsync(request, cancellationToken);
            if (!validated.IsValid)
            {
                StatusResponse response = new()
                {
                    Success = false,
                    Messages = validated.Errors.Select(x => x.ErrorMessage).ToList()
                };
                return BadRequest(response);
            }
            FoodType foodType = _mapper.Map<FoodType>(request);
            foodType.Id = id;

            return Ok(_foodTypeService.Update(foodType));
        }

        [HttpPost("delete/{id}")]
        [CanAccess(Permissions = [nameof(Constants.Permissions.ADM005)])]
        public IActionResult DeleteFoodType(int id)
        {
            return Ok(_foodTypeService.Delete(id));
        }
    }
}
