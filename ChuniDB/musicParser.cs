using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.Sqlite;

namespace ChuniDB
{
    internal class musicParser
    {
        private string filePath;
        private string optionOrigin;
        private SqliteConnection connection;
        private int version;

        private string? dataName;
        private string? formatVersion;
        private string? resourceVersionID;
        private string? resourceVersionStr;
        private string? releaseTagNameID;
        private string? releaseTagNameStr;
        private string? netOpenNameID;
        private string? netOpenNameStr;
        private string? disableFlag;
        private string? exType;
        private string? nameID;
        private string? nameStr;
        private string? rightsInfoNameID;
        private string? rightsInfoNameStr;
        private string? sortName;
        private string? artistNameID;
        private string? artistNameStr;
        private string? genreNameID;
        private string? genreNameStr;
        private string? worksNameID;
        private string? worksNameStr;
        private string? firstLock;
        private string? enableUltima;
        private string? priority;
        private string? cueFileNameID;
        private string? cueFileNameStr;
        private string? previewStartTime;
        private string? previewEndTime;
        private string? worldsEndTagNameID;
        private string? worldsEndTagNameStr;
        private string? starDifType;
        private string? stageNameID;
        private string? stageNameStr;
        private string? basicLevel;
        private string? basicDecimal;
        private string? advancedLevel;
        private string? advancedDecimal;
        private string? expertLevel;
        private string? expertDecimal;
        private string? masterLevel;
        private string? masterDecimal;
        private string? ultimaLevel;
        private string? ultimaDecimal;
        private string? worldsEndLevel;
        private string? worldsEndDecimal;

        //default constructor
        //Inputs:
        //string xmlFilePath = a file path that will point to music.xmls for parsing
        //SqliteConnection connstring = connection string info for inserting into the SQLite database
        //int version = command line argument that determines parsing patters based on Chunithm file version
        //string option = name of the option folder whose files are being parsed.
        public musicParser(string xmlFilePath, SqliteConnection connString, string option, int version)
        {
            filePath = xmlFilePath;
            connection = connString;
            optionOrigin = option;
            this.version = version;
        }

