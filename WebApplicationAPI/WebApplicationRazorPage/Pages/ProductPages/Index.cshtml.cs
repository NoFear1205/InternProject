using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.ProductPages
{
    public class IndexModel : PageModel
    {
        private readonly IBaseService<Product> baseService;
        private readonly IMapper mapper;
        public IndexModel(IBaseService<Product> baseService, IMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }
        public List<Product> list { get; set; }
        public void OnGet()
        {
         
            list = baseService.List(c=>c.Name.Contains(""),1,100,"Category");
        }
        public IActionResult OnPost(string searchValue)
        {
            if (searchValue == null)
                searchValue = "";
            list = baseService.List(c => c.Name.Contains(searchValue), 1, 100, "Category");
            return Page();
        }
    }
}
