using API.Repositories;
using Entities.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        public RoleRepository _roleRepository { get; set; }
        public RoleController()
        {
                _roleRepository = new RoleRepository();
        }

        [HttpPost("role")]
        public async Task<IActionResult> AddRole([FromQuery] string RoleName)
        {
            
                await _roleRepository.AddRole(new Role() { Name = RoleName });

            
            return Ok();
        }
    }
}
