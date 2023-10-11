using API.DataManager;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Data;

namespace API.Repositories
{
    public class RoleRepository
    {
        APIContext _context;

        public RoleRepository()
        {
            _context = new APIContext();
        }


        public async Task<IEnumerable<Role>> GetRoles() => await Task.Run(() => _context.Roles);

        public async Task<Role> GetRoleById(int id) => await _context.Roles.FirstOrDefaultAsync(x => x.RoleId == id);
    
        public async Task<int> GetRoleIdByName(string name) => await Task.Run(() => {
            Role role = _context.Roles.Where(x => x.Name == name).FirstOrDefault();
            if(role is not null)
            {
                return role.RoleId;
            }
            else
            {
                return -1;
            }
            
            }) ;

        public async Task AddRole(Role role) => await Task.Run(() => {
            _context.Roles.Add(role);
            _context.SaveChanges();
        });

        public async Task DeleteRole(Role role) => await Task.Run(() =>
        {
            _context.Remove(role);
            _context.SaveChanges();
        });

        public async Task UpdateRole(Role role) => await Task.Run(() =>
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
        });

        public async Task<IEnumerable<User>> GetUsersInRole(Role role) => await Task.Run(() => role.Users);
        public async Task AddUserToRole(User user, Role role) => await Task.Run(() => {
            role.Users.Add(user);
            _context.SaveChanges();
        });

        public async Task RemoveUserToRole(User user, Role role) => await Task.Run(() => {
            role.Users.Remove(user);
            _context.SaveChanges();
        });



    }
}
