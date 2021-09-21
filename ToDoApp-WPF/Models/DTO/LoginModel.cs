using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp_WPF.Models.DTO
{
    class LoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public string Grant_Type { get; set; }

        public LoginModel (string username, string password)
        {
            this.Username = username;
            this.Password = password;
            this.Grant_Type = "password";
        }

        public LoginModel()
        {
        }
    }
}
