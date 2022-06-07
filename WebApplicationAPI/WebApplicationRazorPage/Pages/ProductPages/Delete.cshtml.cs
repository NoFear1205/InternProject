using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.ProductPages
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IBaseService<Product> baseService;
        private readonly IMapper mapper;
        public ProductView product;
        public DeleteModel(IBaseService<Product> baseService, IMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }
        public void OnGet(int Id)
        {
            product = mapper.Map<ProductView>(baseService.FindOne(c=>c.Id==Id,"Category"));
        }
        public IActionResult OnPost(int Id)
        {
            var Temp = baseService.FindOne(c => c.Id == Id);
            baseService.Delete(Temp);
            return RedirectToPage("Index");
        }
    }
}
