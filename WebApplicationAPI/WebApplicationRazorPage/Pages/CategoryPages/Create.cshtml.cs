using AutoMapper;
using DomainLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.CategoryPages
{

    public class CreateModel : PageModel
    {
        private readonly IBaseService<Category> baseService;
        private readonly IMapper mapper;
        public CreateModel(IBaseService<Category> baseService, IMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }
        public void OnGet()
        {
            
        }

        public IActionResult OnPost(CategoryAddModel model) 
        {
            baseService.Add(mapper.Map<Category>(model));
            return RedirectToPage("Index");
        }
    }
}
