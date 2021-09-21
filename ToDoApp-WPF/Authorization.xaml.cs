using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ToDoApp_WPF.Models.DTO;
using ToDoApp_WPF.Operations;

namespace ToDoApp_WPF
{
    /// <summary>
    /// Interaction logic for Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            email.Text = string.Empty;
            email.Foreground = Brushes.Black;
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            password.Password = string.Empty;
            password.Foreground = Brushes.Black;
        }

        private void signUp_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
        }

        private void login_Click(object sender, RoutedEventArgs e)
        {
            ApiOperations.AuthenticateUser(email.Text, password.Password, this);
        }
    }
}
