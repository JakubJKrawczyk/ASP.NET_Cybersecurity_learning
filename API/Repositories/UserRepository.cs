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



        public async Task<User> GetUserById(int id) => await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);

        public async Task<int> GetUserIdByLogin(string login) => Task.Run(() =>
        {
            User user = _context.Users.Where(x => x.Login == login).FirstOrDefault();
            if (user is not null)
            {
                return user.UserId;
            }
            else
            {
                return -1;
            }

        }).Result;
        public async Task<IEnumerable<Role>> GetUserRoles(int id) => Task.Run(() => GetUserById(id).Result.Roles).Result;
        public async Task AddRoleToUser(User user, Role role)
            => await Task.Run(() =>
            {
                user.Roles.Add(role);
                _context.SaveChanges();
            });

        public async Task RemoveRoleFromUser(User user, Role role) => await Task.Run(() =>
        {

            user.Roles.Remove(role);
            _context.SaveChanges();
        });


        public async Task<bool?> CheckifUserInRole(User user, Role role) => Task.Run(() =>
        {

            if (user.Roles.Where(r => r.RoleId == role.RoleId).Any())
            {
                return true;
            }
            else return false;
        }).Result;

        public async Task AdduserAsync(User user) => await Task.Run(() =>
        {
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
