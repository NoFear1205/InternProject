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
        private readonly IBaseService<Role> Role;
        public RoleController(IBaseService<Role> Role)
        {
            this.Role = Role;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(Role.Get());
        }
        [HttpPost]
        public IActionResult Index(Role Model)
        {

            return Ok(Role.Add(Model));
        }
        [HttpGet]
        [Route("Paging")]
        public IActionResult ListOfRole(int Page,string SearchValue)
        {
            int PageSize = 10;
            return Ok(Role.List(c => c.Name.Contains(SearchValue), Page, PageSize));           
        }
    }
}
