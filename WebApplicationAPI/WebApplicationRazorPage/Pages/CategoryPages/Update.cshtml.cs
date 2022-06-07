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
        private readonly IBaseService<Category> baseService;
        private readonly IMapper mapper;
        public Category category;
        public UpdateModel(IBaseService<Category> baseService, IMapper mapper)
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
            baseService.Update(mapper.Map<Category>(model));
            return RedirectToPage("Index");
        }
    }
}
