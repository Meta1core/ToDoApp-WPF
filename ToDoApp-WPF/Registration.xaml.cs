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
    /// Interaction logic for Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        const string API_PATH = "https://localhost:44370/";
        public Registration()
        {
            InitializeComponent();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            email.Text = string.Empty;
            email.Foreground = Brushes.Black;
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            password.Password = string.Empty;
            password.Foreground = Brushes.Black;
        }

        private void TextBox_GotFocus_2(object sender, RoutedEventArgs e)
        {
            confirmPassword.Password = string.Empty;
            confirmPassword.Foreground = Brushes.Black;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApiOperations.RegisterUser(email.Text, password.Password, confirmPassword.Password, this);
            //ApiOperations.GetTasks();
        }
    }
}
