using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly IBaseService<Category> CategoryService;
        private readonly IMapper mapper;
        public CategoryController(IBaseService<Category> CategoryService, IMapper mapper)
        {
            this.CategoryService = CategoryService;
            this.mapper = mapper;
        }
        [HttpPost]
        public IActionResult Add([FromBody] CategoryAdd model)
        {
            if (ModelState.IsValid)
            {
                  return Ok(CategoryService.Add(mapper.Map<Category>(model)));
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
            foreach(var item in CategoryService.List(c => c.Name.Contains(searchValue), page, pageSize))
            {
                list.Add(mapper.Map<CategoryView>(item));
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int Id)
        {
            var temp = CategoryService.FindOne(c => c.ID == Id);

             return Ok(mapper.Map<CategoryView>(temp));   
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int[] id)
        {
            foreach(var item in id)
            {
                var temp = CategoryService.FindOne(c => c.ID == item);
                if (temp != null)
                {
                    CategoryService.Delete(temp);
                }
            }
            return Ok("Delete record successfully");
        }
        [HttpPut]
        [Route("Update")]
        public IActionResult Update([FromBody] CategoryView model)
        {
            
            if (CategoryService.FindOne(c => c.ID == model.Id) != null)
            {
                return Ok(CategoryService.Update(mapper.Map<Category>(model)));
            }
            else return NotFound("No find record");
        }
    }
}
