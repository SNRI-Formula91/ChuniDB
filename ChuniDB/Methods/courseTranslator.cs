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
    internal class courseTranslator
    {
        private string filePath;
        private SqliteConnection connection;

        private string? nameID;
        private string? nameStr;

        public courseTranslator(string xmlFilePath, SqliteConnection connString)
        {
            filePath = xmlFilePath;
            connection = connString;
        }

        public void translateCourseFile()
        {
            XmlDocument courseFile = new XmlDocument();
            courseFile.Load(@filePath);

            nameID = courseFile.SelectSingleNode("/CourseData/name/id/text()")!.Value;

            SqliteCommand queryName = new SqliteCommand("SELECT courseNameEN FROM Courses WHERE courseID = " + nameID, connection);

            try
            {
                nameStr = queryName.ExecuteScalar() as string;
                Console.WriteLine(nameStr);
                XmlElement node = (XmlElement)courseFile.SelectSingleNode("/CourseData/name/str")!;
                node.InnerText = nameStr!;
                
                courseFile.Save(filePath);
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
