using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using ToDoApp_WPF.Models.DTO;
namespace ToDoApp_WPF.Operations
{
    static class ApiOperations
    {
        const string API_PATH = "https://localhost:44370/";

        static string Token = "";
        public static void AuthenticateUser(string email, string password, Window loginForm)
        {
            LoginModel loginModel = new LoginModel() { Username = email, Password = password };
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response =
                 client.PostAsync(API_PATH + "token",
                   new StringContent(string.Format("grant_type=password&username={0}&password={1}",
                     loginModel.Username,
                     loginModel.Password), Encoding.UTF8,
                     "application/x-www-form-urlencoded")).Result;

                string resultJSON = response.Content.ReadAsStringAsync().Result;
                LoginTokenResponse result = JsonConvert.DeserializeObject<LoginTokenResponse>(resultJSON);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Login succesfull");
                    Token = result.AccessToken;
                    MainWindow mainWindow = new MainWindow();
                    loginForm.Close();
                    mainWindow.Show();
                }
                else
                {
                    MessageBox.Show("Login not success!");
                }
            }
        }
        public static async void RegisterUser(string email, string password, string confirmPassword, Window registerForm)
        {
            RegistrationModel loginModel = new RegistrationModel() { Username = email, Password = password, ConfirmPassword = confirmPassword };
            await System.Threading.Tasks.Task.Run(() =>
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response =
                      client.PostAsync(API_PATH + "api/user/register",
                        new StringContent(string.Format("Email={0}&Password={1}&ConfirmPassword={2}",
                          loginModel.Username,
                          loginModel.Password, loginModel.ConfirmPassword), Encoding.UTF8,
                          "application/x-www-form-urlencoded")).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Successful!");
                        registerForm.Close();
                    }
                    else
                    {
                        MessageBox.Show("Bad Request!");
                    }
                }
            });
        }

        public static void GetTasks()
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().First();
            using (HttpClient client = new HttpClient())
            {
                mainWindow.ListOfTasks.ItemsSource = null;
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                HttpResponseMessage response = client.GetAsync(API_PATH + "api/tasks").Result;
                string resultJSON = response.Content.ReadAsStringAsync().Result;
                mainWindow.ListOfTasks.ItemsSource = JsonConvert.DeserializeObject<List<Models.Task>>(resultJSON).ToList();
            }
        }

        public async static void CreateTask(string header, string description, DateTime? dateOfTask, int? directoryId, Window registerForm)
        {
            AddTaskModel newTask = new AddTaskModel() { DateOfTask = dateOfTask, Description = description, Header = header, Directory_Id = directoryId };
            HttpResponseMessage response = null;
            await System.Threading.Tasks.Task.Run(() =>
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                    string json = JsonConvert.SerializeObject(newTask);
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                    response =
                      client.PostAsync(API_PATH + "api/tasks", data).Result;
                }
            });
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Task was successful added!");
                registerForm.Close();
            }
            else
            {
                MessageBox.Show("Bad Request!");
            }
        }
        public static async void UpdateTask(int id, string header, string description, DateTime? dateOfTask, bool isDone)
        {
            UpdateTaskModel newTask = new UpdateTaskModel() { Id = id, DateOfTask = dateOfTask, Description = description, Header = header, IsDone = isDone };
            await System.Threading.Tasks.Task.Run(() =>
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                    string json = JsonConvert.SerializeObject(newTask);
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");

                    HttpResponseMessage response =
                      client.PutAsync(API_PATH + "api/tasks", data).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Task was successful updated!");
                    }
                    else
                    {
                        MessageBox.Show("Bad Request!");
                    }
                }
            });
        }
        public static async void DeleteTask(int id)
        {
            await System.Threading.Tasks.Task.Run(() =>
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                    HttpResponseMessage response =
                      client.DeleteAsync(API_PATH + "api/tasks/" + id).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Task was successful deleted!");
                    }
                    else
                    {
                        MessageBox.Show("Bad Request!");
                    }
                }
            });
        }
        public static List<Directory> GetDirectories()
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().First();
            string resultJSON = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                HttpResponseMessage response = client.GetAsync(API_PATH + "api/directory").Result;
                resultJSON = response.Content.ReadAsStringAsync().Result;
                mainWindow.DirectoriesList.ItemsSource = JsonConvert.DeserializeObject<List<Directory>>(resultJSON).ToList();
                return JsonConvert.DeserializeObject<List<Directory>>(resultJSON).ToList();
            }
        }
        public static async void GetTasksInDirectory(int directoryId)
        {
            MainWindow mainWindow = Application.Current.Windows.OfType<MainWindow>().First();
            await System.Threading.Tasks.Task.Run(() =>
            {
                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Token);
                    HttpResponseMessage response = client.GetAsync(API_PATH + "api/tasks/indirectory/" + directoryId).Result;
                    string resultJSON = response.Content.ReadAsStringAsync().Result;
                    mainWindow.ListOfTasks.ItemsSource = JsonConvert.DeserializeObject<List<Models.Task>>(resultJSON).ToList();
                }
            });
        }
    }
}
