﻿using API.Repositories;
using Authentication;
using Entities.DataModels;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using System.Security.AccessControl;

namespace API.Controllers
{
    [ApiController]
    [Route("users")]
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
        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? authUser = await _repo.GetUserWithId((int)id);
                Console.WriteLine(authUser.RoleId);
                if (authUser is not null)
                {
                    Role role = await _roleRepository.GetRoleWithId(authUser.RoleId);
                    if (SecuritySystem.CheckIfUserHasRole(role, "Administrator"))
                    {

                        return Ok(await _repo.GetUsers());
                    }
                    else return StatusCode(StatusCodes.Status403Forbidden);
                }
                else return NotFound();
            }
            else return BadRequest();

        }

        [HttpGet("user/{login}")]
        public async Task<IActionResult> GetUser([FromRoute] string login, [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? authenticateUser = await _repo.GetUserWithId((int)id);
                if (authenticateUser is not null)
                {
                    if (authenticateUser.Login == login)
                    {
                        User? user = await _repo.GetUserWithLogin(login);
                        if (user is not null)
                        {
                            return Ok(user);
                        }
                        else
                        {
                            return NotFound(login);

                        }
                    }
                    else
                    {
                        Role role = await _roleRepository.GetRoleWithId(authenticateUser.RoleId);
                        if (SecuritySystem.CheckIfUserHasRole(role, "Administrator"))
                        {

                            User? user = await _repo.GetUserWithLogin(login);
                            if (user is not null)
                            {
                                return Ok(user);
                            }
                            else
                            {
                                return NotFound(login);

                            }
                        }
                        else return StatusCode(StatusCodes.Status403Forbidden);
                    }
                   
                }
                else return NotFound(id);
            }
            else return BadRequest();

        }


        [HttpPost("user")]
        public async Task<IActionResult> Adduser([FromQuery] string login, [FromQuery] string password, [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? authUser = await _repo.GetUserWithId((int)id);
                if (authUser is not null)
                {
                    Role role = await _roleRepository.GetRoleWithId(authUser.RoleId);
                    if (SecuritySystem.CheckIfUserHasRole(role, "Administrator"))
                    {

                        await _repo.AdduserAsync(new User() { Login = login, Password = password , RoleId = 2, FirstLogin = true, PasswordExpirationDate = DateTime.Now + TimeSpan.FromDays(30)});
                        return Ok();

                    }
                    else return StatusCode(StatusCodes.Status403Forbidden);
                }
                else return NotFound(id);
            }
            else return BadRequest();

        }
        [HttpPut("user")]
        public async Task<IActionResult> UpdateUser([FromQuery] string login, [FromQuery] string password, [FromQuery] bool firstLogin, [FromQuery] DateTime expirationDate,  [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? authUser = await _repo.GetUserWithId((int)id);
                if (authUser is not null)
                {
                    Role role = await _roleRepository.GetRoleWithId(authUser.RoleId);
                    if (SecuritySystem.CheckIfUserHasRole(role, "Administrator") || SecuritySystem.CheckIfUserHasRole(role, "User"))
                    {
                        User newUser = await _repo.GetUserWithLogin(login);
                        newUser.Login = login;
                        newUser.Password = password;
                        newUser.FirstLogin = firstLogin;
                        newUser.PasswordExpirationDate = expirationDate;
                        await _repo.Updateuser(newUser);
                        return Ok();

                    }
                    else return StatusCode(StatusCodes.Status403Forbidden);
                }
                else return NotFound(id);
            }
            else return BadRequest();

        }

        [HttpPut("user/{login}/groups")]
        public async Task<IActionResult> AddRoleToUser(string login, [FromQuery] string roleName, [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? requestUser = await _repo.GetUserWithId((int)id);
                if (requestUser is not null)
                {
                    Role authRole = await _roleRepository.GetRoleWithId(requestUser.RoleId);
                    if (SecuritySystem.CheckIfUserHasRole(authRole, "Administrator"))
                    {

                        User? user = await _repo.GetUserWithLogin(login);
                        if (user is not null)
                        {


                            Role? role = await _roleRepository.GetRoleWithName(roleName);
                            if (role is not null)
                            {
                                await _repo.AddUserToRole(user, role);
                                return Ok();

                            }
                            else return NotFound(roleName);



                        }
                        else return NotFound(login);

                    }
                    else return StatusCode(StatusCodes.Status403Forbidden);

                }
                else return NotFound(id);
            }
            else return BadRequest(id);

        }
        [HttpDelete("user")]
        public async Task<IActionResult> DeleteUser([FromQuery] string login, [FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? user = await _repo.GetUserWithId((int)id);
                if (user is not null)
                {
                    Role authRole = await _roleRepository.GetRoleWithId(user.RoleId);
                    if (SecuritySystem.CheckIfUserHasRole(authRole, "Administrator"))
                    {
                        User userToDelete = await _repo.GetUserWithLogin(login);
                        await _repo.Deleteuser(userToDelete);
                        return Ok(user);

                    }
                    else
                    {
                        return StatusCode(StatusCodes.Status403Forbidden);
                    }
                }
                else
                {
                    return NotFound(login);
                }

            }
            else
            {
                return BadRequest(id);
            }

        }

        [HttpPost("auth")]
        public async Task<IActionResult> Authenticate([FromQuery] string login, [FromQuery] string password)
        {
            User? user = await _repo.GetUserWithLogin(login);
            if (user is not null)
            {
                if (user.Password == password)
                {
                    return Ok(SecuritySystem.GenerateJwtToken(user));

                }
                else
                {
                    return BadRequest();
                }
            }
            else
            {
                return NotFound(login);
            }

        }
        [HttpGet("privileges")]
        public async Task<IActionResult> CheckPrivileges([FromQuery] string token)
        {
            int? id = SecuritySystem.ValidateToken(token);
            if (id is not null)
            {
                User? user = await _repo.GetUserWithId((int)id);
                if (user is not null)
                {
                    Role authRole = await _roleRepository.GetRoleWithId(user.RoleId);
                    
                        return Ok(authRole.Name);
                    
                }
                else return NotFound(id);
            }
            else return BadRequest();
        }

    }
}
