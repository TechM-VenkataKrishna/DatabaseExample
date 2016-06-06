using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

// https://developer.xamarin.com/guides/xamarin-forms/working-with/databases/
// sqlite-net-pcl
namespace DatabaseExample
{
    public partial class EmployeeScreen : ContentPage
    {
        private ObservableCollection<Employee> employeeList { get; set; }

        public EmployeeScreen()
        {
            InitializeComponent();

            EmployeeDatabase employeeDatabase = new EmployeeDatabase();
            employeeList = employeeDatabase.GetItems();
            employeeDatabase.Close();

            listView.ItemsSource = employeeList;
        }

        private async void Insert(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                Employee employee = new Employee();
                employee.EmployeeId = RandomNumber();
                employee.EmployeeName = RandomString(8);

                EmployeeDatabase employeeDatabase = new EmployeeDatabase();
                employeeDatabase.InsertItem(employee);
                employeeDatabase.Close();
            });
        }

        private async void Get(object sender, EventArgs e)
        {
            await Task.Run(() =>
            {
                EmployeeDatabase employeeDatabase = new EmployeeDatabase();
                employeeList = employeeDatabase.GetItems();
                employeeDatabase.Close();
            });
            listView.ItemsSource = employeeList;
        }

        private async void Delete(object sender, EventArgs e)
        {
            var b = (Button)sender;
            var t = (long)b.CommandParameter;

            await Task.Run(() =>
            {
                EmployeeDatabase employeeDatabase = new EmployeeDatabase();
                employeeDatabase.DeleteItem(t);
                employeeDatabase.Close();

                EmployeeDatabase employeeDatabase1 = new EmployeeDatabase();
                employeeList = employeeDatabase1.GetItems();
                employeeDatabase1.Close();
            });
            listView.ItemsSource = employeeList;

        }

        public string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public int RandomNumber()
        {
            Random rnd = new Random();
            return rnd.Next(1, 100);
        }

    }
}
