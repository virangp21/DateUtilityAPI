using DateUtilityAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DateUtilityAPI.Data
{
    public class InMemoryDatabase : IDataRepository
    {
        private readonly HolidayHelper helper;

        public InMemoryDatabase(HolidayHelper helper)
        {
            this.helper = helper;
        }
        /// <summary>
        /// This method returns public holidays
        /// Ideally data should come from a database table but for this exercise public holiday dates are generated using application logic
        /// </summary>
        /// <returns> List of DateTime </returns>
        public List<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate)
        {
            var publicHolidays = new List<DateTime>();
            for (int i = startDate.Year; i <= endDate.Year; i++)
            {
                publicHolidays.AddRange(helper.GetNationalPublicHolidays(i));
            }

            if (publicHolidays.Any(x => x >= startDate && x <= endDate))
            {
                return publicHolidays.Where(x => x >= startDate && x <= endDate).ToList();
            }

            return null;
        }


        /// <summary>
        /// This method returns public holidays for a given state of Australia
        /// Ideally data should come from a database table but for this exercise public holiday dates are generated using application logic
        /// </summary>
        /// <returns> List of DateTime </returns>
        public List<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate,State state)
        {
            var publicHolidays = new List<DateTime>();
            for (int i = startDate.Year; i <= endDate.Year; i++)
            {
                publicHolidays.AddRange(helper.GetStateSpecificPublicHolidays(i,state));
            }

            if (publicHolidays.Any(x => x >= startDate && x <= endDate))
            {
                return publicHolidays.Where(x => x >= startDate && x <= endDate).ToList();
            }
            return null;
        }
    }
}
