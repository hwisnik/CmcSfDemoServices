using System;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shared.Entities;
using Shared.Entities.DTO.Customer;

namespace Unit_Tests.Helpers
{
    /// <summary>
    /// Summary description for HelperTest
    /// </summary>
    [TestClass]
    [ExcludeFromCodeCoverage]

    public class HelperTest
    {
        public HelperTest()
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

        [TestCleanup]
        public void Cleanup()
        {
            //Runs after each test
            UnitTestHelpers.WriteTestResultsToTestContext(TestContext);
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
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        /// <summary>
        /// Purpose of the following method is to generate values for every property of the input class
        /// and generate test assertions of each property.
        /// UnitTestHelpers.PopulateEntityProperties generates 2 output files under c:temp\UnitTestClasses
        /// xxx.Properties.txt has the statements needed to populate the object that you passed in, just
        /// instantiate the class and add the property statements.
        /// //xxx.AssertStatements.txt has the assert statements to check each property
        /// </summary>
        [TestMethod]
        public void TestPopulateEntityProperties()
        {

            //Generates 2 files in C:\temp\UnitTestClasses
            UnitTestHelpers.PopulateEntityProperties(new Account());

 


        }
    }
}
