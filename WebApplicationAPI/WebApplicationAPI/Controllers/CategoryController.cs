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
        private readonly IBaseService<Category> CategoryService;
        private readonly IMapper Mapper;
            public CategoryController(IBaseService<Category> CategoryService, IMapper Mapper)
        {
            this.CategoryService = CategoryService;
            this.Mapper = Mapper;
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] CategoryAdd Model)
        {
            if (ModelState.IsValid)
            {
                  return Ok(CategoryService.Add(Mapper.Map<Category>(Model)));
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
            foreach(var item in CategoryService.List(c => c.Name.Contains(SearchValue), Page, PageSize))
            {
                list.Add(Mapper.Map<CategoryView>(item));
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int Id)
        {
            var Category = CategoryService.FindOne(c => c.Id == Id);

             return Ok(Mapper.Map<CategoryView>(Category));   
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int[] Id)
        {
            foreach(var item in Id)
            {
                var Category = CategoryService.FindOne(c => c.Id == item);
                if (Category != null)
                {
                    CategoryService.Delete(Category);
                }
            }
            return Ok("Delete record successfully");
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] CategoryView Model)
        {
            
            if (CategoryService.FindOne(c => c.Id == Model.Id) != null)
            {
                return Ok(CategoryService.Update(Mapper.Map<Category>(Model)));
            }
            else return NotFound("No find record");
        }
    }
}
