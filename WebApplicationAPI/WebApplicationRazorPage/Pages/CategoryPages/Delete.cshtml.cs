using DomainLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.CategoryPages
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly IBaseService<Category> baseService;
        public Category category;
        public DeleteModel(IBaseService<Category> baseService)
        {
            this.baseService = baseService;
        }
        public void OnGet(int Id)
        {
            category = baseService.FindOne(c=>c.ID==Id);
        }
        public IActionResult OnPost(int Id)
        {
            category = baseService.FindOne(c => c.ID == Id);
            baseService.Delete(category);
            return RedirectToPage("Index");
        }
    }
}
