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
        private readonly IBaseService<Role> baseService;
        private readonly IMapper mapper;
        public CreateModel(IBaseService<Role> baseService, IMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }
        public void OnGet()
        {
            
        }

        public IActionResult OnPost(CategoryAddModel model) 
        {
            baseService.Add(mapper.Map<Role>(model));
            return RedirectToPage("Index");
        }
    }
}
