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
    internal class musicParser
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
        private string exType;
        private string nameID;
        private string nameStr;
        private string rightsInfoNameID;
        private string rightsInfoNameStr;
        private string sortName;
        private string artistNameID;
        private string artistNameStr;
        private string genreNameID;
        private string genreNameStr;
        private string worksNameID;
        private string worksNameStr;
        private string firstLock;
        private string enableUltima;
        private string priority;
        private string cueFileNameID;
        private string cueFileNameStr;
        private string previewStartTime;
        private string previewEndTime;
        private string worldsEndTagNameID;
        private string worldsEndTagNameStr;
        private string starDifType;
        private string stageNameID;
        private string stageNameStr;
        private string basicLevel;
        private string basicDecimal;
        private string advancedLevel;
        private string advancedDecimal;
        private string expertLevel;
        private string expertDecimal;
        private string masterLevel;
        private string masterDecimal;
        private string ultimaLevel;
        private string ultimaDecimal;
        private string worldsEndLevel;
        private string worldsEndDecimal;

        //default constructor
        //Inputs:
        //string xmlFilePath = a file path that will point to music.xmls for parsing
        //SqliteConnection connstring = connection string info for inserting into the SQLite database
        //string option = name of the option folder whose files are being parsed.
        public musicParser(string xmlFilePath, SqliteConnection connString, string option)
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
            disableFlag = "none";
            exType = "";
            nameID = "";
            nameStr = "";
            rightsInfoNameID = "none";
            rightsInfoNameStr = "none";
            sortName = "";
            artistNameID = "";
            artistNameStr = "";
            genreNameID = "";
            genreNameStr = "";
            worksNameID = "";
            worksNameStr = "";
            firstLock = "";
            enableUltima = "none";
            priority = "";
            cueFileNameID = "";
            cueFileNameStr = "";
            previewStartTime = "none";
            previewEndTime = "none";
            worldsEndTagNameID = "";
            worldsEndTagNameStr = "";
            starDifType = "";
            stageNameID = "";
            stageNameStr = "";
            basicLevel = "";
            basicDecimal = "";
            advancedLevel = "";
            advancedDecimal = "";
            expertLevel = "";
            expertDecimal = "";
            masterLevel = "";
            masterDecimal = "";
            ultimaLevel = "none";
            ultimaDecimal = "";
            worldsEndLevel = "";
            worldsEndDecimal ="";
    }

        //method for parsing the majority of the information in a music.xml file
        public void parseMusicXML()
        {
            XPathDocument musicFile = new XPathDocument(filePath);
            XPathNavigator musicParser = musicFile.CreateNavigator();

            dataName = musicParser.SelectSingleNode("/MusicData/dataName/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/formatVersion/text()") is not null)
                formatVersion = musicParser.SelectSingleNode("/MusicData/formatVersion/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/resourceVersion/id/text()") is not null)
                resourceVersionID = musicParser.SelectSingleNode("/MusicData/resourceVersion/id/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/resourceVersion/str/text()") is not null)
                resourceVersionStr = musicParser.SelectSingleNode("/MusicData/resourceVersion/str/text()")!.Value;
            releaseTagNameID = musicParser.SelectSingleNode("/MusicData/releaseTagName/id/text()")!.Value;
            releaseTagNameStr = musicParser.SelectSingleNode("/MusicData/releaseTagName/str/text()")!.Value;
            netOpenNameID = musicParser.SelectSingleNode("/MusicData/netOpenName/id/text()")!.Value;
            netOpenNameStr = musicParser.SelectSingleNode("/MusicData/netOpenName/str/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/disableFlag/text()") is not null)
                disableFlag = musicParser.SelectSingleNode("/MusicData/disableFlag/text()")!.Value;
            exType = musicParser.SelectSingleNode("/MusicData/exType/text()")!.Value;
            nameID = musicParser.SelectSingleNode("/MusicData/name/id/text()")!.Value;
            nameStr = musicParser.SelectSingleNode("/MusicData/name/str/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/rightsInfoName/id/text()") is not null)
                rightsInfoNameID = musicParser.SelectSingleNode("/MusicData/rightsInfoName/id/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/rightsInfoName/str/text()") is not null)
                rightsInfoNameStr = musicParser.SelectSingleNode("/MusicData/rightsInfoName/str/text()")!.Value;
            sortName = musicParser.SelectSingleNode("/MusicData/sortName/text()")!.Value;
            artistNameID = musicParser.SelectSingleNode("/MusicData/artistName/id/text()")!.Value;
            artistNameStr = musicParser.SelectSingleNode("/MusicData/artistName/str/text()")!.Value;
            genreNameID = musicParser.SelectSingleNode("/MusicData/genreNames/list/StringID/id/text()")!.Value;
            genreNameStr = musicParser.SelectSingleNode("/MusicData/genreNames/list/StringID/str/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/worksName/id/text()") is not null)
                worksNameID = musicParser.SelectSingleNode("/MusicData/worksName/id/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/worksName/str/text()") is not null)
                worksNameStr = musicParser.SelectSingleNode("/MusicData/worksName/str/text()")!.Value;
            firstLock = musicParser.SelectSingleNode("/MusicData/firstLock/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/enableUltima/text()") is not null)
                enableUltima = musicParser.SelectSingleNode("/MusicData/enableUltima/text()")!.Value;
            priority = musicParser.SelectSingleNode("/MusicData/priority/text()")!.Value;
            cueFileNameID = musicParser.SelectSingleNode("/MusicData/cueFileName/id/text()")!.Value;
            cueFileNameStr = musicParser.SelectSingleNode("/MusicData/cueFileName/str/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/previewStartTime/text()") is not null)
                previewStartTime = musicParser.SelectSingleNode("/MusicData/previewStartTime/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/previewEndTime/text()") is not null)
                previewEndTime = musicParser.SelectSingleNode("/MusicData/previewEndTime/text()")!.Value;
            worldsEndTagNameID = musicParser.SelectSingleNode("/MusicData/worldsEndTagName/id/text()")!.Value;
            worldsEndTagNameStr = musicParser.SelectSingleNode("/MusicData/worldsEndTagName/str/text()")!.Value;
            starDifType = musicParser.SelectSingleNode("/MusicData/starDifType/text()")!.Value;
            stageNameID = musicParser.SelectSingleNode("/MusicData/stageName/id/text()")!.Value;
            stageNameStr = musicParser.SelectSingleNode("/MusicData/stageName/str/text()")!.Value;
            basicLevel = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[1]/level/text()")!.Value;
            basicDecimal = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[1]/levelDecimal/text()")!.Value;
            advancedLevel = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[2]/level/text()")!.Value;
            advancedDecimal = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[2]/levelDecimal/text()")!.Value;
            expertLevel = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[3]/level/text()")!.Value;
            expertDecimal = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[3]/levelDecimal/text()")!.Value;
            masterLevel = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[4]/level/text()")!.Value;
            masterDecimal = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[4]/levelDecimal/text()")!.Value;
            if (musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[5]/type/data/text()")!.Value == "WORLD'S END")
            {
                worldsEndLevel = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[5]/level/text()")!.Value;
                worldsEndDecimal = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[5]/levelDecimal/text()")!.Value;
                ultimaLevel = "none";
            }
            if (musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[5]/type/data/text()")!.Value == "ULTIMA")
            {
                ultimaLevel = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[5]/level/text()")!.Value;
                ultimaDecimal = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[5]/levelDecimal/text()")!.Value;
                ultimaLevel += "." + ultimaDecimal;
                worldsEndLevel = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[6]/level/text()")!.Value;
                worldsEndDecimal = musicParser.SelectSingleNode("/MusicData/fumens/MusicFumenData[6]/levelDecimal/text()")!.Value;
            }
            //Calculate common difficulties at the end
            basicLevel += "." + basicDecimal;
            advancedLevel += "." + advancedDecimal;
            expertLevel += "." + expertDecimal;
            masterLevel += "." + masterDecimal;
            worldsEndLevel += "." + worldsEndDecimal;
        }

        //method for inserting all the parsed information into a SQLite database
        public void insertToDB()
        {
            Console.WriteLine(nameID);
            Console.WriteLine(dataName);
            Console.WriteLine(optionOrigin);
            Console.WriteLine(disableFlag);
            Console.WriteLine(nameStr);
            Console.WriteLine(sortName);
            Console.WriteLine(artistNameID);
            Console.WriteLine(artistNameStr);
            Console.WriteLine(genreNameID);
            Console.WriteLine(genreNameStr);
            Console.WriteLine(rightsInfoNameID);
            Console.WriteLine(rightsInfoNameStr);
            Console.WriteLine(worksNameID);
            Console.WriteLine(worksNameStr);
            Console.WriteLine(firstLock);
            Console.WriteLine(enableUltima);
            Console.WriteLine(priority);
            Console.WriteLine(previewStartTime);
            Console.WriteLine(previewEndTime);
            Console.WriteLine(worldsEndTagNameID);
            Console.WriteLine(worldsEndTagNameStr);
            Console.WriteLine(starDifType);
            Console.WriteLine(stageNameID);
            Console.WriteLine(stageNameStr);
            Console.WriteLine(basicLevel);
            Console.WriteLine(advancedLevel);
            Console.WriteLine(expertLevel);
            Console.WriteLine(masterLevel);
            Console.WriteLine(ultimaLevel);
            Console.WriteLine(worldsEndLevel);
            Console.WriteLine(cueFileNameID);
            Console.WriteLine(cueFileNameStr);
            Console.WriteLine(formatVersion);
            Console.WriteLine(resourceVersionID);
            Console.WriteLine(resourceVersionStr);
            Console.WriteLine(releaseTagNameID);
            Console.WriteLine(releaseTagNameStr);
            Console.WriteLine(netOpenNameID);
            Console.WriteLine(netOpenNameStr);
            Console.WriteLine(exType);

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
