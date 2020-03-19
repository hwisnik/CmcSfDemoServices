using BusinessLogic.Services;
using Shared.Logger;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Commands;
using Unit_Tests.Helpers;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using DataAccess.SFInterfaces;
using Shared.Entities.DTO.Program;
//using Shared.Entities.DTO.Client_Entities;
using Shared.Entities.DTO.SchedEntities;
using Shared.Entities.InputObjects.Lead;
using Shared.Handlers;
using Shared.Entities.SFDb.Lead;
using Shared.Entities._SearchCriteria.Client;


namespace Unit_Tests.BusinessLogic
{
    [ExcludeFromCodeCoverage]
    [TestClass]
    public class LeadServiceTest
    {
        #region setup

        private static Mock<ILeadRepository> MoqLeadRepository { get; set; }
        private static Mock<IERMSRepository> MoqERMSCustomer { get; set; }
        private static Mock<IContactRepository> MoqcontactRepository { get; set; }
        private static Mock<IPhoneRepository> MoqphoneRepository { get; set; }
        private static Mock<IEmailRepository> MoqemailRepository { get; set; }
        private static Mock<IAddressRepository> MoqaddressRepository { get; set; }
        private static Mock<IUsageRepository> MoqusageRepository { get; set; }
        private static Mock<ICustomerRepository> MoqcustomerRepository { get; set; }
        private static Mock<IProgramRepository> MoqprogramRepository { get; set; }
        private static Mock<ICdcRepository> MoqcdcRepository { get; set; }

        private static Mock<LoggingCommandHandlerDecorator<LogCommand>> MoqLogCommandHandlerDecorator { get; set; }
        private static LogCommand LogCommand { get; set; }

        private static Mock<ILog> MoqLogInstance { get; set; }

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext { get; set; }

        // Use ClassInitialize to run code before running the first test in the class
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext)
        {
            AppLogger.Initialize();
            MoqLeadRepository = new Mock<ILeadRepository>();
            MoqERMSCustomer = new Mock<IERMSRepository>();
            MoqcontactRepository = new Mock<IContactRepository>();
            MoqphoneRepository = new Mock<IPhoneRepository>();
            MoqemailRepository = new Mock<IEmailRepository>();
            MoqaddressRepository = new Mock<IAddressRepository>();
            MoqusageRepository = new Mock<IUsageRepository>();
            MoqprogramRepository = new Mock<IProgramRepository>();
            MoqcustomerRepository = new Mock<ICustomerRepository>();
            MoqcdcRepository = new Mock<ICdcRepository>();

            MoqLogCommandHandlerDecorator = new Mock<LoggingCommandHandlerDecorator<LogCommand>>();
            MoqLogInstance = new Mock<ILog>();
            MoqLogInstance.Setup(s => s.Logger.IsEnabledFor(log4net.Core.Level.Debug)).Returns(false);
            LogCommand = new LogCommand
            {
                LoggingInstance = MoqLogInstance.Object
            };
        }

        // Use TestCleanup to run code after each test has run
        [TestCleanup]
        public void Cleanup()
        {
            //Runs after each test
            UnitTestHelpers.WriteTestResultsToTestContext(TestContext);
        }
        #endregion