        //method for parsing the majority of the information in a music.xml file
        public void parseMusicXML()
        {
            string xmlFileInput = System.IO.File.ReadAllText(@filePath);
            XElement root = XElement.Parse(xmlFileInput);

            //Start processing common data found in all music files regardless of version
            dataName = (string)(from el in root.Descendants("dataName") select el).First();
            //A010 music.xml appears to trigger exceptions due to this element being missing.
            try
            {
                disableFlag = (string)(from el in root.Descendants("disableFlag") select el).First();
            }
            catch (InvalidOperationException e)
            {
                disableFlag = "none";
            }
            exType = (string)(from el in root.Descendants("exType") select el).First();
            sortName = (string)(from el in root.Descendants("sortName") select el).First();
            firstLock = (string)(from el in root.Descendants("firstLock") select el).First();
            priority = (string)(from el in root.Descendants("priority") select el).First();
            previewStartTime = (string)(from el in root.Descendants("previewStartTime") select el).First();
            previewEndTime = (string)(from el in root.Descendants("previewEndTime") select el).First();
            starDifType = (string)(from el in root.Descendants("starDifType") select el).First();
            basicLevel = (string)(from el in root.Descendants("level") select el).First();
            basicDecimal = (string)(from el in root.Descendants("levelDecimal") select el).First();
            advancedLevel = (string)(from el in root.Descendants("level") select el).Skip(1).First();
            advancedDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(1).First();
            expertLevel = (string)(from el in root.Descendants("level") select el).Skip(2).First();
            expertDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(2).First();
            masterLevel = (string)(from el in root.Descendants("level") select el).Skip(3).First();
            masterDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(3).First();
            //For processing Paradise and prior file patterns
            if (version == 0)
            {
                formatVersion = (string)(from el in root.Descendants("formatVersion") select el).First();
                resourceVersionID = (string)(from el in root.Descendants("id") select el).First();
                resourceVersionStr = (string)(from el in root.Descendants("str") select el).First();
                releaseTagNameID = (string)(from el in root.Descendants("id") select el).Skip(1).First();
                releaseTagNameStr = (string)(from el in root.Descendants("str") select el).Skip(1).First();
                netOpenNameID = (string)(from el in root.Descendants("id") select el).Skip(2).First();
                netOpenNameStr = (string)(from el in root.Descendants("str") select el).Skip(2).First();
                nameID = (string)(from el in root.Descendants("id") select el).Skip(3).First();
                nameStr = (string)(from el in root.Descendants("str") select el).Skip(3).First();
                rightsInfoNameID = (string)(from el in root.Descendants("id") select el).Skip(4).First();
                rightsInfoNameStr = (string)(from el in root.Descendants("str") select el).Skip(4).First();
                artistNameID = (string)(from el in root.Descendants("id") select el).Skip(5).First();
                artistNameStr = (string)(from el in root.Descendants("str") select el).Skip(5).First();
                genreNameID = (string)(from el in root.Descendants("id") select el).Skip(6).First();
                genreNameStr = (string)(from el in root.Descendants("str") select el).Skip(6).First();
                worksNameID = (string)(from el in root.Descendants("id") select el).Skip(7).First();
                worksNameStr = (string)(from el in root.Descendants("str") select el).Skip(7).First();
                cueFileNameID = (string)(from el in root.Descendants("id") select el).Skip(8).First();
                cueFileNameStr = (string)(from el in root.Descendants("str") select el).Skip(8).First();
                worldsEndTagNameID = (string)(from el in root.Descendants("id") select el).Skip(9).First();
                worldsEndTagNameStr = (string)(from el in root.Descendants("str") select el).Skip(9).First();
                stageNameID = (string)(from el in root.Descendants("id") select el).Skip(10).First();
                stageNameStr = (string)(from el in root.Descendants("str") select el).Skip(10).First();
                worldsEndLevel = (string)(from el in root.Descendants("level") select el).Skip(4).First();
                worldsEndDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(4).First();

                //These do not exist in Paradise
                enableUltima = "none";
                ultimaLevel = "none";
            }
            //For processing Chunithm New files patterns
            if (version == 1)
            {
                formatVersion = "none";
                resourceVersionID = "none";
                resourceVersionStr = "none";
                releaseTagNameID = (string)(from el in root.Descendants("id") select el).First();
                releaseTagNameStr = (string)(from el in root.Descendants("str") select el).First();
                netOpenNameID = (string)(from el in root.Descendants("id") select el).Skip(1).First();
                netOpenNameStr = (string)(from el in root.Descendants("str") select el).Skip(1).First();
                nameID = (string)(from el in root.Descendants("id") select el).Skip(2).First();
                nameStr = (string)(from el in root.Descendants("str") select el).Skip(2).First();
                artistNameID = (string)(from el in root.Descendants("id") select el).Skip(3).First();
                artistNameStr = (string)(from el in root.Descendants("str") select el).Skip(3).First();
                genreNameID = (string)(from el in root.Descendants("id") select el).Skip(4).First();
                genreNameStr = (string)(from el in root.Descendants("str") select el).Skip(4).First();
                worksNameID = (string)(from el in root.Descendants("id") select el).Skip(5).First();
                worksNameStr = (string)(from el in root.Descendants("str") select el).Skip(5).First();
                enableUltima = (string)(from el in root.Descendants("enableUltima") select el).First();
                cueFileNameID = (string)(from el in root.Descendants("id") select el).Skip(6).First();
                cueFileNameStr = (string)(from el in root.Descendants("str") select el).Skip(6).First();
                worldsEndTagNameID = (string)(from el in root.Descendants("id") select el).Skip(7).First();
                worldsEndTagNameStr = (string)(from el in root.Descendants("str") select el).Skip(7).First();
                stageNameID = (string)(from el in root.Descendants("id") select el).Skip(8).First();
                stageNameStr = (string)(from el in root.Descendants("str") select el).Skip(8).First();
                basicLevel = (string)(from el in root.Descendants("level") select el).First();
                basicDecimal = (string)(from el in root.Descendants("levelDecimal") select el).First();
                advancedLevel = (string)(from el in root.Descendants("level") select el).Skip(1).First();
                advancedDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(1).First();
                expertLevel = (string)(from el in root.Descendants("level") select el).Skip(2).First();
                expertDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(2).First();
                masterLevel = (string)(from el in root.Descendants("level") select el).Skip(3).First();
                masterDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(3).First();
                ultimaLevel = (string)(from el in root.Descendants("level") select el).Skip(4).First();
                ultimaDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(4).First();
                worldsEndLevel = (string)(from el in root.Descendants("level") select el).Skip(5).First();
                worldsEndDecimal = (string)(from el in root.Descendants("levelDecimal") select el).Skip(5).First();

                ultimaLevel = ultimaLevel + "." + ultimaDecimal;

                //Variables from Paradise not found in New music data.
                rightsInfoNameID = "none";
                rightsInfoNameStr = "none";
                previewStartTime = "none";
                previewEndTime = "none";
            }
            //Calculate common difficulties at the end
            basicLevel = basicLevel + "." + basicDecimal;
            advancedLevel = advancedLevel + "." + advancedDecimal;
            expertLevel = expertLevel + "." + expertDecimal;
            masterLevel = masterLevel + "." + masterDecimal;
            worldsEndLevel = worldsEndLevel + "." + worldsEndDecimal;
        }
        
