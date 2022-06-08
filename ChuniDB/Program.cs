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

            music musicFile = new music(@"E:\Chunithm Data\Paradise\Data\A000\music\music0003\Music.xml", connectionString);
            musicFile.parseMusicXML();
            musicFile.insertToDB();

            connectionString.Close();
        }
    }        
}



