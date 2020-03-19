//using System;
//using System.Collections.Generic;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using BusinessLogic.Services.Interfaces;
//using Moq;
//using log4net;
//using Shared.Logger;
//using Unit_Tests.Helpers;
//using System.Threading.Tasks;
//using Shared.Entities;
//using BusinessLogic.Services;
//using CmcSfRestServices.Controllers;
//using System.Web.Http;
//using System.Web.Http.Results;
//using System.Diagnostics.CodeAnalysis;
//using System.Web;
//using Shared.Commands;
//using Shared.Entities.DTO.Customer;
//using Shared.Entities.InputObjects.Lead;
//using Shared.Entities.SFDb.Lead;
//using Shared.Entities._SearchCriteria.Client;
//using Shared.Handlers;


//namespace Unit_Tests.Controllers
//{
//    /// <summary>
//    /// Summary description for LeadControllerTest
//    /// </summary>
//    /// 
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class LeadControllerTest
//    {
//        #region setup
//        private TestContext testContextInstance;
//        private static Mock<ILeadService> MoqService { get; set; }
//        private static Mock<LoggingCommandHandlerDecorator<LogCommand>> MoqLogCommandHandlerDecorator { get; set; }
//        private static Mock<ICommandHandler<LogCommand>> MoqLogCommand { get; set; }
//        private static Mock<ILog> MoqLogInstance { get; set; }
//        private static Mock<HttpContextBase> MoqHttpContextBase { get; set; }

//        /// <summary>
//        ///Gets or sets the test context which provides
//        ///information about and functionality for the current test run.
//        ///</summary>
//        public TestContext TestContext
//        {
//            get
//            {
//                return testContextInstance;
//            }
//            set
//            {
//                testContextInstance = value;
//            }
//        }

//        // Use ClassInitialize to run code before running the first test in the class
//        [ClassInitialize()]
//        public static void ClassInitialize(TestContext testContext)
//        {
//            AppLogger.Initialize();
//            MoqService = new Mock<ILeadService>();
//            MoqLogCommandHandlerDecorator = new Mock<LoggingCommandHandlerDecorator<LogCommand>>();
//            MoqLogInstance = new Mock<ILog>();
//            MoqHttpContextBase = new Mock<HttpContextBase>();
//        }

//        // Use TestCleanup to run code after each test has run
//        [TestCleanup]
//        public void Cleanup()
//        {
//            //Runs after each test
//            UnitTestHelpers.WriteTestResultsToTestContext(TestContext);
//        }
//        #endregion
//        [TestMethod]
//        public async Task GetLeadReturnsExpectedLead()
//        {
//            // Arrange
//            DateTime now = DateTime.Now;

//            var entity = new Lead
//            {
//                LeadGuid = Guid.NewGuid(),
//                CustomerGuid = Guid.NewGuid(),
//                LeadSource = "Test",
//                LeadStatusGuid = Guid.NewGuid(),
//            };

//            var response = new GenericServiceResponse
//            {

//                Success = true,
//                Entity = entity
//            };


//            MoqService.Setup(s => s.GetLead(It.IsAny<Guid>(), It.IsAny<LogCommand>())).ReturnsAsync(response);
//            MoqLogInstance.Setup(s => s.Logger.IsEnabledFor(log4net.Core.Level.Debug)).Returns(false);
//            LeadController controller = new LeadController(MoqService.Object, MoqLogInstance.Object, MoqLogCommandHandlerDecorator.Object);

//            // Act
//            IHttpActionResult actionResult = await controller.GetLead(entity.LeadGuid);
//            var contentResult = actionResult as OkNegotiatedContentResult<GenericServiceResponse>;

//            Lead LeadEntityResponse = (Lead)contentResult.Content.Entity;
//            UnitTestHelpers.AddTestContextPropertiesFromOkHttpResult(entity, contentResult, LeadEntityResponse, TestContext);

//            //Assert
//            Assert.IsNotNull(contentResult);
//            Assert.AreEqual(true, contentResult.Content.Success);
//            Assert.IsNull(contentResult.Content.OperationException);

//            Assert.AreEqual(entity.LeadGuid, LeadEntityResponse.LeadGuid);
//            Assert.AreEqual(entity.CustomerGuid, LeadEntityResponse.CustomerGuid);
//            Assert.AreEqual("Test", LeadEntityResponse.LeadSource);
//            Assert.AreEqual(entity.LeadStatusGuid, LeadEntityResponse.LeadStatusGuid);
//        }

