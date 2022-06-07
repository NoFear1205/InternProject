using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.ProductMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.ProductPages
{
   [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly IBaseService<Product> baseService;
        private readonly IMapper mapper;
        public ProductAdd model { get; set; }
        public CreateModel(IBaseService<Product> baseService, IMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost(ProductAdd model) 
        {
            baseService.Add(mapper.Map<Product>(model));
            return RedirectToPage("Index");
        }
    }
}
