using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IBaseService<Category> categoryService;
        private readonly IMapper mapper;
            public CategoryController(IBaseService<Category> CategoryService, IMapper Mapper)
        {
            this.categoryService = CategoryService;
            this.mapper = Mapper;
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] CategoryAdd Model)
        {
            if (ModelState.IsValid)
            {
                  return Ok(categoryService.Add(mapper.Map<Category>(Model)));
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("Pagination")]
        public IActionResult Paging(int Page, string? SearchValue = "", int PageSize = 0)
        {
            if(PageSize == 0)
                PageSize = 10;
            if(Page <= 0)
            {
                Page = 1;
            }
            List<CategoryView> list = new List<CategoryView>();
            foreach(var item in categoryService.List(c => c.Name.Contains(SearchValue), Page, PageSize))
            {
                list.Add(mapper.Map<CategoryView>(item));
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int Id)
        {
            var Category = categoryService.FindOne(c => c.Id == Id);

             return Ok(mapper.Map<CategoryView>(Category));   
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int[] Id)
        {
            foreach(var item in Id)
            {
                var Category = categoryService.FindOne(c => c.Id == item);
                if (Category != null)
                {
                    categoryService.Delete(Category);
                }
            }
            return Ok("Delete record successfully");
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] CategoryView Model)
        {
            
            if (categoryService.FindOne(c => c.Id == Model.Id) != null)
            {
                return Ok(categoryService.Update(mapper.Map<Category>(Model)));
            }
            else return NotFound("No find record");
        }
    }
}
