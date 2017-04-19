using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace CustomerContracts.Core
{
    public class User
    {
        public int Id { get; set; }

        public string Login { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public int UserRoleId { get; set; }

        public Role Role { get; set; }
    }

    public class UserRepository : BaseRepository
    {
        public UserRepository()
            : base(ConfigurationManager.ConnectionStrings["Connection"].ConnectionString) { }

        public User Login(string email, string password)
        {
            if (string.IsNullOrEmpty(email))
                throw new Exception("Enter the e-mail");

            if (string.IsNullOrEmpty(password))
                throw new Exception("Enter the password");

            string sql = @"select * from UserSys
                            inner join UserRole on UserRole.Id = UserSys.UserRoleId
                            where Email = @email";

            User userSelected = connection.Query<User, Role, User>(
                sql,
                (user, role) => 
                {
                    user.Role = role;
                    return user;
                }
                , new { email }).SingleOrDefault();

            if (Encryption.Crypt(userSelected.Password) != password)
                return null;

            return userSelected;
        }

        public User GetUser(string email)
        {
            string sql = @"select * from UserSys
                            inner join UserRole on UserRole.Id = UserSys.UserRoleId
                            where Email = @email";

            User userSelected = connection.Query<User, Role, User>(
                sql,
                (user, role) =>
                {
                    user.Role = role;
                    return user;
                }
                , new { email }).SingleOrDefault();

            return userSelected;
        }

        public bool IsAdmin(string userName)
        {
            string sql = @"select UserRole.IsAdmin from UserSys
                            inner join UserRole on UserRole.Id = UserSys.UserRoleId
                            where Email = @userName";

            return this.connection.Query<bool>(sql, new { userName }).FirstOrDefault();
        }

        public IEnumerable<User> GetAll()
        {
            return this.connection.Query<User>("select Id,Login,Email,UserRoleId from UserSys");
        }
    }
}
