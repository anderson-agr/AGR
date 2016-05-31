using SQLite.Net;

namespace AGR.Data
{
    public interface IConfig
    {
        SQLiteConnection GetConnection();
    }
}