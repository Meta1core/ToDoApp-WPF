﻿using Microsoft.AspNet.SignalR.Client;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToDoApp_WPF.Models.DTO;
using ToDoApp_WPF.Operations;
namespace ToDoApp_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public System.Threading.Thread Thread { get; set; }

        public string Host = "https://localhost:44370/";

        public IHubProxy Proxy { get; set; }
        public HubConnection Connection { get; set; }

        public bool Active { get; set; }

        public MainWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                InitializeComponent();
                ApiOperations.GetTasks();
                ApiOperations.GetDirectories();
                MessageBox.Show("Delete tasks by double-clicking the left mouse button");
            });
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginModel person = new LoginModel("1@mail.ru", "f9jRS692xJ");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TaskDetails taskDetails = new TaskDetails();
            taskDetails.Show();
        }

        private void CheckBox_MouseEnter(object sender, MouseEventArgs e)
        {
        }

        private void TabItem_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Directory directory = (Directory)DirectoriesList.SelectedItem;
            if (directory != null)
            {
                Dispatcher.Invoke(new Action(() => ApiOperations.GetTasksInDirectory(directory.Id)));
            }
        }

        private void StackPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ApiOperations.GetTasks();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            var taskId = ((CheckBox)sender).Tag;
            UpdateTask((int)taskId);
        }
        private void UpdateTask(int taskId)
        {
            foreach (Models.Task task in ListOfTasks.ItemsSource)
            {
                if (task.Id == taskId)
                {
                    ApiOperations.UpdateTask(task.Id, task.Header, task.Description, task.DateOfTask, true);
                    break;
                }
            }
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Active = true;
            Thread = new System.Threading.Thread(() =>
            {
                Connection = new HubConnection(Host);
                Proxy = Connection.CreateHubProxy("taskHub");

                Proxy.On("sendNotification", () =>
                {
                    Refresh();
                });
                Connection.Start();

                while (Active)
                {
                    System.Threading.Thread.Sleep(10);
                }
            })
            { IsBackground = true };
            Thread.Start();
        }


        private void Refresh()
        {
            Dispatcher.Invoke(new Action(() => ApiOperations.GetTasks()));
            Dispatcher.Invoke(new Action(() => ApiOperations.GetDirectories()));
        }

        private void ListOfTasks_PreviewMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        }

        private void ListOfTasks_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (ListOfTasks.SelectedItem != null)
            {
                Models.Task task = (Models.Task)ListOfTasks.SelectedItem;
                if (task != null)
                {
                    ApiOperations.DeleteTask(task.Id);
                }
            }
        }
    }
}
