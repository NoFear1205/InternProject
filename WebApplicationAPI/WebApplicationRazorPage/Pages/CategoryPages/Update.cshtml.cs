using AutoMapper;
using DomainLayer.Model;
using DomainLayer.ViewModel.CategoryView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.CategoryPages
{
    [BindProperties]
    public class UpdateModel : PageModel
    {
        private readonly IBaseService<Role> baseService;
        private readonly IMapper mapper;
        public Role category;
        public UpdateModel(IBaseService<Role> baseService, IMapper mapper)
        {
            this.baseService = baseService;
            this.mapper = mapper;
        }
        public void OnGet(int id)
        {
            category = baseService.FindOne(c=>c.Id==id);
        }
        public IActionResult OnPost(CategoryView model)
        {
            baseService.Update(mapper.Map<Role>(model));
            return RedirectToPage("Index");
        }
    }
}
