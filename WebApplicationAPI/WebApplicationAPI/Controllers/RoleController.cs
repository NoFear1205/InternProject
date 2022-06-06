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
        private readonly IBaseService<Role> role;
        public RoleController(IBaseService<Role> role)
        {
            this.role = role;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return Ok(role.Get());
        }
        [HttpPost]
        public IActionResult Index(Role model)
        {

            return Ok(role.Add(model));
        }
        [HttpGet]
        [Route("Paging")]
        public IActionResult ListOfRole(int page,string searchValue)
        {
            int pageSize = 10;
            return Ok(role.List(c => c.Name.Contains(searchValue), page, pageSize));           
        }
    }
}
