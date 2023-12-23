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
    internal class charaTranslator
    {
        private string filePath;
        private SqliteConnection connection;

        private string? nameID;
        private string? nameStr;
        private string? sortName;
        private string? worksID;
        private string? worksStr;

        public charaTranslator(string xmlFilePath, SqliteConnection connString)
        {
            filePath = xmlFilePath;
            connection = connString;
        }

        public void translateCharaFile()
        {
            XmlDocument charaFile = new XmlDocument();
            charaFile.Load(@filePath);

            nameID = charaFile.SelectSingleNode("/CharaData/name/id/text()")!.Value;
            worksID = charaFile.SelectSingleNode("/CharaData/works/id/text()")!.Value;

            SqliteCommand queryName = new SqliteCommand("SELECT charaNameEN FROM Characters WHERE charaID = " + nameID, connection);
            SqliteCommand querySortName = new SqliteCommand("SELECT sortNameEN FROM Characters WHERE charaID = " + nameID, connection);
            SqliteCommand queryWorks = new SqliteCommand("SELECT worksNameEN FROM Works WHERE worksID = " + worksID, connection);

            try
            {
                sortName = querySortName.ExecuteScalar() as string;
                Console.WriteLine(sortName);
                XmlElement node = (XmlElement)charaFile.SelectSingleNode("/CharaData/sortName")!;
                node.InnerText = sortName!;
                nameStr = queryName.ExecuteScalar() as string;
                Console.WriteLine(nameStr);
                node = (XmlElement)charaFile.SelectSingleNode("/CharaData/name/str")!;
                node.InnerText = nameStr!;
                worksStr = queryWorks.ExecuteScalar() as string;
                Console.WriteLine(worksStr);
                node = (XmlElement)charaFile.SelectSingleNode("/CharaData/works/str")!;
                node.InnerText = worksStr!;

                //logic for getting all the extra transformations
                for (int i = 1; i < 10; i++)
                {
                    if (charaFile.SelectSingleNode("/CharaData/addImages" + i + "/charaName/id/text()") is not null)
                        if (charaFile.SelectSingleNode("/CharaData/addImages" + i + "/charaName/id/text()")!.Value != "-1")
                        {
                            nameID = charaFile.SelectSingleNode("/CharaData/addImages" + i + "/charaName/id/text()")!.Value;
                            //queryName must be redefined for the SqliteCommand to accept the new nameID
                            queryName = new SqliteCommand("SELECT charaNameEN FROM Characters WHERE charaID = " + nameID, connection);
                            nameStr = queryName.ExecuteScalar() as string;
                            Console.WriteLine(nameStr);
                            node = (XmlElement)charaFile.SelectSingleNode("/CharaData/addImages" + i + "/charaName/str")!;
                            node.InnerText = nameStr!;
                        }
                }
                charaFile.Save(filePath);
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
