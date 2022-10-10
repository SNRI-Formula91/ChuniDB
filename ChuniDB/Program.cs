﻿using System.Xml;
using System.Xml.Linq;
using System.IO;
using Microsoft.Data.Sqlite;

namespace ChuniDB
{
    internal static class ChuniDB
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
                Console.Write("No parameters provided.");
            else
            {
                if (args.Length > 1)
                    Console.Write("Invalid number of parameters provided.");
                else
                {
                    string task = args[0];
                    DirectoryInfo currentPath = new DirectoryInfo(@"D:\Chunithm Data\DataWorkspace");
                    switch (args[0])
                    {
                        case "export":
                            var baseExportString = new SqliteConnectionStringBuilder()
                            {
                                Mode = SqliteOpenMode.ReadWriteCreate,
                                DataSource = "D:\\Chunithm Data\\ChuniDB-TEST.db",
                                Cache = SqliteCacheMode.Shared
                            }.ToString();

                            var connectionExportString = new SqliteConnection(baseExportString);
                            connectionExportString.Open();
                            
                            parser chuniParser = new parser(currentPath, connectionExportString);
                            chuniParser.SearchFolders();
                            connectionExportString.Close();
                            break;
                        case "import":
                            var baseImportString = new SqliteConnectionStringBuilder()
                            {
                                Mode = SqliteOpenMode.ReadOnly,
                                DataSource = "D:\\Chunithm Data\\ChuniDB-TEST.db",
                                Cache = SqliteCacheMode.Shared
                            }.ToString();
                            var connectionImportString = new SqliteConnection(baseImportString);
                            connectionImportString.Open();
                            translator chuniTranslator = new translator(currentPath, connectionImportString);
                            chuniTranslator.TranslateContent();
                            connectionImportString.Close();
                            break;
                        default:
                            Console.Write("Invalid operation provided");
                            break;
                    }
                }
            }
        }
    }
}
        




