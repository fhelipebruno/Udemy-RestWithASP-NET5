
using Udemy_RestWithASP_NET5.Data.Converter.VO;
using Udemy_RestWithASP_NET5.Model;
using Udemy_RestWithASP_NET5.Model.Context;
using System.Security.Cryptography;
using System.Text;

namespace Udemy_RestWithASP_NET5.Repository {
    public class UserRepository : IUserRepository {

        private readonly MySQLContext _context;

        public UserRepository(MySQLContext context) {
            _context = context;
        }

        public User ValidateCredentials(UserVO user) {
            var pass = ComputeHash(user.Password, new SHA256CryptoServiceProvider());
            return _context.Users.FirstOrDefault(u => u.UserName == user.UserName && (u.Password == pass));
        }

        public User ValidateCredentials(string userName)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == userName);
        }



        public User RefreshUserInfo(User user) {
            if (!_context.Users.Any(u => u.Id.Equals(user.Id))) return null;          

            var result = _context.Users.SingleOrDefault(p => p.Id.Equals(user.Id));

            if (result != null) {
                try {
                    _context.Entry(result).CurrentValues.SetValues(user);
                    _context.SaveChanges();
                }
                catch (Exception) {

                    throw;
                }
            }

            return user;
        }
        private string ComputeHash(string input, SHA256CryptoServiceProvider algorithm) {
            Byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            Byte[] hashedBytes = algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
