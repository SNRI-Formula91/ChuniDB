using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;

namespace ChuniDB
{
    internal static class ChuniDB
    {
        static void Main()
        {
            var baseString = new SqliteConnectionStringBuilder()
            {
                Mode = SqliteOpenMode.ReadWriteCreate,
                DataSource = "E:\\Chunithm Data\\ChuniDB-TEST.db",
                Cache = SqliteCacheMode.Shared
            }.ToString();

            var connectionString = new SqliteConnection(baseString);
            connectionString.Open();
            DirectoryInfo currentPath = new DirectoryInfo(@"E:\Chunithm Data\Paradise\Data");
            DataRoot chuniRoot = new DataRoot(currentPath, connectionString);
            chuniRoot.SearchFolders();

            connectionString.Close();
        }
    }        
}



