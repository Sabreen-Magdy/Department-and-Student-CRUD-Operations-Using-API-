using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfStudent.Models;

namespace WpfStudent
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
                //Base Address = "http://localhost:5103/"
                //Get Data from service "api/Department"
                HttpClient httpClient = new HttpClient();
                HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5103/api/Student");
                if (response.IsSuccessStatusCode)
                {
                    string res = await response.Content.ReadAsStringAsync();
                    List<Student> students = JsonSerializer.Deserialize<List<Student>>(res) ?? new List<Student>();
                    StdList.ItemsSource = students;
                }
                else
                {
                    MessageBox.Show("Try Again");
                }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int stdId = int.Parse(StdIdTextBox.Text);
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5103/api/Student/{stdId}");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                StdDTO std = JsonSerializer.Deserialize<StdDTO>(res) ?? new StdDTO();
                StdList1.ItemsSource = new List<StdDTO> { std };
            }
            else
            {
                MessageBox.Show("Failed to fetch department. Status code: " + response.StatusCode);
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string stdName = StdIdTextBox1.Text;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5103/api/Student/{stdName}");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                StdDTO std = JsonSerializer.Deserialize<StdDTO>(res) ?? new StdDTO();
                StdList2.ItemsSource = new List<StdDTO> { std };
            }
            else
            {
                MessageBox.Show("Failed to fetch department. Status code: " + response.StatusCode);
            }
        }

        private async void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            var student = new
            {
                Name = NameTextBox.Text,
                Age = AgeTextBox.Text,
                Address = addressTextBox.Text,
                Image = ImageTextBox.Text,
                DeptId = deptIdTextBox.Text,

            };
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync("http://localhost:5103/api/Student", new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Student added successfully.");
            }
            else
            {
                MessageBox.Show("Failed to add Student. Status code: " + response.StatusCode);
            }
        }

        private async void UpdateStudent_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(IdTextBox.Text);
            var student = new
            {
                Id = id,
                Name = NameTextBox1.Text,
                Age = AgeTextBox1.Text,
                Address = AddressTextBox1.Text,
                DeptId = DeptIdTextBox1.Text,
                Image = ImageTextBox1.Text,
            };

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PutAsync("http://localhost:5103/api/Student", new StringContent(JsonSerializer.Serialize(student), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Student updated successfully.");
            }
            else
            {
                MessageBox.Show("Failed to update Student. Status code: " + response.StatusCode);
            }
        }

        private async void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(IdTextBox3.Text);

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"{"http://localhost:5103/api/Student"}/{id}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Student deleted successfully.");
            }
            else
            {
                MessageBox.Show("Failed to delete Student. Status code: " + response.StatusCode);
            }
        }
    }
}