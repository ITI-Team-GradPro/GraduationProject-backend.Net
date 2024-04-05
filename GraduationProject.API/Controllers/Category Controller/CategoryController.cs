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
                return BadRequest(ModelState);
            try
            {
                var exixstingcategory = await _categoryManager.GetByName(category.Name);
                var categories = await _categoryManager.GetAll();

                if(exixstingcategory is not null && categories.Any(d=>d.Name == category.Name))
                {
                    return Conflict(new GeneralResponse {StatusCode = "Conflict" , Message ="Category already exists!"});
                }

                var NewCategory = await _categoryManager.Add(category);
                return Ok(new GeneralResponse { StatusCode = "Success" , Message="Category added successfully"});
            }
            catch (Exception)
            {
                return BadRequest(new GeneralResponse { StatusCode ="Error" , Message="Can't add category!"});
            }
        }

        [HttpGet]
        public async Task <ActionResult<List<CategoryReadDto>>> GetAll()
        {
            try
            {
                var categories = await _categoryManager.GetAll()/*.ToList()*/;
                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest(new GeneralResponse { StatusCode="Error" , Message= "Can't get all categories" });
            }

        }

        [HttpGet("{name}")]
        public async Task<ActionResult<CategoryReadDto>> GetByName(string name)
        {
            CategoryReadDto? category = await _categoryManager.GetByName(name);
            try
            {
                if (category is null)
                {
                    return NotFound(new GeneralResponse { StatusCode="Error" , Message= " no category found with this name" });
                }
                return Ok(category);
            }
            catch (Exception)
            {
                 return NotFound(new GeneralResponse { StatusCode = "Error", Message = "Can't find category!" });
            }

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var IsFound = await _categoryManager.Delete(id);
            try { 
            if (!IsFound)
            {
                return NotFound(new GeneralResponse { StatusCode="Error" , Message="Category not found"});
            }
            
                return Ok(new GeneralResponse { StatusCode="Success" , Message="Category deleted successfully"});
            }
            catch (Exception)
            {
                return NotFound(new GeneralResponse { StatusCode = "Error", Message = "Can't delete category!" });
            }

        }


    }
}
