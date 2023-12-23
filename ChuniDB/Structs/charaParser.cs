using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.Data.Sqlite;
using System.Xml.XPath;

namespace ChuniDB.Structs
{
    internal class charaParser
    {
        private string filePath;
        private string optionOrigin;
        private SqliteConnection connection;

        private string dataName;
        private string formatVersion;
        private string resourceVersionID;
        private string resourceVersionStr;
        private string releaseTagNameID;
        private string releaseTagNameStr;
        private string netOpenNameID;
        private string netOpenNameStr;
        private string disableFlag;
        //need to account for all the extra transformations
        private string[] nameID;
        private string[] nameStr;
        private string rightsInfoNameID;
        private string rightsInfoNameStr;
        private string sortName;
        private string worksNameID;
        private string worksNameStr;
        private string illustratorID;
        private string illustratorStr;
        private string defaultHave;
        private string firstSkillID;
        private string firstSkillStr;
        private string rareType;
        private string normConditionVerID;
        private string normConditionVerStr;
        private string priority;
        
        //default constructor
        //Inputs:
        //string xmlFilePath = a file path that will point to chara.xmls for parsing
        //SqliteConnection connstring = connection string info for inserting into the SQLite database
        //int version = command line argument that determines parsing patters based on Chunithm file version
        //string option = name of the option folder whose files are being parsed.
        public charaParser(string xmlFilePath, SqliteConnection connString, string option)
        {
            filePath = xmlFilePath;
            connection = connString;
            optionOrigin = option;

        dataName = "";
        formatVersion = "none";
        resourceVersionID = "none";
        resourceVersionStr = "none";
        releaseTagNameID = "";
        releaseTagNameStr = "";
        netOpenNameID = "";
        netOpenNameStr = "";
        disableFlag = "";
        nameID = new string[10];
        //using array fills so that we don't get hit with null value exceptions with sqlite queries later
        Array.Fill(nameID, "none");
        nameStr = new string[10];
        Array.Fill(nameStr, "none");
        rightsInfoNameID = "none";
        rightsInfoNameStr = "none";
        sortName = "";
        worksNameID = "";
        worksNameStr = "";
        illustratorID = "";
        illustratorStr = "";
        defaultHave = "";
        firstSkillID = "none";
        firstSkillStr = "none";
        rareType = "";
        normConditionVerID = "none";
        normConditionVerStr = "none";
        priority = "";
        }

