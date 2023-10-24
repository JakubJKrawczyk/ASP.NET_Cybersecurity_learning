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

        public async Task<Role?> GetUserRole(string login) => _context.Users.Where(u => u.Login == login).FirstOrDefault()?.UserRole;
        public async Task AddUserToRole(User user, Role role)
        {
            user.UserRole = role;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveUserFromRole(User user, Role role) => await Task.Run(() =>
        {

            user.UserRole = null;
            _context.SaveChanges();
        });


        public async Task<bool?> CheckifUserInRole(User user, Role role) => await Task.Run(() =>
        {

            if (user.UserRole.Name == role.Name)
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
