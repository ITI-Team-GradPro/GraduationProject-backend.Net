using GraduationProject.BL.Dtos;
using GraduationProject.BL.Managers;
using GraduationProject.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GraduationProject.API.Controllers.Category_Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpPost]
        public async Task<ActionResult> Add(CategoryAddDto category)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var NewCategory = await _categoryManager.Add(category);
            return Ok(NewCategory);
                  
        }

        [HttpGet]
        public async Task <ActionResult<List<CategoryReadDto>>> GetAll()
        {
            var categories = await _categoryManager.GetAll()/*.ToList()*/;
            return Ok(categories);
        }

        //[HttpGet("{name:string}")]
        //public async Task< ActionResult<CategoryReadDto>> GetByName(string name)
        //{
        //    CategoryReadDto? category = _categoryManager.GetByName(name);
        //    if (category is null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(category);
        //}

        [HttpGet("{name}")]
        public async Task<ActionResult<CategoryReadDto>> GetByName(string name)
        {
            CategoryReadDto? category = await _categoryManager.GetByName(name);
            if (category is null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var IsFound = await _categoryManager.Delete(id);
            if (!IsFound)
            {
                return NotFound();
            }

            return Ok("Category Removed Successfully");
        }


    }
}
