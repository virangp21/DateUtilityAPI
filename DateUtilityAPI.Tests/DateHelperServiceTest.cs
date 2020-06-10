using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Moq;
using DateUtilityAPI.Data;
using DateUtilityAPI.Helpers;
using DateUtilityAPI.Domain;

namespace DateUtilityAPI.Tests
{
    [TestClass]
    public class DateHelperServiceTest
    {
        [TestMethod]
        public void IsWorkingDay_NonWorkingDay_ShouldReturnFalse()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);
          

            var currentDate = new DateTime(2020, 1, 4);
            var lstDatesToExclued = new List<DateTime>();

            //Act
            var result = dateHeplperService.IsWorkingDay(currentDate,lstDatesToExclued);

            //Asset
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void IsWorkingDay_WorkingDay_ShouldReturnTrue()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);


            var currentDate = new DateTime(2020, 1, 8);
            var lstDatesToExclued = new List<DateTime>();

            //Act
            var result = dateHeplperService.IsWorkingDay(currentDate, lstDatesToExclued);

            //Asset
            Assert.IsTrue(result);
        }


        [TestMethod]
        public void IsWorkingDay_ExcludedDate_ShouldReturnFalse()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);


            var currentDate = new DateTime(2020, 1, 1);
            var lstDatesToExclued = new List<DateTime>() { new DateTime(2020,1,1) };

            //Act
            var result = dateHeplperService.IsWorkingDay(currentDate, lstDatesToExclued);

            //Asset
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CountWorkingDays_Valid_DateRange_Should_Return_Result()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);
            repositoryMock.Setup(r => r.GetPublicHolidays(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<DateTime>() { });
            var startDate = new DateTime(2020, 1, 6);
            var endDate = new DateTime(2020, 1, 12);
            //Act
           var result =  dateHeplperService.CountWorkingDays(startDate, endDate);

            //Assert
            Assert.AreEqual(4, result);
        }


        [TestMethod]
        public void CountWorkingDays_Valid_DateRange_With_Excluded_Dates_Should_Return_Result()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);
            repositoryMock.Setup(r => r.GetPublicHolidays(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<DateTime>() { new DateTime(2020,1,7) });
            var startDate = new DateTime(2020, 1, 6);
            var endDate = new DateTime(2020, 1, 12);
            //Act
            var result = dateHeplperService.CountWorkingDays(startDate, endDate);

            //Assert
            Assert.AreEqual(3, result);
        }


        [TestMethod]
        public void CountWorkingDays_InValid_DateRange_Should_Return_Zero()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);
            repositoryMock.Setup(r => r.GetPublicHolidays(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(new List<DateTime>() { });
            var startDate = new DateTime(2020, 1, 14);
            var endDate = new DateTime(2020, 1, 8);
            //Act
            var result = dateHeplperService.CountWorkingDays(startDate, endDate);

            //Assert
            Assert.AreEqual(0, result);
        }


        [TestMethod]
        public void CountWorkingDays_Valid_DateRange_With_State_Should_Return_Result()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);
            repositoryMock.Setup(r => r.GetPublicHolidays(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<State>())).Returns(new List<DateTime>() { });
            var startDate = new DateTime(2020, 1, 6);
            var endDate = new DateTime(2020, 1, 12);
            var state = State.NSW;
            //Act
            var result = dateHeplperService.CountWorkingDays(startDate, endDate,state);

            //Assert
            Assert.AreEqual(4, result);
        }

        [TestMethod]
        public void CountWorkingDays_Valid_DateRange_With_State_And_Excluded_Dates_Should_Return_Result()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);
            repositoryMock.Setup(r => r.GetPublicHolidays(It.IsAny<DateTime>(), It.IsAny<DateTime>(),It.IsAny<State>())).Returns(new List<DateTime>() { new DateTime(2020,1,7) });
            var startDate = new DateTime(2020, 1, 6);
            var endDate = new DateTime(2020, 1, 12);
            var state = State.NSW;
            //Act
            var result = dateHeplperService.CountWorkingDays(startDate, endDate, state);

            //Assert
            Assert.AreEqual(3, result);
        }


        [TestMethod]
        public void CountWorkingDays_InValid_DateRange_With_State_Should_Return_Zero()
        {
            //Arrange
            var repositoryMock = new Mock<IDataRepository>();
            var dateHeplperService = new DateHelperService(repositoryMock.Object);
            repositoryMock.Setup(r => r.GetPublicHolidays(It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<State>())).Returns(new List<DateTime>() { });
            var startDate = new DateTime(2020, 1, 12);
            var endDate = new DateTime(2020, 1, 6);
            var state = State.NSW;
            //Act
            var result = dateHeplperService.CountWorkingDays(startDate, endDate,state);

            //Assert
            Assert.AreEqual(0, result);
        }

    }
}
