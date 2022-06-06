using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using DomainLayer.ViewModel.ProductMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly IBaseService<Product> ProductService;
        private readonly IMapper mapper;
        public ProductController(IBaseService<Product> ProductService, IMapper mapper)
        {
            this.ProductService= ProductService;
            this.mapper= mapper;
        }
        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ProductAdd model)
        {
            //var temp = _mapper.Map<Category>(model);
            if (ModelState.IsValid)
            {
                //CategoryService.Add(model);
                //return Ok(model);
                return Ok(ProductService.Add(mapper.Map<Product>(model)));
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("Pagination")]
        public IActionResult Paging(int page, string? searchValue = "", int pageSize = 0)
        {
            if (pageSize == 0)
                pageSize = 10;
            if (page <= 0)
            {
                page = 1;
            }
            List<ProductView> list = new List<ProductView>();
            var temp = ProductService.List(c => c.Name.Contains(searchValue), page, pageSize, "Category");
            foreach(var item in temp)
            {
                list.Add(mapper.Map<ProductView>(item));
            }
            return Ok(list);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int Id)
        {
            var temp = ProductService.FindOne(c => c.Id == Id, "Category");
            return Ok(mapper.Map<ProductView>(temp));
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int[] id)
        {
            foreach (var item in id)
            {
                var temp = ProductService.FindOne(c => c.Id == item);
                if (temp != null)
                {
                    ProductService.Delete(temp);
                }
            }
            return Ok();
        }
        [HttpPut]
        [Route("Update")]
        public  IActionResult Update([FromBody] ProductUpdate model)
        {

            if (ProductService.FindOne(c => c.Id == model.Id) != null)
            {
                return Ok(ProductService.Update(mapper.Map<Product>(model)));
            }
            else return NotFound("No find record");
        }
    }
}
