using API.DataManager;
using Entities.DataModels;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class UserRepository
    {
        APIContext _context;
        public UserRepository()
        {
            _context = new APIContext();
        }

        public async Task<IEnumerable<User>> GetUsers() => await Task.Run(() => _context.Users);

        public async Task<User?> GetUserWithLogin(string login) => await _context.Users.Where(x => x.Login == login).FirstOrDefaultAsync();

        public async Task<User?> GetUserWithId(int id) => await _context.Users.Where(x => x.UserId == id).FirstOrDefaultAsync();
        public async Task<IEnumerable<Role>> GetUserRoles(string login) => _context.Users.Include(x => x.Roles).FirstOrDefaultAsync(x => x.Login == login).Result.Roles;
        public async Task AddRoleToUser(User user, Role role)
        {
            user.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRoleFromUser(User user, Role role) => await Task.Run(() =>
        {

            user.Roles.Remove(role);
            _context.SaveChanges();
        });


        public async Task<bool?> CheckifUserInRole(User user, Role role) => await Task.Run(() =>
        {

            if (user.Roles.Where(r => r.RoleId == role.RoleId).Any())
            {
                return true;
            }
            else return false;
        });

        public async Task AdduserAsync(User user) => await Task.Run(() =>
        {
            user.UserId = _context.Users.Max(x => x.UserId) + 1;
            _context.Users.Add(user);
            _context.SaveChanges();
        });

        public async Task Updateuser(User user) => await Task.Run(() =>
        {

            _context.Users.Update(user);
            _context.SaveChanges();
        });

        public async Task Deleteuser(User user) => await Task.Run(() =>
        {

            _context.Users.Remove(user);
            _context.SaveChanges();
        });

    }
}
