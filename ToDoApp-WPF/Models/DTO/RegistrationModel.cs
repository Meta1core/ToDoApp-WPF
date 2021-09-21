using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoApp_WPF.Models.DTO
{
    class RegistrationModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public RegistrationModel(string username, string password, string confirmPassword)
        {
            this.Username = username;
            this.Password = password;
            this.ConfirmPassword = confirmPassword;
        }
        public RegistrationModel()
        {
        }
    }
}