        //method for parsing the majority of the information in a chara.xml file
        public void parseCharaXML()
        {
            XPathDocument charaFile = new XPathDocument(filePath);
            XPathNavigator charaParser = charaFile.CreateNavigator();

            //Start processing common data found in all character files regardless of version
            dataName = charaParser.SelectSingleNode("/CharaData/dataName/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/formatVersion/text()") is not null)
                formatVersion = charaParser.SelectSingleNode("/CharaData/formatVersion/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/resourceVersion/id/text()") is not null)
                resourceVersionID = charaParser.SelectSingleNode("/CharaData/resourceVersion/id/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/resourceVersion/str/text()") is not null)
                resourceVersionStr = charaParser.SelectSingleNode("/CharaData/resourceVersion/str/text()")!.Value;
            releaseTagNameID = charaParser.SelectSingleNode("/CharaData/releaseTagName/id/text()")!.Value;
            releaseTagNameStr = charaParser.SelectSingleNode("/CharaData/releaseTagName/str/text()")!.Value;
            netOpenNameID = charaParser.SelectSingleNode("/CharaData/netOpenName/id/text()")!.Value;
            netOpenNameStr = charaParser.SelectSingleNode("/CharaData/netOpenName/str/text()")!.Value;
            disableFlag = charaParser.SelectSingleNode("/CharaData/disableFlag/text()")!.Value;
            nameID[0] = charaParser.SelectSingleNode("/CharaData/name/id/text()")!.Value;
            nameStr[0] = charaParser.SelectSingleNode("/CharaData/name/str/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/rightsInfoName/id/text()") is not null)
                rightsInfoNameID = charaParser.SelectSingleNode("/CharaData/rightsInfoName/id/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/rightsInfoName/str/text()") is not null)
                rightsInfoNameStr = charaParser.SelectSingleNode("/CharaData/rightsInfoName/str/text()")!.Value;
            sortName = charaParser.SelectSingleNode("/CharaData/sortName/text()")!.Value;
            worksNameID = charaParser.SelectSingleNode("/CharaData/works/id/text()")!.Value;
            worksNameStr = charaParser.SelectSingleNode("/CharaData/works/str/text()")!.Value;
            illustratorID = charaParser.SelectSingleNode("/CharaData/illustratorName/id/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/illustratorName/str/text()") is not null)
                illustratorStr = charaParser.SelectSingleNode("/CharaData/illustratorName/str/text()")!.Value;
            defaultHave = charaParser.SelectSingleNode("/CharaData/defaultHave/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/firstSkill/id/text()") is not null)
                firstSkillID = charaParser.SelectSingleNode("/CharaData/firstSkill/id/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/firstSkill/str/text()") is not null)
                firstSkillStr = charaParser.SelectSingleNode("/CharaData/firstSkill/str/text()")!.Value;
            rareType = charaParser.SelectSingleNode("/CharaData/rareType/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/normConditions/resourceVersion/id/text()") is not null)
                normConditionVerID = charaParser.SelectSingleNode("/CharaData/normConditions/resourceVersion/id/text()")!.Value;
            if (charaParser.SelectSingleNode("/CharaData/normConditions/resourceVersion/str/text()") is not null)
                normConditionVerStr = charaParser.SelectSingleNode("/CharaData/normConditions/resourceVersion/str/text()")!.Value;

            //logic for getting all the extra transformations
            for (int i = 1; i < 10 ; i++)
            {
                if(charaParser.SelectSingleNode("/CharaData/addImages" + i + "/charaName/id/text()") is not null)
                    if(charaParser.SelectSingleNode("/CharaData/addImages" + i + "/charaName/id/text()")!.Value != "-1")
                        nameID[i] = charaParser.SelectSingleNode("/CharaData/addImages" + i + "/charaName/id/text()")!.Value;
                if (charaParser.SelectSingleNode("/CharaData/addImages" + i + "/charaName/str/text()") is not null)
                    if (charaParser.SelectSingleNode("/CharaData/addImages" + i + "/charaName/str/text()")!.Value != "Invalid")
                        nameStr[i] = charaParser.SelectSingleNode("/CharaData/addImages" + i + "/charaName/str/text()")!.Value;
            }
            priority = charaParser.SelectSingleNode("/CharaData/priority/text()")!.Value;
        }

        //method for inserting all the parsed information into a SQLite database
        public void insertToDB()
        {
            Console.WriteLine(dataName);
            Console.WriteLine(formatVersion);
            Console.WriteLine(resourceVersionID);
            Console.WriteLine(resourceVersionStr);
            Console.WriteLine(releaseTagNameID);
            Console.WriteLine(releaseTagNameStr);
            Console.WriteLine(netOpenNameID);
            Console.WriteLine(netOpenNameStr);
            Console.WriteLine(disableFlag);
            Console.WriteLine(nameID[0]);
            Console.WriteLine(nameStr[0]);
            Console.WriteLine(rightsInfoNameID);
            Console.WriteLine(rightsInfoNameStr);
            Console.WriteLine(sortName);
            Console.WriteLine(worksNameID);
            Console.WriteLine(worksNameStr);
            Console.WriteLine(illustratorID);
            Console.WriteLine(illustratorStr);
            Console.WriteLine(defaultHave);
            Console.WriteLine(firstSkillID);
            Console.WriteLine(firstSkillStr);
            Console.WriteLine(rareType);
            Console.WriteLine(normConditionVerID);
            Console.WriteLine(normConditionVerStr);
            Console.WriteLine(priority);

            for (int j = 0; j < nameID.Length; j++)
            {
                if (nameID[j] != "none")
                {
                    Console.WriteLine(nameID[j]);
                    Console.WriteLine(nameStr[j]);
                
                    SqliteCommand insertChara = new SqliteCommand("INSERT INTO CharacterData (nameID, dataName, option, disableFlag, charaName, " +
                    "sortName, defaultHave, illustratorID, illustratorName, rightsInfoID, rightsInfoName, worksID, worksName, " +
                    "firstSkillID, firstSkill, normConditionID, normCondition, resourceVersionID, resourceVersion, " +
                    "priority, releaseTagID, releaseTagVer, netOpenID, netOpenVer) " +
                    "VALUES (@nameID, @dataName, @option, @disableFlag, @charaName, @sortName, @defaultHave, @illustratorID, @illustratorName, " +
                    "@rightsInfoID, @rightsInfoName, @worksID, @worksName, @firstSkillID, @firstSkill, @normConditionID, @normCondition, " +
                    "@resourceVersionID, @resourceVersion, @priority, @releaseTagID, @releaseTagVer, @netOpenID, @netOpenVer)", connection);

          
                    insertChara.Parameters.AddWithValue("@nameID", nameID[j]);
                    insertChara.Parameters.AddWithValue("@dataName", dataName);
                    insertChara.Parameters.AddWithValue("@option", optionOrigin);
                    insertChara.Parameters.AddWithValue("@disableFlag", disableFlag);
                    insertChara.Parameters.AddWithValue("@charaName", nameStr[j]);
                    insertChara.Parameters.AddWithValue("@sortName", sortName);
                    insertChara.Parameters.AddWithValue("@defaultHave", defaultHave);
                    insertChara.Parameters.AddWithValue("@illustratorID", illustratorID);
                    insertChara.Parameters.AddWithValue("@illustratorName", illustratorStr);
                    insertChara.Parameters.AddWithValue("@rightsInfoID", rightsInfoNameID);
                    insertChara.Parameters.AddWithValue("@rightsInfoName", rightsInfoNameStr);
                    insertChara.Parameters.AddWithValue("@worksID", worksNameID);
                    insertChara.Parameters.AddWithValue("@worksName", worksNameStr);
                    insertChara.Parameters.AddWithValue("@firstSkillID", firstSkillID);
                    insertChara.Parameters.AddWithValue("@firstSkill", firstSkillStr);
                    insertChara.Parameters.AddWithValue("@normConditionID", normConditionVerID);
                    insertChara.Parameters.AddWithValue("@normCondition", normConditionVerStr);
                    insertChara.Parameters.AddWithValue("@resourceVersionID", resourceVersionID);
                    insertChara.Parameters.AddWithValue("@resourceVersion", resourceVersionStr);
                    insertChara.Parameters.AddWithValue("@priority", priority);
                    insertChara.Parameters.AddWithValue("@releaseTagID", releaseTagNameID);
                    insertChara.Parameters.AddWithValue("@releaseTagVer", releaseTagNameStr);
                    insertChara.Parameters.AddWithValue("@netOpenID", netOpenNameID);
                    insertChara.Parameters.AddWithValue("@netOpenVer", netOpenNameStr);
                    try
                    {
                        insertChara.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                }
            }
        }
    }
}