//        [TestMethod]
//        public async Task GetLeadsReturnsCorrectLeads()
//        {
//            // Arrange
//            DateTime now = DateTime.Now;

//            var entity = new List<SFSimpleLeadResults>()
//            {
//                new SFSimpleLeadResults()
//                {
//                    LeadGuid = new Guid(), CustomerGuid = Guid.NewGuid(), BillAccount = "123456789", CMCCustomerID = 1,
//                    CustomerName = "Test Customer", PhoneNumber = "9999999999", Email = "TestEmail@Email.Com",
//                    County = "TestCounty", STAddress = "999 Test st.", ZipCode = "99999"
//                },
//                new SFSimpleLeadResults()
//                {
//                    LeadGuid = new Guid(), CustomerGuid = Guid.NewGuid(), BillAccount = "123456789", CMCCustomerID = 1,
//                    CustomerName = "Test Customer", PhoneNumber = "9999999999", Email = "TestEmail@Email.Com",
//                    County = "TestCounty", STAddress = "999 Test st.", ZipCode = "99999"
//                },
//                new SFSimpleLeadResults()
//                {
//                    LeadGuid = new Guid(), CustomerGuid = Guid.NewGuid(), BillAccount = "123456789", CMCCustomerID = 1,
//                    CustomerName = "Test Customer", PhoneNumber = "9999999999", Email = "TestEmail@Email.Com",
//                    County = "TestCounty", STAddress = "999 Test st.", ZipCode = "99999"
//                },
//                new SFSimpleLeadResults()
//                {
//                    LeadGuid = new Guid(), CustomerGuid = Guid.NewGuid(), BillAccount = "123456789", CMCCustomerID = 1,
//                    CustomerName = "Test Customer", PhoneNumber = "9999999999", Email = "TestEmail@Email.Com",
//                    County = "TestCounty", STAddress = "999 Test st.", ZipCode = "99999"
//                }
//            };
//            var response = new GenericServiceResponse
//            {

//                Success = true,
//                Entity = entity
//            };


//            MoqService.Setup(s => s.GetLead(It.IsAny<SearchLeadSimple>(), It.IsAny<LogCommand>())).ReturnsAsync(response);
//            MoqLogInstance.Setup(s => s.Logger.IsEnabledFor(log4net.Core.Level.Debug)).Returns(false);
//            LeadController controller = new LeadController(MoqService.Object, MoqLogInstance.Object, MoqLogCommandHandlerDecorator.Object);

//            // Act
//            IHttpActionResult actionResult = await controller.GetLeads(It.IsAny<SearchLeadSimple>());
//            var contentResult = actionResult as OkNegotiatedContentResult<GenericServiceResponse>;

//            List<SFSimpleLeadResults> LeadEntityResponse = (List<SFSimpleLeadResults>)contentResult.Content.Entity;
//            UnitTestHelpers.AddTestContextPropertiesFromOkHttpResult(entity, contentResult, LeadEntityResponse, TestContext);

//            //Assert
//            Assert.IsNotNull(contentResult);
//            Assert.AreEqual(true, contentResult.Content.Success);
//            Assert.IsNull(contentResult.Content.OperationException);

//            foreach (var member in LeadEntityResponse)
//            {
//                Assert.AreEqual("123456789", member.BillAccount);
//                Assert.AreEqual(1, member.CMCCustomerID);
//                Assert.AreEqual("Test Customer", member.CustomerName);
//                Assert.AreEqual("9999999999", member.PhoneNumber);
//                Assert.AreEqual("999 Test st.", member.STAddress);
//                Assert.AreEqual("99999", member.ZipCode);
//                Assert.AreEqual("TestCounty", member.County);
//                Assert.AreEqual("TestEmail@Email.Com", member.Email);
//            }
//        }

//        [TestMethod]
//        public async Task GetPrioritizedLeadsReturns5Leads()
//        {
//            //arrange

//            var leadIdList = new List<Guid>
//            {
//                Guid.NewGuid(),
//                Guid.NewGuid(),
//                Guid.NewGuid(),
//                Guid.NewGuid(),
//                Guid.NewGuid()
//            };

