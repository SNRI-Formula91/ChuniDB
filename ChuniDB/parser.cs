using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;

namespace ChuniDB
{
    internal class parser
    {
        private string basePath;
        private SqliteConnection cString;

        public parser(DirectoryInfo inputPath, SqliteConnection dbString)
        {
            basePath = inputPath.ToString();
            cString = dbString;
        }
        
        public void SearchFolders(int versionFlag)
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
                                    musicParser musicFile = new musicParser(@file.FullName, cString, optionFolder, versionFlag);
                                    musicFile.parseMusicXML();
                                    musicFile.insertToDB();
                                    break;
                                case "Chara.xml":
                                    break;
                                case "NamePlate.xml":
                                    break;
                                case "SystemVoice.xml":
                                    break;
                                case "Trophy.xml":
                                    break;

                            }
                        }
                    }
                }
            }
        }
    }
}
