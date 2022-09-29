using System.Xml;
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
                if (args.Length > 2)
                    Console.Write("Invalid number of parameters provided.");
                else
                {
                    if (args[0] == "p" || args[0] == "n")
                    {
                        int versionFlag;
                        switch (args[0])
                        {
                            case "p":
                                versionFlag = 0;
                                break;
                            case "n":
                                versionFlag = 1;
                                break;
                            default:
                                versionFlag = 99;
                                break;
                        }
                        if (versionFlag == 99)
                        {
                            Console.Write("Parameter flag for version flag out of range.");
                        }
                        else
                        {
                            string task = args[1];
                            switch (args[1])
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
                                    if (versionFlag == 0)
                                    {
                                        DirectoryInfo currentPath = new DirectoryInfo(@"D:\Chunithm Data\Paradise\Data");
                                        parser chuniRoot = new parser(currentPath, connectionExportString);
                                        chuniRoot.SearchFolders(versionFlag);
                                    }
                                    if (versionFlag == 1)
                                    {
                                        DirectoryInfo currentPath = new DirectoryInfo(@"D:\Chunithm Data\New\Data");
                                        parser chuniRoot = new parser(currentPath, connectionExportString);
                                        chuniRoot.SearchFolders(versionFlag);
                                    }
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

                                    if (versionFlag == 0)
                                    {
                                        DirectoryInfo currentPath = new DirectoryInfo(@"D:\Chunithm Data\Paradise\Data");
                                        translator chuniRoot = new translator(currentPath, connectionImportString);
                                        chuniRoot.TranslateContent(versionFlag);
                                    }
                                    if (versionFlag == 1)
                                    {
                                        DirectoryInfo currentPath = new DirectoryInfo(@"D:\Chunithm Data\New\Data");
                                        translator chuniRoot = new translator(currentPath, connectionImportString);
                                        chuniRoot.TranslateContent(versionFlag);
                                    }
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
    }        
}



