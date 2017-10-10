using SQLite;
using System.IO;
using TestDrive.Data;
using TestDrive.Droid;

[assembly:Xamarin.Forms.Dependency(typeof(SQLiteAndroid))]
namespace TestDrive.Droid
{
    public class SQLiteAndroid : ISQLite
    {
        private const string nomeArquivoDB = "Agendamento.db3";

        public SQLiteConnection PegarConexao()
        {
            var caminhoDB = Path.Combine(Android.OS.Environment.ExternalStorageDirectory.Path, nomeArquivoDB);
            return new SQLiteConnection(caminhoDB);
        }
    }
}