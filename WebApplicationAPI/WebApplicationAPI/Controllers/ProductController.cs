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
        public ProductController(IBaseService<Product> ProductService, IMapper Mapper)
        {
            this.productService= ProductService;
            this.mapper= Mapper;
        }
        [HttpPost]
        public IActionResult Add([FromBody] ProductAddModel Model)
        {
            
            if (ModelState.IsValid)
            {
                return Ok(productService.Add(mapper.Map<Product>(Model)));
            }
            return BadRequest(ModelState);
        }
        [HttpGet]
        [Route("Pagination")]
        public IActionResult Paging(int Page, string? SearchValue = "", int PageSize = 0)
        {
            if (PageSize <= 0)
                PageSize = 10;
            if (Page <= 0)
            {
                Page = 1;
            }
            List<ProductView> ListProduct = new List<ProductView>();
            var temp = productService.List(c => c.Name.Contains(SearchValue), Page, PageSize, "Category");
            foreach(var item in temp)
            {
                ListProduct.Add(mapper.Map<ProductView>(item));
            }
            return Ok(ListProduct);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int Id)
        {
            var Product = productService.FindOne(c => c.Id == Id, "Category");
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
        public  IActionResult Update([FromBody] ProductUpdateModel Model)
        {

            if (productService.FindOne(c => c.Id == Model.Id) != null)
            {
                return Ok(productService.Update(mapper.Map<Product>(Model)));
            }
            else return NotFound("No find record");
        }
    }
}
