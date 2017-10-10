using SQLite;

namespace TestDrive.Data
{
    public interface ISQLite
    {
        SQLiteConnection PegarConexao();
    }
}