//            var expectedEntityList = new List<SfPrioritizedLead>
//            {
//                new SfPrioritizedLead {AerialDistance = 5d, AverageUsage = 104d, Latitude = 40.141056, LeadGuid = leadIdList[0], Longitude = 75.194975},
//                new SfPrioritizedLead {AerialDistance = 6d, AverageUsage = 103d, Latitude = 40.141057, LeadGuid = leadIdList[1], Longitude = 75.194976},
//                new SfPrioritizedLead {AerialDistance = 7d, AverageUsage = 102d, Latitude = 40.141058, LeadGuid = leadIdList[2], Longitude = 75.194977},
//                new SfPrioritizedLead {AerialDistance = 8d, AverageUsage = 101d, Latitude = 40.141059, LeadGuid = leadIdList[3], Longitude = 75.194978},
//                new SfPrioritizedLead {AerialDistance = 9d, AverageUsage = 100d, Latitude = 40.141060, LeadGuid = leadIdList[4], Longitude = 75.194979}
//            };

//            var response = new GenericServiceResponse
//            {
//                Success = true,
//                Entity = expectedEntityList
//            };

//            MoqService.Setup(s => s.GetPrioritizedLeads(It.IsAny<PrioLeadInput>(), It.IsAny<LogCommand>())).ReturnsAsync(response);
//            MoqLogInstance.Setup(s => s.Logger.IsEnabledFor(log4net.Core.Level.Debug)).Returns(false);
//            var controller = new LeadController(MoqService.Object, MoqLogInstance.Object, MoqLogCommandHandlerDecorator.Object);

//            //Act
//            var actionResult = await controller.GetPrioritizedLeads(new PrioLeadInput
//            {
//                NumberOfLeadsToQuery = 1000,
//                NextAppointmentLat = 40.141056,
//                PrevAppointmentLat = 40.141056,
//                NumberOfLeadsToReturn = 5,
//                PostalCodes = new string[1] { "19034" },
//                SfProgramId = string.Empty,
//                PrevAppointmentLong = 75.194975,
//                SfSubProgramId = string.Empty,
//                NextAppointmentLong = 75.194975,
//                AppointmentDateTime = DateTime.Now,
//                WorkType = Guid.NewGuid()

//            });

//            var contentResult = actionResult as OkNegotiatedContentResult<GenericServiceResponse>;
//            var resultEntityList = (List<SfPrioritizedLead>)contentResult.Content.Entity;
//            UnitTestHelpers.AddTestContextPropertiesFromOkHttpResult(expectedEntityList, contentResult, resultEntityList, TestContext);


//            //Verify services repo is called one time
//            MoqService.Verify(s => s.GetPrioritizedLeads(It.IsAny<PrioLeadInput>(), It.IsAny<LogCommand>()), Times.AtLeastOnce);

//            Assert.IsTrue(contentResult.Content.Success);
//            Assert.IsNull(contentResult.Content.OperationException);
//            Assert.IsNotNull(resultEntityList);
//            Assert.AreEqual(expectedEntityList,resultEntityList);

//        }
//        //TODO: waiting on confirmation of model
//        //[TestMethod]
//        //public async Task GetDetailedLeadReturnsDetailedLead()
//        //{
//        //    // Arrange
//        //    DateTime now = DateTime.Now;

//        //    var entity = new SFDetailedLead
//        //    {

//        //    };

//        //    var response = new GenericServiceResponse
//        //    {

//        //        Success = true,
//        //        Entity = entity
//        //    };


//        //    MoqService.Setup(s => s.GetLead(It.IsAny<Guid>(), It.IsAny<LogCommand>())).ReturnsAsync(response);
//        //    MoqLogInstance.Setup(s => s.Logger.IsEnabledFor(log4net.Core.Level.Debug)).Returns(false);
//        //    LeadController controller = new LeadController(MoqService.Object, MoqLogInstance.Object, MoqLogCommandHandlerDecorator.Object);

//        //    // Act
//        //    IHttpActionResult actionResult = await controller.GetLead(entity.LeadId);
//        //    var contentResult = actionResult as OkNegotiatedContentResult<GenericServiceResponse>;

//        //    Lead LeadEntityResponse = (Lead)contentResult.Content.Entity;
//        //    UnitTestHelpers.AddTestContextPropertiesFromOkHttpResult(entity, contentResult, LeadEntityResponse, TestContext);

//        //    //Assert
//        //    Assert.IsNotNull(contentResult);
//        //    Assert.AreEqual(true, contentResult.Content.Success);
//        //    Assert.IsNull(contentResult.Content.OperationException);

//        //    Assert.AreEqual(entity.LeadId, LeadEntityResponse.LeadId);
//        //    Assert.AreEqual("Test", LeadEntityResponse.LeadSource);
//        //    Assert.AreEqual(entity.AuditType, LeadEntityResponse.AuditType);
//        //}
//    }

//}

