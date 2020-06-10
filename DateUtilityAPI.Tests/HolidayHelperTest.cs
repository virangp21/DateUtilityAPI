using DateUtilityAPI.Data;
using DateUtilityAPI.Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DateUtilityAPI.Tests
{
    [TestClass]
    public class HolidayHelperTest
    {
        [TestMethod]
        public void GetNationalPublicHolidays_ValidYear_Should_Return_List()
        {
            //Arrange
            var holidayHealper = new HolidayHelper();
            var year = 2020;
            //Act
            var result = holidayHealper.GetNationalPublicHolidays(year);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(7, result.Count);

            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 1, 1))); // New Year
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 1, 27))); // Australia Day
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 4, 10))); // Easter Friday
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 4, 13))); // Easter Monday
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 4, 25))); // ANZAC Day
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 12, 25))); // Christmas Day
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 12, 28))); // Boxing Day
        }

        [TestMethod]
        public void GetStateSpecificPublicHolidays_ValidYear_State_Should_Return_List()
        {
            //Arrange
            var holidayHealper = new HolidayHelper();
            var year = 2020;
            var state = State.ACT;
            //Act
            var result = holidayHealper.GetStateSpecificPublicHolidays(year,state);

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(11, result.Count);

            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 1, 1))); // New Year
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 1, 27))); // Australia Day
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 4, 10))); // Easter Friday
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 4, 13))); // Easter Monday
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 4, 25))); // ANZAC Day
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 12, 25))); // Christmas Day
            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 12, 28))); // Boxing Day

            Assert.IsTrue(result.Any(x => x == new DateTime(2020, 3, 9))); // Canberra Day
          
        }
    }
}
