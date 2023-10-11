using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using API.DataManager;
using Entities.DataModels;
using API.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly UserRepository _repo;
        private readonly RoleRepository _roleRepository;

        public UsersController()
        {
            _repo = new UserRepository();
            _roleRepository = new RoleRepository();
        }


        // GET: Users

        [HttpGet("users")]
        public IEnumerable<User> GetUsers() {
            return _repo.GetUsers().Result;
        }

        [HttpGet("user")]
        public User GetUser([FromQuery] string login)
        {
            int? id = _repo.GetUserIdByLogin(login).Result;
            User user;
            if (id != -1)
            {

                user = _repo.GetUserById((int)id).Result;
                
                return user;
            }
            else
            {
                return new User();
            }
            
        }
        [HttpPost("user")]
        public async Task<IActionResult> Adduser([FromQuery] string login, [FromQuery] string password) {
            await _repo.AdduserAsync(new User() { Login = login, Password = password });
            return Ok();
        }
        [HttpPut("user/{login}/groups")]
        public async Task<IActionResult> AddRoleToUser(string login, [FromQuery] string roleName)
        {
            int id = await _repo.GetUserIdByLogin(login);
            if(id != -1)
            {

                User user = await _repo.GetUserById(id);
                int roleid = await _roleRepository.GetRoleIdByName(roleName);
                if(roleid != -1)
                {
                    Role role = await _roleRepository.GetRoleById(roleid);
                    await _repo.AddRoleToUser(user, role);
                    return Ok();
                }
                else
                {
                    return NotFound(roleName);
                }
            }
            else
            {
                return NotFound(login);
            }
        }

        [HttpDelete("user")]
        public async Task<IActionResult> DeleteUser([FromQuery] string login)
        {
            int id = await _repo.GetUserIdByLogin(login);
            if(id != -1)
            {
                User user = await _repo.GetUserById(id);
                await _repo.Deleteuser(user);
                return Ok();
            }
            else
            {
                return NotFound(login);
            }

        }

    }
}
