using DomainLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer.Interfaces;

namespace WebApplicationAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class RoleController : Controller
    {
        private readonly IBaseService<Role> baseService;
        public RoleController(IBaseService<Role> baseService)
        {
            this.baseService = baseService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(baseService.Get());
        }
        [HttpPost]
        public IActionResult Index(Role Model)
        {

            return Ok(baseService.Add(Model));
        }
        [HttpGet]
        [Route("Paging")]
        public IActionResult ListOfRole(int Page,string SearchValue)
        {
            int PageSize = 10;
            return Ok(baseService.List(c => c.Name.Contains(SearchValue), Page, PageSize));           
        }
    }
}
