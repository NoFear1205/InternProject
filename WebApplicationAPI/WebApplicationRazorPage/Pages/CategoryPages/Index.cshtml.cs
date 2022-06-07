using DomainLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.Interfaces;

namespace WebApplicationRazorPage.Pages.CategoryPages
{
    public class IndexModel : PageModel
    {
        private readonly IBaseService<Category> baseService;
        public IndexModel(IBaseService<Category> baseService)
        {
            this.baseService = baseService;
        }
        public List<Category> list { get; set; }
        public void OnGet()
        {
            list = baseService.Get();
        }
        public IActionResult OnPost(string searchValue)
        {
            if (searchValue == null)
                searchValue = "";
            list = baseService.List(c => c.Name.Contains(searchValue), 1, 10);
            return Page();
        }
    }
}
