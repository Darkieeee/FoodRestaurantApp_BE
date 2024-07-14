using AutoMapper;
using FoodRestaurantApp_BE.Filters;
using FoodRestaurantApp_BE.Models.Databases;
using FoodRestaurantApp_BE.Models.DTOs;
using FoodRestaurantApp_BE.Services;
using FoodRestaurantApp_BE.Services.Abstracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodRestaurantApp_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ValidToken]
    public class FoodController(IFoodService foodService, IMapper mapper,
                                IFileService fileService) : ControllerBase
    {
        private readonly IFoodService _foodService = foodService;
        private readonly IMapper _mapper = mapper;
        private readonly IFileService _fileService = (ImageFileService)fileService;

        [HttpPost("create")]
        public async Task<IActionResult> CreateFood([FromForm] StoreUpdateFoodRequest request)
        {
            Food f = _mapper.Map<Food>(request);
            if (request.Image != null)
            {
                string mainDirectory = Environment.CurrentDirectory;
                string subDirectory = Path.Combine("Images", "Foods");
                string fileUploadedUri = await _fileService.UploadFile(Path.Combine(mainDirectory, subDirectory),
                                                                       request.Image);
                f.Image = fileUploadedUri;
            }
            OperationResult<Food> result = await _foodService.CreateAsync(f);
            if(result.Success)
            {
                return Ok(result);
            }
            else
            {
                if(f.Image != null && _fileService.CheckFileIfExists(f.Image))
                {
                    _fileService.DeleteFile(f.Image);
                }
                return BadRequest(result);
            }
        }

        [HttpPost("update/{id}")]
        public IActionResult UpdateFood(int id, [FromForm] StoreUpdateFoodRequest request)
        {
            return Ok();
        }

        [HttpGet("best-seller")]
        public IActionResult TakeTopSellingFood(int top)
        {
            return Ok(_foodService.TakeTopSellingFoods(top));
        }

        [HttpGet("detail")]
        public IActionResult GetDetail(string slug, int id)
        {
            return Ok(_foodService.GetBySlugAndId(slug, id));
        }

        [HttpGet("get-list")]
        public IActionResult GetList()
        {
            return Ok(_foodService.GetAll());
        }

        [HttpGet("get-pagination")]
        public IActionResult GetPagination(string? search, PageSizeOption option = PageSizeOption.TenPerPage, int page = 1) 
        {
            return Ok(_foodService.GetPagination(search, option, page));
        }

        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            return Ok(await _foodService.DeleteAsync(id));
        }
    }
}
