using AUInterconnect.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AUInterconnect.DataModelsTests
{
    
    
    /// <summary>
    ///This is a test class for ProposalParserTest and is intended
    ///to contain all ProposalParserTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProposalParserTest
    {

        private static string testContent1 =
"<h1>Test Proposal Event</h1>" +
"<h2>Host Organization</h2>" +
"<p>&nbsp;</p>" +
"<h2>Contact</h2>" +
"<p>Jeff Wang (333)333-3333 cjw39@hotmail.com</p>" +
"<h2>Description</h2>" +
"<p>Personally i'm not a fan of the <span style=\"color: #888888;\">twilight</span> series</p>" +
"<span style=\"text-decoration: line-through;\">deleted description</span>" +
"<h2>Date and Time</h2>" +
"<p>10/1/2013 1:00 PM - 10/1/2013 5:00 PM</p>" +
"<h2>Meeting Time and Location</h2>" +
"<p>10/1/2013 1:00 PM at the intersection of Glenn and College</p>" +
"<h2>Event Location</h2>" +
"<p>Nowhere</p>" +
"<h2>Transportation</h2>" +
"<p>Walking</p>" +
"<p>Requesting drivers: Yes</p>" +
"<h2>Equipment</h2>" +
"<p>&nbsp;</p>" +
"<h2>Food</h2>" +
"<p>&nbsp;</p>" +
"<h2>Cost</h2>" +
"<p>&nbsp;</p>" +
"<h2>Other Notes</h2>" +
"<p>&nbsp;</p>" +
"<h2>Event Capacity</h2>" +
"<p>100</p>" +
"<h2>Registration Deadline</h2>" +
"<p>9/29/2013 12:00 PM</p>";

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for RemoveHtmlTags
        ///</summary>
        [TestMethod()]
        public void RemoveHtmlTagsTest()
        {
            string str = "<h1>hello</h1>";
            Assert.AreEqual("hello", ProposalParser.RemoveHtmlTags(str));
       }


        /// <summary>
        ///A test for GetEventName
        ///</summary>
        [TestMethod()]
        public void GetEventNameTest()
        {
            string str = "<h1>Test <b>Name</b></h1> <h2></h2>";
            Assert.AreEqual("Test Name", ProposalParser.GetEventName(str));

            //Multiple lines - Test 1
            str = "<h1>Test Name" + Environment.NewLine + "</h1> <h2></h2>";
            Assert.AreEqual("Test Name", ProposalParser.GetEventName(str));
        }

        /// <summary>
        ///A test for GetHostOrg
        ///</summary>
        [TestMethod()]
        public void GetHostOrgTest()
        {
            string content = "<h1>Test Proposal Event</h1>\n" +
                "<h2>Host Organization</h2>\n" +
                "<p>Something</p>\n" +
                "<h2>Contact</h2>\n" +
                "<p>Jeff Wang (333)333-3333 cjw39@hotmail.com</p>";
            Assert.AreEqual("Something", ProposalParser.GetHostOrg(content));

            content = "<h1>Test Proposal Event</h1>\n" +
                "<h2>Contact</h2>\n" +
                "<p>Jeff Wang (333)333-3333 cjw39@hotmail.com</p>";
            Assert.IsNull(ProposalParser.GetHostOrg(content));

            content = "<h1>Test Proposal Event</h1>\n" +
                "<h2>Host Organization</h2>\n" +
                "<p></p>\n" +
                "<h2>Contact</h2>\n" +
                "<p>Jeff Wang (333)333-3333 cjw39@hotmail.com</p>";
            Assert.IsNull(ProposalParser.GetHostOrg(content));
        }

        /// <summary>
        ///A test for GetContactPhone
        ///</summary>
        [TestMethod()]
        public void GetContactPhoneTest()
        {
            Assert.AreEqual(3333333333, ProposalParser.GetContactPhone(testContent1));
        }

        /// <summary>
        ///A test for GetContactName
        ///</summary>
        [TestMethod()]
        public void GetContactNameTest()
        { 
            Assert.AreEqual("Jeff Wang", ProposalParser.GetContactName(testContent1));
        }

        /// <summary>
        ///A test for GetContactEmail
        ///</summary>
        [TestMethod()]
        public void GetContactEmailTest()
        {
            Assert.AreEqual("cjw39@hotmail.com", ProposalParser.GetContactEmail(testContent1));
        }

        /// <summary>
        ///A test for GetEventDesc
        ///</summary>
        [TestMethod()]
        public void GetEventDescTest()
        {
            Assert.AreEqual("Personally i'm not a fan of the twilight series",
                ProposalParser.GetEventDesc(testContent1));
        }

        /// <summary>
        ///A test for GetEventStartTime
        ///</summary>
        [TestMethod()]
        public void GetEventStartTimeTest()
        {
            DateTime expected = new DateTime(2013, 10, 1, 13, 0, 0);
            Assert.AreEqual(expected,  ProposalParser.GetEventStartTime(testContent1));
        }

        /// <summary>
        ///A test for GetEventEndTime
        ///</summary>
        [TestMethod()]
        public void GetEventEndTimeTest()
        {
            DateTime expected = new DateTime(2013, 10, 1, 17, 0, 0);
            Assert.AreEqual(expected, ProposalParser.GetEventEndTime(testContent1));
        }

        /// <summary>
        ///A test for GetMeetingLocation
        ///</summary>
        [TestMethod()]
        public void GetMeetingLocationTest()
        {
            Assert.AreEqual("The intersection of Glenn and College",
                ProposalParser.GetMeetingLocation(testContent1));
        }

        /// <summary>
        ///A test for GetMeetingTime
        ///</summary>
        [TestMethod()]
        public void GetMeetingTimeTest()
        {
            DateTime expected = new DateTime(2013, 10, 1, 13, 0, 0);
            Assert.AreEqual(expected, ProposalParser.GetMeetingTime(testContent1));
        }

        /// <summary>
        ///A test for GetEventLocation
        ///</summary>
        [TestMethod()]
        public void GetEventLocationTest()
        {
            Assert.AreEqual("Nowhere", ProposalParser.GetEventLocation(testContent1));
        }

        /// <summary>
        ///A test for GetRequestDrivers
        ///</summary>
        [TestMethod()]
        public void GetRequestDriversTest()
        {
            Assert.IsTrue(ProposalParser.GetRequestDrivers(testContent1));
        }

        /// <summary>
        ///A test for GetEquipment
        ///</summary>
        [TestMethod()]
        public void GetEquipmentTest()
        {
            Assert.IsNull(ProposalParser.GetEquipment(testContent1));
        }

        /// <summary>
        ///A test for GetEventCapacity
        ///</summary>
        [TestMethod()]
        public void GetEventCapacityTest()
        {
            Assert.AreEqual(100, ProposalParser.GetEventCapacity(testContent1));
        }

        /// <summary>
        ///A test for GetRegDeadline
        ///</summary>
        [TestMethod()]
        public void GetRegDeadlineTest()
        {
            DateTime expected = new DateTime(2013, 9, 29, 12, 0, 0);
            Assert.AreEqual(expected, ProposalParser.GetRegDeadline(testContent1));
        }

        /// <summary>
        ///A test for RemoveStrikeViaStyle
        ///</summary>
        [TestMethod()]
        public void RemoveStrikeViaStyleTest()
        {
            String str = null;
            
            //No strikethrough
            str = "abc";
            Assert.AreEqual("abc", ProposalParser.RemoveStrikeViaStyle(str));
            str = "abc <span>cde</span>";
            Assert.AreEqual("abc <span>cde</span>", ProposalParser.RemoveStrikeViaStyle(str));

            //Simple strikethrough case
            str = "abc<span style=\"text-decoration: line-through;\">September 15, 2012</span>";
            Assert.AreEqual("abc", ProposalParser.RemoveStrikeViaStyle(str));

            //Strikethrough nested HTML
            str = "abc<span style=\"text-decoration: line-through;\"><b>September</b> 15, 2012</span>";
            Assert.AreEqual("abc", ProposalParser.RemoveStrikeViaStyle(str));

            //Strikethrough with other styles
            str = "abc<span style=\"color:red;text-decoration: line-through;\">September 15, 2012</span>";
            Assert.AreEqual("abc", ProposalParser.RemoveStrikeViaStyle(str));
        }
    }
}
