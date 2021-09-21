using System;
using System.Collections.Generic;
using System.Linq;
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
using ToDoApp_WPF.Operations;

namespace ToDoApp_WPF
{
    /// <summary>
    /// Interaction logic for TaskDetails.xaml
    /// </summary>
    public partial class TaskDetails : Window
    {
        private int? DirectoryId = null;
        public TaskDetails()
        {
            InitializeComponent();
            GetDirectories();
            SetValidationProperties();
        }

        private void SetValidationProperties()
        {
            this.header.MaxLength = 12;
            this.description.MaxLength = 30;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ApiOperations.CreateTask(header.Text, description.Text, GetDateAndTime(), DirectoryId, this);
            ApiOperations.GetTasks();
        }
        public void GetDirectories()
        {
            this.directories.ItemsSource = ApiOperations.GetDirectories();
            this.directories.DisplayMemberPath = "DirectoryName";
            this.directories.SelectedValuePath = "Id";
        }
        private DateTime? GetDateAndTime()
        {
            DateTime? date = null;
            if (dateOfTask.SelectedDate.HasValue)
            {
                if (timeOfTask.SelectedTime.HasValue)
                {
                    date = ProcessDate(dateOfTask.SelectedDate.Value, timeOfTask?.SelectedTime.Value);
                }
                else
                {
                    date = dateOfTask.SelectedDate.Value;
                }
            }

            return date;
        }

        private void Header_GotFocus(object sender, RoutedEventArgs e)
        {
            header.Text = string.Empty;
        }

        private void Description_GotFocus(object sender, RoutedEventArgs e)
        {
            description.Text = string.Empty;
        }
        private DateTime? ProcessDate(DateTime? date, DateTime? time)
        {
            DateTime dateOfTask = new DateTime(date.Value.Year,
                    date.Value.Month,
                    date.Value.Day,
                    time.Value.Hour,
                    time.Value.Minute,
                    time.Value.Second);
            return dateOfTask;
        }

        private void directories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DirectoryId = (int) directories.SelectedValue;
        }
    }
}