        [TestMethod]
        public async Task GetLeadsReturnsAListOfLeads()
        {
            //Arrange 

             var entity = new SFSimpleLeadResults()
            {
                LeadGuid = new Guid(),
                CustomerGuid = Guid.NewGuid(),
                CAPTier = "CAP",
                BillAccount = "123456789",
                CMCCustomerID = 1,
                CustomerName = "Test Customer",
                PhoneNumber = "9999999999",
                Email = "TestEmail@Email.Com",
                County = "TestCounty",
                STAddress = "999 Test st.",
                ZipCode = "99999"
            };

            var entityList = new List<SFSimpleLeadResults> { entity };

            /*
             * What I learned: So.. the setup has a fun little thing of a moq return... so you get to pick what it returns.
             * Then we just check to make sure that it returned something... However, this seems redundant and I should check with howard. 
             */

            MoqLeadRepository.Setup(s => s.GetSfLeads(It.IsAny<SearchLeadSimple>(), It.IsAny<LogCommand>())).ReturnsAsync(entityList);
            var leadService = new LeadService(
                MoqLeadRepository.Object
                , MoqERMSCustomer.Object
                , MoqcontactRepository.Object
                //                , MoqphoneRepository.Object
                //                , MoqemailRepository.Object
                , MoqaddressRepository.Object
                , MoqusageRepository.Object
                , MoqprogramRepository.Object
                , MoqcustomerRepository.Object
                , MoqcdcRepository.Object
                , MoqLogInstance.Object, MoqLogCommandHandlerDecorator.Object);

            //Act
            GenericServiceResponse serviceResponse = await leadService.GetLead(new SearchLeadSimple()

            {
                NumberOfRecords = 100,
                CMCCustomerID = 1,
                CustomerName = "",
                BillAccount = "",
                StAddress = "",
                ZipCode = "",
                PhoneNumber = "",
                EmailAddress = "",
                ProgramGuid = Guid.Empty,

            }, LogCommand);

            UnitTestHelpers.AddTestContextFromServiceResponse(entityList, serviceResponse, TestContext);

            //Verify services repo is called one time
            MoqLeadRepository.Verify(s => s.GetSfLeads(It.IsAny<SearchLeadSimple>(), It.IsAny<LogCommand>()), Times.AtLeastOnce);

            Assert.IsNotNull(serviceResponse);
            Assert.IsTrue(serviceResponse.Success);
            Assert.IsNull(serviceResponse.OperationException);

            var leadEntityList = (List<SFSimpleLeadResults>)serviceResponse.Entity;

            //Assert
            Assert.IsTrue(leadEntityList.Any());

        }

        [TestMethod]
        public async Task GetLeadsReturnsExpectedExceptionMessage()
        {
            //Arrange 

            var entity = new SFSimpleLeadResults()
            {
                LeadGuid = new Guid(),
                CustomerGuid = Guid.NewGuid(),
                CAPTier = "CAP",
                BillAccount = "123456789",
                CMCCustomerID = 1,
                CustomerName = "Test Customer",
                PhoneNumber = "9999999999",
                Email = "TestEmail@Email.Com",
                County = "TestCounty",
                STAddress = "999 Test st.",
                ZipCode = "99999"
            };

            List<SFSimpleLeadResults> entityList = new List<SFSimpleLeadResults> { entity };

            MoqLeadRepository.Setup(s => s.GetSfLeads(It.IsAny<SearchLeadSimple>(), It.IsAny<LogCommand>())).ThrowsAsync(new Exception("Lead Repository Exception"));

            var leadService = new LeadService(MoqLeadRepository.Object
                , MoqERMSCustomer.Object
                , MoqcontactRepository.Object
                //, MoqphoneRepository.Object
                //, MoqemailRepository.Object
                , MoqaddressRepository.Object
                , MoqusageRepository.Object
                , MoqprogramRepository.Object
                , MoqcustomerRepository.Object
                , MoqcdcRepository.Object
                , MoqLogInstance.Object
                , MoqLogCommandHandlerDecorator.Object);

            //Act
            GenericServiceResponse serviceResponse = await leadService.GetLead(new SearchLeadSimple()
            {
                CustomerName = "Test Customer"
            }, LogCommand);

            UnitTestHelpers.AddTestContextFromServiceResponse(entityList, serviceResponse, TestContext, false, "Lead Repository Exception");

            //Verify services repo is called one time
            MoqLeadRepository.Verify(s => s.GetSfLeads(It.IsAny<SearchLeadSimple>(), It.IsAny<LogCommand>()), Times.AtLeastOnce);

            Assert.IsFalse(serviceResponse.Success);
            Assert.IsNotNull(serviceResponse.OperationException);
            Assert.AreEqual("Lead Repository Exception", serviceResponse.OperationException.Message);
            Assert.IsNull(serviceResponse.Entity);

        }

    }
}
