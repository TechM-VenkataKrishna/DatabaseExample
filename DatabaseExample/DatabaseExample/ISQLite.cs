using SQLite;

namespace DatabaseExample
{
    public interface ISQLite
    {
        SQLiteConnection GetConnection();
    }
}
