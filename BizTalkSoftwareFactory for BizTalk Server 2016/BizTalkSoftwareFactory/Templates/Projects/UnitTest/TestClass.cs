using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;
using System.Collections.Generic;
using Winterdom.BizTalk.PipelineTesting;
using Microsoft.BizTalk.Messaging.Interop;

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


        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Example Unit test for receive pipelines
        //[DeploymentItem(@"..\..\TestData\Schema\<Receive_Pipeline_Name>_Test_Data.txt")]
        //[TestMethod()]
        //public void <Receive_Pipeline_Name>_Test()
        //{
        //    string inputFile = "<Pipeline_Name>_Test_Data.txt";
        //    string outputFile = "<Pipeline_Name>_Test_Data";
        //    ReceivePipelineWrapper pipeline = PipelineFactory.CreateReceivePipeline(typeof(<Pipeline_Name>));

        //    // Create the input message to pass through the pipeline
        //    Stream stream = new FileStream(inputFile, FileMode.Open);
        //    IBaseMessage inputMessage = MessageHelper.CreateFromStream(stream);

        //    inputMessage.BodyPart.Charset = "UTF-8";

        //    // Add the necessary schemas to the pipeline, so that
        //    // disassembling works
        //    pipeline.AddDocSpec(typeof(<Pipeline_Name>));

        //    // Execute the pipeline, and check the output
        //    MessageCollection outputMessages = pipeline.Execute(inputMessage);
        //    Assert.IsNotNull(outputMessages);

        //    // Check the expected count
        //    int expectedCount = 9;
        //    Assert.AreEqual(expectedCount, outputMessages.Count);

        //    // Write the messages to the output folder for reference
        //    foreach (var msg in outputMessages)
        //    {
        //        string contents = new StreamReader(msg.BodyPart.Data).ReadToEnd();
        //        File.WriteAllText(string.Format("{0}_{1}.out", outputFile, msg.MessageID), contents);
        //    }
        //}
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        // Example Unit test for Send pipelines
        //[DeploymentItem(@"..\..\TestData\Schema\<Send_Pipeline_Name>_Test_Data.xml")]
        //[DeploymentItem(@"..\..\TestData\Pipeline\<Send_Pipeline_Name>_Test_Expected.txt")]
        //[TestMethod()]
        //public void <Send_Pipeline_Name>_Test()
        //{
        //    string inputFile = "<Send_Pipeline_Name>_Test_Data.xml";
        //    string expectedFile = "<Send_Pipeline_Name>_Test_Expected.txt";
        //    SendPipelineWrapper pipeline = PipelineFactory.CreateSendPipeline(typeof(<Send_Pipeline_Name>));

        //    // Create the input message to pass through the pipeline
        //    Stream stream = new FileStream(inputFile, FileMode.Open);
        //    IBaseMessage inputMessage = MessageHelper.CreateFromStream(stream);

        //    inputMessage.BodyPart.Charset = "UTF-8";

        //    // Add the necessary schemas to the pipeline, so that
        //    // disassembling works
        //    pipeline.AddDocSpec(typeof(<Send_Pipeline_Name>));

        //    // Execute the pipeline, and check the output
        //    IBaseMessage resultMessage = pipeline.Execute(inputMessage);
        //    Assert.IsNotNull(resultMessage);

        //    // Check the expected result message
        //    StreamReader sr = new StreamReader(resultMessage.BodyPart.Data);
        //    string result = sr.ReadToEnd();

        //    // Compare the contents with the expected values
        //    string expected = File.ReadAllText(expectedFile);

        //    //test the result
        //    Assert.AreEqual(result, expected, "The results are not identical to the expected data!");
        //}
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    }
}
