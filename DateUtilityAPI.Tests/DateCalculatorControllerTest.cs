using DateUtilityAPI.Controllers;
using DateUtilityAPI.Domain;
using DateUtilityAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace DateUtilityAPI.Tests
{
    [TestClass]
    public class DateCalculatorControllerTest
    {
        [TestMethod]
        public void GetWorkingDaysCount_ValidDateRange_ShouldReturnValue()
        {
            //Arrange
            var dateHelperServiceMock = new Mock<IDateHelperService>();

            dateHelperServiceMock.Setup(r => r.CountWorkingDays(It.IsAny<DateTime>(),It.IsAny<DateTime>())).Returns(2);
            
            var controller = new DateCalculatorController(dateHelperServiceMock.Object);

            var startDate = new DateTime(2020, 1, 5);
            var endDate = new DateTime(2020, 1, 8);

            //Act
            IActionResult result = controller.GetWorkingDaysCount(startDate, endDate);
            var okResult = result as OkObjectResult;

            //Asset
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetWorkingDaysCount_InValidDates_MinValue_ShouldReturnBadRequest()
        {
            //Arrange
            var dateHelperServiceMock = new Mock<IDateHelperService>();

          
            var controller = new DateCalculatorController(dateHelperServiceMock.Object);

            //Act
            IActionResult result = controller.GetWorkingDaysCount(default(DateTime), default(DateTime));
            var badRequestResult = result as BadRequestObjectResult;

            //Asset
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("From Date and To Date must be a valid date", badRequestResult.Value);
        }

        [TestMethod]
        public void GetWorkingDaysCount_InValidDate_Range_ShouldReturnBadRequest()
        {
            //Arrange
            var dateHelperServiceMock = new Mock<IDateHelperService>();
            var controller = new DateCalculatorController(dateHelperServiceMock.Object);
            var startDate = new DateTime(2020, 2, 1);
            var endDate = new DateTime(2020, 1, 1);

            //Act
            IActionResult result = controller.GetWorkingDaysCount(startDate,endDate);
            var badRequestResult = result as BadRequestObjectResult;

            //Asset
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("To Date must be larger than From Date", badRequestResult.Value);
        }


        [TestMethod]
        public void GeStatetWorkingDaysCount_ValidDateRange_ShouldReturnValue()
        {
            //Arrange
            var dateHelperServiceMock = new Mock<IDateHelperService>();

            dateHelperServiceMock.Setup(r => r.CountWorkingDays(It.IsAny<DateTime>(), It.IsAny<DateTime>(),It.IsAny<State>())).Returns(2);

            var controller = new DateCalculatorController(dateHelperServiceMock.Object);

            var startDate = new DateTime(2020, 1, 5);
            var endDate = new DateTime(2020, 1, 8);
            var state = State.NSW;
            //Act
            IActionResult result = controller.GetStateWorkingDaysCount(state,startDate, endDate);
            var okResult = result as OkObjectResult;

            //Asset
            Assert.IsNotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetStateWorkingDaysCount_InValidDates_MinValue_ShouldReturnBadRequest()
        {
            //Arrange
            var dateHelperServiceMock = new Mock<IDateHelperService>();
            var controller = new DateCalculatorController(dateHelperServiceMock.Object);

            //Act
            IActionResult result = controller.GetStateWorkingDaysCount(State.NSW, default(DateTime), default(DateTime));
            var badRequestResult = result as BadRequestObjectResult;

            //Asset
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("From Date and To Date must be a valid date", badRequestResult.Value);
        }

        [TestMethod]
        public void GetStateWorkingDaysCount_InValidDate_Range_ShouldReturnBadRequest()
        {
            //Arrange
            var dateHelperServiceMock = new Mock<IDateHelperService>();
            var controller = new DateCalculatorController(dateHelperServiceMock.Object);
            var startDate = new DateTime(2020, 2, 1);
            var endDate = new DateTime(2020, 1, 1);
            var state = State.NSW;

            //Act
            IActionResult result = controller.GetStateWorkingDaysCount(state, startDate, endDate);
            var badRequestResult = result as BadRequestObjectResult;

            //Asset
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
            Assert.AreEqual("To Date must be larger than From Date", badRequestResult.Value);
        }

    }
}
