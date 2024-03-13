using CodeFirst.Models;

namespace AmazeCareProject.Models
{
    public class User
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SelectedRole { get; set; }


        public User(string userName, string password,string selectedRole)
        {
            UserName = userName;
            Password = password;
            SelectedRole = selectedRole;
        }

        public static implicit operator User?(Patient? v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator User?(Admin? v)
        {
            throw new NotImplementedException();
        }

        public static implicit operator User?(Doctors? v)
        {
            throw new NotImplementedException();
        }
    }
}
