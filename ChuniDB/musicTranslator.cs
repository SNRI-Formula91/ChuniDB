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

namespace ChuniDB
{
    internal class musicTranslator
    {
        private string filePath;
        private SqliteConnection connection;
        private int version;

        private string? nameID;
        private string? nameStr;
        private string? sortName;
        private string? artistNameID;
        private string? artistNameStr;
        private string? genreNameID;
        private string? genreNameStr;

        public musicTranslator(string xmlFilePath, SqliteConnection connString, int version)
        {
            filePath = xmlFilePath;
            connection = connString;
            this.version = version;
        }

        public void translateMusicFile()
        {
            string xmlFileInput = System.IO.File.ReadAllText(@filePath);
            XDocument musicFile = XDocument.Parse(xmlFileInput);

            if (version == 0)
            {
                nameID = (string)(from el in musicFile.Descendants("id") select el).Skip(3).First();
                artistNameID = (string)(from el in musicFile.Descendants("id") select el).Skip(5).First();
                genreNameID = (string)(from el in musicFile.Descendants("id") select el).Skip(6).First();
            }
            if (version == 1)
            {
                nameID = (string)(from el in musicFile.Descendants("id") select el).Skip(2).First();
                artistNameID = (string)(from el in musicFile.Descendants("id") select el).Skip(3).First();
                genreNameID = (string)(from el in musicFile.Descendants("id") select el).Skip(4).First();
            }
            SqliteCommand queryName = new SqliteCommand("SELECT songNameEN FROM Songs WHERE songID = " + nameID, connection);
            SqliteCommand querySortName = new SqliteCommand("SELECT sortNameEN FROM Songs WHERE songID = " + nameID, connection);
            SqliteCommand queryArtist = new SqliteCommand("SELECT artistEN FROM Artists WHERE artistID = " + artistNameID, connection);
            SqliteCommand queryGrenre = new SqliteCommand("SELECT genreEN FROM Genres WHERE genreID = " + genreNameID, connection);

            try
            {
                sortName = querySortName.ExecuteScalar() as string;
                XElement sortedName = (from el in musicFile.Descendants("sortName") select el).First();
                sortedName.SetValue(sortName!);
                genreNameStr = queryGrenre.ExecuteScalar() as string;
                if (version == 0)
                {
                    nameStr = queryName.ExecuteScalar() as string;
                    XElement songName = (from el in musicFile.Descendants("str") select el).Skip(3).First();
                    songName.SetValue(nameStr!);
                    artistNameStr = queryArtist.ExecuteScalar() as string;
                    XElement artistName = (from el in musicFile.Descendants("str") select el).Skip(5).First();
                    artistName.SetElementValue("str", artistNameStr);
                    XElement genre = (from el in musicFile.Descendants("str") select el).Skip(6).First();
                    genre.SetElementValue("str", genreNameStr);
                }
                if (version == 1)
                {
                    nameStr = queryName.ExecuteScalar() as string;
                    XElement songName = (from el in musicFile.Descendants("str") select el).Skip(2).First();
                    songName.SetValue(nameStr!);
                    artistNameStr = queryArtist.ExecuteScalar() as string;
                    XElement artistName = (from el in musicFile.Descendants("str") select el).Skip(3).First();
                    artistName.SetValue(artistNameStr!);
                    genreNameStr = queryGrenre.ExecuteScalar() as string;
                    XElement genre = (from el in musicFile.Descendants("str") select el).Skip(4).First();
                    genre.SetValue(genreNameStr!);
                }
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
