using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ChuniDB
{
    internal class DataRoot
    {
        private string basePath;
        private SqliteConnection cString;

        public DataRoot(DirectoryInfo inputPath, SqliteConnection dbString)
        {
            basePath = inputPath.ToString();
            cString = dbString;
        }
        
        public void SearchFolders()
        {
            foreach (string optionDirectories in Directory.GetDirectories(basePath))
            {
                DirectoryInfo option = new DirectoryInfo(optionDirectories);
                string optionFolder = option.Name;
                foreach (string folderType in Directory.GetDirectories(optionDirectories))
                {
                    foreach (string folder in Directory.GetDirectories(folderType))
                    {
                        FileInfo[] files = new DirectoryInfo(folder).GetFiles();
                        foreach (FileInfo file in files)
                        {
                            switch (file.Name)
                            {
                                case "Music.xml":
                                    music musicFile = new music(@file.FullName, cString, optionFolder);
                                    musicFile.parseMusicXML();
                                    musicFile.insertToDB();
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}
