using Microsoft.AspNetCore.Mvc;
using ECommerce.Application.DTOs;
using ECommerce.Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.Logging;
using AutoMapper;
using ECommerce.Application.Models;

namespace ECommerce.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryApplicationService _categoryService;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(
            ICategoryApplicationService categoryService,
            IValidator<CreateCategoryDto> createValidator,
            IMapper mapper,
            ILogger<CategoriesController> logger)
        {
            _categoryService = categoryService;
            _createValidator = createValidator;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetCategory(string id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            if (category == null)
                return NotFound();

            return Ok(category);
        }

        [HttpGet("{id}/subcategories")]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetSubcategories(string id)
        {
            var subcategories = await _categoryService.GetSubcategoriesAsync(id);
            return Ok(subcategories);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> CreateCategory([FromForm] CreateCategoryFormModel model)
        {
            var categoryDto = _mapper.Map<CreateCategoryDto>(model);
            var validationResult = await _createValidator.ValidateAsync(categoryDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var createdCategory = await _categoryService.CreateCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.Id }, createdCategory);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> UpdateCategory(string id, [FromForm] UpdateCategoryFormModel model)
        {
            try
            {
                var categoryDto = _mapper.Map<UpdateCategoryDto>(model);
                var updatedCategory = await _categoryService.UpdateCategoryAsync(id, categoryDto);
                if (updatedCategory == null)
                    return NotFound();

                return Ok(updatedCategory);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating category with ID {CategoryId}", id);
                return StatusCode(500, "An error occurred while updating the category");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(string id)
        {
            var result = await _categoryService.DeleteCategoryAsync(id);
            if (!result)
                return NotFound();

            return NoContent();
        }
    }
} 