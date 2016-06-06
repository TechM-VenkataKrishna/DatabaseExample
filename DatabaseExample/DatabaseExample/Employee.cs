using SQLite;

namespace DatabaseExample
{
    class Employee
    {

        [PrimaryKey, AutoIncrement]
        public long EmployeeSlNo { get; set; }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
    }
}
