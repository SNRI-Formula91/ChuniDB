using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ChuniDB
{
    internal class translator
    {
        private string basePath;
        private SqliteConnection connectionString;

        public translator(DirectoryInfo inputPath, SqliteConnection dbString)
        {
            basePath = inputPath.ToString();
            connectionString = dbString;
        }

        public void TranslateContent(int versionFlag)
        {
            foreach (string optionDirectories in Directory.GetDirectories(basePath))
            {
                DirectoryInfo option = new DirectoryInfo(optionDirectories);
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
                                    musicTranslator fileToTranslate = new musicTranslator(@file.FullName, connectionString, versionFlag);
                                    fileToTranslate.translateMusicFile();
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
