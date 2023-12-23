using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

namespace ChuniDB.Methods
{
    internal class musicTranslator
    {
        private string filePath;
        private SqliteConnection connection;

        private string? nameID;
        private string? nameStr;
        private string? sortName;
        private string? artistNameID;
        private string? artistNameStr;
        private string? genreNameID;
        private string? genreNameStr;
        private string? worksNameID;
        private string? worksNameStr;

        public musicTranslator(string xmlFilePath, SqliteConnection connString)
        {
            filePath = xmlFilePath;
            connection = connString;
        }

        public void translateMusicFile()
        {
            XmlDocument musicFile = new XmlDocument();
            musicFile.Load(@filePath);

            nameID = musicFile.SelectSingleNode("/MusicData/name/id/text()")!.Value;
            artistNameID = musicFile.SelectSingleNode("/MusicData/artistName/id/text()")!.Value;
            genreNameID = musicFile.SelectSingleNode("/MusicData/genreNames/list/StringID/id/text()")!.Value;

            SqliteCommand queryName = new SqliteCommand("SELECT songNameEN FROM Songs WHERE songID = " + nameID, connection);
            SqliteCommand querySortName = new SqliteCommand("SELECT sortNameEN FROM Songs WHERE songID = " + nameID, connection);
            SqliteCommand queryArtist = new SqliteCommand("SELECT artistEN FROM Artists WHERE artistID = " + artistNameID, connection);
            SqliteCommand queryGrenre = new SqliteCommand("SELECT genreEN FROM Genres WHERE genreID = " + genreNameID, connection);
            SqliteCommand queryWorks = new SqliteCommand("SELECT worksNameEN FROM Works WHERE worksID = " + worksNameID, connection);

            try
            {
                sortName = querySortName.ExecuteScalar() as string;
                Console.WriteLine(sortName);
                XmlElement node = (XmlElement)musicFile.SelectSingleNode("/MusicData/sortName")!;
                node.InnerText = sortName!;
                nameStr = queryName.ExecuteScalar() as string;
                Console.WriteLine(nameStr);
                node = (XmlElement)musicFile.SelectSingleNode("/MusicData/name/str")!;
                node.InnerText = nameStr!;
                artistNameStr = queryArtist.ExecuteScalar() as string;
                Console.WriteLine(artistNameStr);
                node = (XmlElement)musicFile.SelectSingleNode("/MusicData/artistName/str")!;
                node.InnerText = artistNameStr!;
                genreNameStr = queryGrenre.ExecuteScalar() as string;
                Console.WriteLine(genreNameStr);
                node = (XmlElement)musicFile.SelectSingleNode("/MusicData/genreNames/list/StringID/str")!;
                node.InnerText = genreNameStr!;
                worksNameStr = queryWorks.ExecuteScalar() as string;
                Console.WriteLine(worksNameStr);
                node = (XmlElement)musicFile.SelectSingleNode("MusicData/worksName/str")!;
                node.InnerText = worksNameStr!;
                
                musicFile.Save(filePath);
            }
            catch (Exception ex)
            {
                if (ex is ArgumentNullException)
                {
                    Console.WriteLine("Query returned no result");
                    throw;
                }
                throw new Exception(ex.Message);
            }
        }
    }
}
