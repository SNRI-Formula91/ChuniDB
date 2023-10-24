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
    internal class courseParser
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
        private string nameID;
        private string nameStr;
        private string difficultyID;
        private string difficultyStr;
        private string ruleID;
        private string ruleStr;
        private string rewardID;
        private string rewardStr;
        private string rewardID2;
        private string rewardStr2;
        private string teamOnly;
        private string isDuplicateAllowed;
        private string conditionsID;
        private string conditionsStr;
        private string conditionsText;
        private string priority;
        //New+ loads the track names of selected songs dynamically so we don't need to parse the rest of the document


        //default constructor
        //Inputs:
        //string xmlFilePath = a file path that will point to course.xmls for parsing
        //SqliteConnection connstring = connection string info for inserting into the SQLite database
        //string option = name of the option folder whose files are being parsed.
        public courseParser(string xmlFilePath, SqliteConnection connString, string option)
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
            nameID = "";
            nameStr = "";
            difficultyID = "";
            difficultyStr = "";
            ruleID = "";
            ruleStr = "";
            rewardID = "";
            rewardStr = "";
            rewardID2 = "";
            rewardStr2 = "";
            teamOnly = "";
            isDuplicateAllowed = "";
            conditionsID = "none";
            conditionsStr = "none";
            conditionsText = "none";
            priority = "";
        }

        public void parseCourseXML()
        {
            XPathDocument courseFile = new XPathDocument(filePath);
            XPathNavigator courseParser = courseFile.CreateNavigator();

            dataName = courseParser.SelectSingleNode("/CourseData/dataName/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/formatVersion/text()") is not null)
                formatVersion = courseParser.SelectSingleNode("/CourseData/formatVersion/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/resourceVersion/id/text()") is not null)
                resourceVersionID = courseParser.SelectSingleNode("/CourseData/resourceVersion/id/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/resourceVersion/str/text()") is not null)
                resourceVersionStr = courseParser.SelectSingleNode("/CourseData/resourceVersion/str/text()")!.Value;
            releaseTagNameID = courseParser.SelectSingleNode("/CourseData/releaseTagName/id/text()")!.Value;
            releaseTagNameStr = courseParser.SelectSingleNode("/CourseData/releaseTagName/str/text()")!.Value;
            netOpenNameID = courseParser.SelectSingleNode("/CourseData/netOpenName/id/text()")!.Value;
            netOpenNameStr = courseParser.SelectSingleNode("/CourseData/netOpenName/str/text()")!.Value;
            disableFlag = courseParser.SelectSingleNode("/CourseData/disableFlag/text()")!.Value;
            nameID = courseParser.SelectSingleNode("/CourseData/name/id/text()")!.Value;
            nameStr = courseParser.SelectSingleNode("/CourseData/name/str/text()")!.Value;
            difficultyID = courseParser.SelectSingleNode("/CourseData/difficulty/id/text()")!.Value;
            difficultyStr = courseParser.SelectSingleNode("/CourseData/difficulty/str/text()")!.Value;
            ruleID = courseParser.SelectSingleNode("/CourseData/rule/id/text()")!.Value;
            ruleStr = courseParser.SelectSingleNode("/CourseData/rule/str/text()")!.Value;
            rewardID = courseParser.SelectSingleNode("/CourseData/reward/id/text()")!.Value;
            rewardStr = courseParser.SelectSingleNode("/CourseData/reward/str/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/reward2nd/id/text()") is not null)
                rewardID2 = courseParser.SelectSingleNode("/CourseData/reward2nd/id/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/reward2nd/str/text()") is not null)
                rewardStr2 = courseParser.SelectSingleNode("/CourseData/reward2nd/str/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/teamOnly/text()") is not null)
                teamOnly = courseParser.SelectSingleNode("/CourseData/teamOnly/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/isMusicDuplicateAllowed/text()") is not null)
                isDuplicateAllowed = courseParser.SelectSingleNode("/CourseData/isMusicDuplicateAllowed/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/conditionsCourse/id/text()") is not null)
                conditionsID = courseParser.SelectSingleNode("/CourseData/conditionsCourse/id/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/conditionsCourse/str/text()") is not null)
                conditionsStr = courseParser.SelectSingleNode("/CourseData/conditionsCourse/str/text()")!.Value;
            if (courseParser.SelectSingleNode("/CourseData/conditionsCourse/data/text()") is not null)
                conditionsText = courseParser.SelectSingleNode("/CourseData/conditionsCourse/data/text()")!.Value;
            priority = courseParser.SelectSingleNode("/CourseData/priority/text()")!.Value;
        }

        public void insertToDB()
        {
            Console.WriteLine(nameID);
            Console.WriteLine(dataName);
            Console.WriteLine(optionOrigin);
            Console.WriteLine(disableFlag);
            Console.WriteLine(nameStr);
            Console.WriteLine(difficultyID);
            Console.WriteLine(difficultyStr);
            Console.WriteLine(ruleID);
            Console.WriteLine(ruleStr);
            Console.WriteLine(rewardID);
            Console.WriteLine(rewardStr);
            Console.WriteLine(rewardID2);
            Console.WriteLine(rewardStr2);
            Console.WriteLine(teamOnly);
            Console.WriteLine(conditionsID);
            Console.WriteLine(conditionsStr);
            Console.WriteLine(conditionsText);
            Console.WriteLine(priority);
            Console.WriteLine(formatVersion);
            Console.WriteLine(resourceVersionID);
            Console.WriteLine(resourceVersionStr);
            Console.WriteLine(releaseTagNameID);
            Console.WriteLine(releaseTagNameStr);
            Console.WriteLine(netOpenNameID);
            Console.WriteLine(netOpenNameStr);
            
            SqliteCommand insertCourse = new SqliteCommand("INSERT INTO CourseData (nameID, dataName, option, disableFlag, courseName, " +
            "difficultyID, difficulty, ruleID, rule, rewardID, reward, rewardID2, reward2, teamOnly, conditionsID, " +
            "conditions, conditionsText, priority, formatVersion, resourceVersionID, resourceVersion, releaseTagID, releaseTagVer," +
            " netOpenID, netOpenVer) " +
            "VALUES (@nameID, @dataName, @option, @disableFlag, @courseName, @difficultyID, @difficulty, @ruleID, @rule, @rewardID, " +
            "@reward, @rewardID2, @reward2, @teamOnly, @conditionsID, @conditions, @conditionsText, @priority, " +
            "@formatVersion, @resourceVersionID, @resourceVersion, @releaseTagID, @releaseTagVer, @netOpenID, @netOpenVer)", connection);

            insertCourse.Parameters.AddWithValue("@nameID", nameID);
            insertCourse.Parameters.AddWithValue("@dataName", dataName);
            insertCourse.Parameters.AddWithValue("@option", optionOrigin);
            insertCourse.Parameters.AddWithValue("@disableFlag", disableFlag);
            insertCourse.Parameters.AddWithValue("@courseName", nameStr);
            insertCourse.Parameters.AddWithValue("@difficultyID", difficultyID);
            insertCourse.Parameters.AddWithValue("@difficulty", difficultyStr);
            insertCourse.Parameters.AddWithValue("@ruleID", ruleID);
            insertCourse.Parameters.AddWithValue("@rule", ruleStr);
            insertCourse.Parameters.AddWithValue("@rewardID", rewardID);
            insertCourse.Parameters.AddWithValue("@reward", rewardStr);
            insertCourse.Parameters.AddWithValue("@rewardID2", rewardID2);
            insertCourse.Parameters.AddWithValue("@reward2", rewardStr2);
            insertCourse.Parameters.AddWithValue("@teamOnly", teamOnly);
            insertCourse.Parameters.AddWithValue("@conditionsID", conditionsID);
            insertCourse.Parameters.AddWithValue("@conditions", conditionsStr);
            insertCourse.Parameters.AddWithValue("@conditionsText", conditionsText);
            insertCourse.Parameters.AddWithValue("@priority", priority);
            insertCourse.Parameters.AddWithValue("@formatVersion", formatVersion);
            insertCourse.Parameters.AddWithValue("@resourceVersionID", resourceVersionID);
            insertCourse.Parameters.AddWithValue("@resourceVersion", resourceVersionStr);
            insertCourse.Parameters.AddWithValue("@releaseTagID", releaseTagNameID);
            insertCourse.Parameters.AddWithValue("@releaseTagVer", releaseTagNameStr);
            insertCourse.Parameters.AddWithValue("@netOpenID", netOpenNameID);
            insertCourse.Parameters.AddWithValue("@netOpenVer", netOpenNameStr);
            try
            {
                insertCourse.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

