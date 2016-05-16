using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace $safeprojectname$
{
    [TestFixture]
    public class TestClass
    {
        public TestClass()
        {
        }

        [TestFixtureSetUp]
        public void Setup()
        {
            // Setup the system for a new test, for example restart the BizTalk host or configure something
            BizUnit.BizUnit bizUnit = new BizUnit.BizUnit(@"..\..\TestCases\TestCaseGenericSetup.xml");
            bizUnit.RunTest();
        }

        [Test]
        public void TestCaseFixture()
        {
            // Execute the testcase
            BizUnit.BizUnit bizUnit = new BizUnit.BizUnit(@"..\..\TestCases\SampleTestCase.xml");
            bizUnit.RunTest();
        }
    }
}