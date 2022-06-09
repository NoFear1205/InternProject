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
        private readonly IBaseService<Product> productService;
        private readonly IMapper mapper;
        public ProductController(IBaseService<Product> productService, IMapper mapper)
        {
            this.productService= productService;
            this.mapper= mapper;
        }
        [HttpPost]
        public IActionResult Add([FromBody] ProductAddModel model)
        {
            
            if (ModelState.IsValid)
            {
                return Ok(productService.Add(mapper.Map<Product>(model)));
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("Pagination")]
        public IActionResult Paging(int page, string? searchValue = "", int pageSize = 0)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (page <= 0)
            {
                page = 1;
            }
            List<ProductView> ListProduct = new List<ProductView>();
            var temp = productService.List(c => c.Name.Contains(searchValue), page, pageSize, "Category");
            foreach(var item in temp)
            {
                ListProduct.Add(mapper.Map<ProductView>(item));
            }
            return Ok(ListProduct);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int id)
        {
            var Product = productService.FindOne(c => c.Id == id, "Category");
            return Ok(mapper.Map<ProductView>(Product));
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int[] id)
        {
            foreach (var item in id)
            {
                var Product = productService.FindOne(c => c.Id == item);
                if (Product != null)
                {
                    productService.Delete(Product);
                }
            }
            return Ok();
        }
        [HttpPut]
        [Route("Update")]
        public  IActionResult Update([FromBody] ProductUpdateModel model)
        {

            if (productService.FindOne(c => c.Id == model.Id) != null)
            {
                return Ok(productService.Update(mapper.Map<Product>(model)));
            }
            else return NotFound("No find record");
        }
    }
}
