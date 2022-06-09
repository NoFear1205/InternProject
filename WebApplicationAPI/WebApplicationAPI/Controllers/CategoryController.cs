using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using DomainLayer.ViewModel.CategoryViewModel;
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
        private readonly IBaseService<Role> categoryService;
        private readonly IMapper mapper;
            public CategoryController(IBaseService<Role> categoryService, IMapper mapper)
        {
            this.categoryService = categoryService;
            this.mapper = mapper;
        }
        [HttpPost, Authorize(Roles = "Admin")]
        public IActionResult Add([FromBody] CategoryAddModel model)
        {
            if (ModelState.IsValid)
            {
                  return Ok(categoryService.Add(mapper.Map<Role>(model)));
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("Pagination")]
        public IActionResult Paging(int page, string? searchValue = "", int pageSize = 0)
        {
            if(pageSize == 0)
                pageSize = 10;
            if(page <= 0)
            {
                page = 1;
            }
            List<CategoryView> list = new List<CategoryView>();
            foreach(var item in categoryService.List(c => c.Name.Contains(searchValue), page, pageSize))
            {
                list.Add(mapper.Map<CategoryView>(item));
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int id)
        {
            var Category = categoryService.FindOne(c => c.Id == id);

             return Ok(mapper.Map<CategoryView>(Category));   
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int[] id)
        {
            foreach(var item in id)
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
        public IActionResult Update([FromBody] CategoryUpdateModel model)
        {
            var temp = categoryService.FindOne(c => c.Id == model.Id);
            if (temp != null)
            {
                return Ok(categoryService.Update(mapper.Map<Role>(model)));
            }
            else return NotFound("No find record");
        }
    }
}
