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
        private readonly IMapper Mapper;
        public ProductController(IBaseService<Product> ProductService, IMapper Mapper)
        {
            this.ProductService= ProductService;
            this.Mapper= Mapper;
        }
        [HttpPost]
        public IActionResult Add([FromBody] ProductAdd Model)
        {
            
            if (ModelState.IsValid)
            {
                return Ok(ProductService.Add(Mapper.Map<Product>(Model)));
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
            var temp = ProductService.List(c => c.Name.Contains(SearchValue), Page, PageSize, "Category");
            foreach(var item in temp)
            {
                ListProduct.Add(Mapper.Map<ProductView>(item));
            }
            return Ok(ListProduct);
        }
        [HttpGet]
        [Route("Get-one")]
        public IActionResult GetOne(int Id)
        {
            var Product = ProductService.FindOne(c => c.Id == Id, "Category");
            return Ok(Mapper.Map<ProductView>(Product));
        }
        [HttpDelete]
        [Route("Delete")]
        public IActionResult Delete([FromBody] int[] id)
        {
            foreach (var item in id)
            {
                var Product = ProductService.FindOne(c => c.Id == item);
                if (Product != null)
                {
                    ProductService.Delete(Product);
                }
            }
            return Ok();
        }
        [HttpPut]
        [Route("Update")]
        public  IActionResult Update([FromBody] ProductUpdate Model)
        {

            if (ProductService.FindOne(c => c.Id == Model.Id) != null)
            {
                return Ok(ProductService.Update(Mapper.Map<Product>(Model)));
            }
            else return NotFound("No find record");
        }
    }
}
