using SQLite;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;

namespace DatabaseExample
{
    class EmployeeDatabase
    {

        SQLiteConnection database;

        public EmployeeDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Employee>();
        }

        public ObservableCollection<Employee> GetItems()
        {
            return new ObservableCollection<Employee>((from i in database.Table<Employee>() select i).ToList());
        }
        public Employee GetItem(int id)
        {
            return database.Table<Employee>().FirstOrDefault(x => x.EmployeeId == id);
        }
        public int DeleteItem(long id)
        {
            return database.Delete<Employee>(id);
        }

        // It will delete all items.
        public int DeleteMultipleItem()
        {
            return database.DeleteAll<Employee>();
        }

        public int InsertItem(Employee employee)
        {
            return database.Insert(employee);
        }

        public int InsertMultipleItem(List<Employee> employeeList)
        {
            return database.InsertAll(employeeList);
        }

        public void Close()
        {
            database.Close();
        }
    }
}
