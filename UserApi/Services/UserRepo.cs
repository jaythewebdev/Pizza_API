using System.Diagnostics;
using UserApi.Interfaces;
using UserApi.Models;

namespace UserApi.Services
{
    public class UserRepo:IUser<int,User>
    {
        private readonly JWTContext _context;

        public UserRepo(JWTContext context)
        {
            _context = context;
        }
        public User Add(User item)
        {
            try
            {
                _context.Users.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
                Debug.WriteLine(item);
            }
            return null;
        }

        public User Get(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == id);
            return user;
        }
    }
}
