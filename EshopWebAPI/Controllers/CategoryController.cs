using AutoMapper;
using EshopWebAPI.Data.Interfaces;
using EshopWebAPI.Models;
using EshopWebAPI.Models.Dto;
using EshopWebAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EshopWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCategories()
        {
            var categories = _mapper.Map<List<CategoryDto>>(_categoryRepository.GetCategories());
            return Ok(categories);
        }

        [HttpGet("{categoryId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetCategory(int categoryId)
        {
            if (categoryId == 0)
            {
                return BadRequest();
            }

            var category = _mapper.Map<CategoryDto>(_categoryRepository.GetCategory(categoryId));

            if (category == null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpGet("product/{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetProductsByCategory(int categoryId)
        {
            if (categoryId == 0)
            {
                return BadRequest();
            }

            var products = _mapper.Map<List<ProductDto>>(_categoryRepository.GetProductByCategory(categoryId));

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult CreateCategory([FromBody] CategoryDto categoryCreate)
        {
            if (categoryCreate == null)
            {
                return BadRequest();
            }

            var category = _categoryRepository.GetCategories().Where(c => c.CategoryName.Trim().ToLower() == categoryCreate.CategoryName.TrimEnd().ToLower()).FirstOrDefault();

            if (category != null) 
            {
                ModelState.AddModelError("", "Category already exists !");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoryMap = _mapper.Map<Category>(categoryCreate);

            if (!_categoryRepository.CreateCategory(categoryMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok("Sucessfully created");
        }

        [HttpPut("{categoryId}")]
        public IActionResult UpdateCategory([FromBody] CategoryDto categoryDto, int categoryId)
        {
            if (categoryDto == null || categoryDto.Id != categoryId)
            {
                return BadRequest();
            }

            var category = _mapper.Map<Category>(categoryDto);

            var result = _categoryRepository.UpdateCategory(category);

            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok(category);
        }
    }
}
