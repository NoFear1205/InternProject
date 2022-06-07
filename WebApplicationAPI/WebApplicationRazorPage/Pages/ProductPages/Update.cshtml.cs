using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using DomainLayer.ViewModel.ProductMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.ProductPages
{
    [BindProperties]
    public class UpdateModel : PageModel
    {
        private readonly IBaseService<Product> baseService;
        private readonly IMapper mapper;
        public ProductUpdate product;
        public UpdateModel(IBaseService<Product> baseService, IMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }
        public void OnGet(int id)
        {
            product = mapper.Map<ProductUpdate>(baseService.FindOne(c=>c.Id==id));
        }
        public IActionResult OnPost(ProductUpdate model)
        {
            baseService.Update(mapper.Map<Product>(model));
            return RedirectToPage("Index");
        }
    }
}
