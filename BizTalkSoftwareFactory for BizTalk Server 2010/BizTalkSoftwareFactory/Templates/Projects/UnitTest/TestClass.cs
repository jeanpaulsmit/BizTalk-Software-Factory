using System;
using BizUnit;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace $safeprojectname$
{
    /// <summary>
    /// Summary description for UnitTest1
    /// </summary>
    [TestClass]
    public class TestClass
    {
        public TestClass()
        {
            //
            // TODO: Add constructor logic here
            //
        }

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
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [ClassInitialize()]
        public static void TestClassInitialize(TestContext testContext)
        {
            // It is impossible to set the deployment item attribute on a method not marked with the TestMethod attribute.
            // That's why this is solved this way:
            // The test files are deployed to "<path and solutionname>\\TestResults\\<username>_<machinename> <date> <time>\\Out"
            // To get to the correct location of the TestCaseGenericSetup.xml, we need to take that path into account.
        }

        // Unit tests can be added by right clicking on Schemas, Mappings, Orchestrations etc

    }
}
