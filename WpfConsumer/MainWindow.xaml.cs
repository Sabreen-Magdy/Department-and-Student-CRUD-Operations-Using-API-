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
using WpfConsumer.Models;
using static System.Net.WebRequestMethods;

namespace WpfConsumer
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
            HttpResponseMessage response = await httpClient.GetAsync("http://localhost:5103/api/Department");
            if(response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                List<Department> departments =JsonSerializer.Deserialize<List<Department>>(res) ?? new List<Department>();
                DeptList.ItemsSource= departments;
            }
            else
            {
                MessageBox.Show("Try Again");
            }
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int deptId = int.Parse(DeptIdTextBox.Text);
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5103/api/Department/{deptId}");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                DeptDTO dept = JsonSerializer.Deserialize<DeptDTO>(res)?? new DeptDTO();
                DeptList1.ItemsSource = new List<DeptDTO> { dept };
            }
            else
            {
                MessageBox.Show("Failed to fetch department. Status code: " + response.StatusCode);
            }
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string deptName = DeptIdTextBox1.Text;
            HttpClient httpClient = new HttpClient();
            HttpResponseMessage response = await httpClient.GetAsync($"http://localhost:5103/api/Department/{deptName}");
            if (response.IsSuccessStatusCode)
            {
                string res = await response.Content.ReadAsStringAsync();
                DeptDTO dept = JsonSerializer.Deserialize<DeptDTO>(res) ?? new DeptDTO();
                DeptList2.ItemsSource = new List<DeptDTO> { dept };
            }
            else
            {
                MessageBox.Show("Failed to fetch department. Status code: " + response.StatusCode);
            }
        }

        private async void AddDepartment_Click(object sender, RoutedEventArgs e)
        {
            var department = new
            {
                Name = NameTextBox.Text,
                Location = LocationTextBox.Text,
                MgrName = MgrNameTextBox.Text
            };
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync("http://localhost:5103/api/Department", new StringContent(JsonSerializer.Serialize(department), Encoding.UTF8, "application/json"));

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Department added successfully.");
            }
            else
            {
                MessageBox.Show("Failed to add department. Status code: " + response.StatusCode);
            }
        }

        private async void UpdateDepartment_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(IdTextBox.Text);
            var department = new
            {
                Id = id,
                Name = NameTextBox1.Text,
                Location = LocationTextBox1.Text,
                MgrName = MgrNameTextBox1.Text
            };

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PutAsync("http://localhost:5103/api/Department", new StringContent(JsonSerializer.Serialize(department), Encoding.UTF8, "application/json"));
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Department updated successfully.");
            }
            else
            {
                MessageBox.Show("Failed to update department. Status code: " + response.StatusCode);
            }
        }

        private async void DeleteDepartment_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(IdTextBox3.Text);

            HttpClient httpClient = new HttpClient();
            var response = await httpClient.DeleteAsync($"{"http://localhost:5103/api/Department"}/{id}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Department deleted successfully.");
            }
            else
            {
                MessageBox.Show("Failed to delete department. Status code: " + response.StatusCode);
            }
        }
    }
}