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

        public async Task<Role?> GetRoleWithName(string name) => await _context.Roles.Where(x => x.Name == name).FirstOrDefaultAsync();

        public async Task<Role?> GetRoleWithId(int id) => await _context.Roles.Where(x => x.RoleId == id).FirstOrDefaultAsync();

        public async Task AddRole(Role role) => await Task.Run(() =>
        {
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







    }
}