        //method for inserting all the parsed information into a SQLite database
        public void insertToDB()
        {
            SqliteCommand insertMusic = new SqliteCommand("INSERT INTO Music (nameID, dataName, option, disableFlag, musicName, " +
            "sortName, artistID, artistName, genreID, genreName, rightsInfoID, rightsInfoName, worksID, worksName, " +
            "firstLock, enableUltima, priority, previewStartTime, previewEndTime, worldsEndTagID, worldsEndTagName, starDifType," +
            "stageID, stageName, basicLevel, advancedLevel, expertLevel, masterLevel, ultimaLevel, worldsEndLevel, cueFileID, " +
            "cueFileName, formatVersion, resourceVersionID, resourceVersion, releaseTagID, releaseTagVer, netOpenID, " +
            "netOpenVer, exType) " +
            "VALUES (@nameID, @dataName, @option, @disableFlag, @musicName, @sortName, @artistID, @artistName, @genreID, " +
            "@genreName, @rightsInfoID, @rightsInfoName, @worksID, @worksName, @firstLock, @enableUltima, @priority, @previewStartTime, " +
            "@previewEndTime, @worldsEndTagID, @worldsEndTagName, @starDifType, @stageID, @stageName, @basicLevel, " +
            "@advancedLevel, @expertLevel, @masterLevel, @ultimaLevel, @worldsEndLevel, @cueFileID, @cueFileName, @formatVersion, " +
            "@resourceVersionID, @resourceVersion, @releaseTagID, @releaseTagVer, @netOpenID, @netOpenVer, @exType)", connection);

            insertMusic.Parameters.AddWithValue("@nameID", nameID);
            insertMusic.Parameters.AddWithValue("@dataName", dataName);
            insertMusic.Parameters.AddWithValue("@option", optionOrigin);
            insertMusic.Parameters.AddWithValue("@disableFlag", disableFlag);
            insertMusic.Parameters.AddWithValue("@musicName", nameStr);
            insertMusic.Parameters.AddWithValue("@sortName", sortName);
            insertMusic.Parameters.AddWithValue("@artistID", artistNameID);
            insertMusic.Parameters.AddWithValue("@artistName", artistNameStr);
            insertMusic.Parameters.AddWithValue("@genreID", genreNameID);
            insertMusic.Parameters.AddWithValue("@genreName", genreNameStr);
            insertMusic.Parameters.AddWithValue("@rightsInfoID", rightsInfoNameID);
            insertMusic.Parameters.AddWithValue("@rightsInfoName", rightsInfoNameStr);
            insertMusic.Parameters.AddWithValue("@worksID", worksNameID);
            insertMusic.Parameters.AddWithValue("@worksName", worksNameStr);
            insertMusic.Parameters.AddWithValue("@firstLock", firstLock);
            insertMusic.Parameters.AddWithValue("@enableUltima", enableUltima);
            insertMusic.Parameters.AddWithValue("@priority", priority);
            insertMusic.Parameters.AddWithValue("@previewStartTime", previewStartTime);
            insertMusic.Parameters.AddWithValue("@previewEndTime", previewEndTime);
            insertMusic.Parameters.AddWithValue("@worldsEndTagID", worldsEndTagNameID);
            insertMusic.Parameters.AddWithValue("@worldsEndTagName", worldsEndTagNameStr);
            insertMusic.Parameters.AddWithValue("@starDifType", starDifType);
            insertMusic.Parameters.AddWithValue("@stageID", stageNameID);
            insertMusic.Parameters.AddWithValue("@stageName", stageNameStr);
            insertMusic.Parameters.AddWithValue("@basicLevel", basicLevel);
            insertMusic.Parameters.AddWithValue("@advancedLevel", advancedLevel);
            insertMusic.Parameters.AddWithValue("@expertLevel", expertLevel);
            insertMusic.Parameters.AddWithValue("@masterLevel", masterLevel);
            insertMusic.Parameters.AddWithValue("@ultimaLevel", ultimaLevel);
            insertMusic.Parameters.AddWithValue("@worldsEndLevel", worldsEndLevel);
            insertMusic.Parameters.AddWithValue("@cueFileID", cueFileNameID);
            insertMusic.Parameters.AddWithValue("@cueFileName", cueFileNameStr);
            insertMusic.Parameters.AddWithValue("@formatVersion", formatVersion);
            insertMusic.Parameters.AddWithValue("@resourceVersionID", resourceVersionID);
            insertMusic.Parameters.AddWithValue("@resourceVersion", resourceVersionStr);
            insertMusic.Parameters.AddWithValue("@releaseTagID", releaseTagNameID);
            insertMusic.Parameters.AddWithValue("@releaseTagVer", releaseTagNameStr);
            insertMusic.Parameters.AddWithValue("@netOpenID", netOpenNameID);
            insertMusic.Parameters.AddWithValue("@netOpenVer", netOpenNameStr);
            insertMusic.Parameters.AddWithValue("@exType", exType);
            try
            {
                insertMusic.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
