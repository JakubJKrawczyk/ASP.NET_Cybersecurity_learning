using API.Repositories;
using Authentication;
using Entities.DataModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController : Controller
    {
        public RoleRepository _roleRepository { get; set; }
        public UserRepository _userRepository { get; set; }
        public RoleController()
        {
            _roleRepository = new RoleRepository();
            _userRepository = new UserRepository();
        }

        [HttpPost("role")]
        public async Task<IActionResult> AddRole([FromQuery] string RoleName, [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? user = await _userRepository.GetUserWithId((int)id);
                if (user is not null)
                {
                    if (SecuritySystem.CheckIfUserHasRole(user, "Administrator"))
                    {
                        await _roleRepository.AddRole(new Role() { RoleId = _roleRepository.GetRoles().Result.Max(i => i.RoleId) + 1, Name = RoleName });


                        return Ok();
                    }
                    else return StatusCode(StatusCodes.Status403Forbidden);
                }
                else return NotFound(id);
            }
            else return BadRequest();

        }
        [HttpPut("role")]
        public async Task<IActionResult> UpdateRole([FromQuery] string rolename, [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? user = await _userRepository.GetUserWithId((int)id);
                if (user is not null)
                {
                    if (SecuritySystem.CheckIfUserHasRole(user, "Administrator"))
                    {


                        Role? role = await _roleRepository.GetRoleWithName(rolename);
                        if (role is not null)
                        {
                            await _roleRepository.UpdateRole(role);
                        }
                        else return NotFound(rolename);




                        return Ok();
                    }
                    else return StatusCode(StatusCodes.Status403Forbidden);
                }
                else return NotFound(id);
            }
            else return BadRequest();
        }

        [HttpDelete("role")]
        public async Task<IActionResult> DeleteRole([FromQuery] string rolename, [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? user = await _userRepository.GetUserWithId((int)id);
                if (user is not null)
                {
                    if (SecuritySystem.CheckIfUserHasRole(user, "Administrator"))
                    {


                        return Ok();
                    }
                    else return StatusCode(StatusCodes.Status403Forbidden);
                }
                else return NotFound(id);
            }
            else return BadRequest();

        }
    }
}
