using System.Windows;
using System.Windows.Media;
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
            email.Text = "test1@mail.ru";
        }

        private void Password_GotFocus(object sender, RoutedEventArgs e)
        {
            password.Password = string.Empty;
            password.Foreground = Brushes.Black;
            password.Password = "123123";
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
