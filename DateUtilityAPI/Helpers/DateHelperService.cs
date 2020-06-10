using DateUtilityAPI.Data;
using DateUtilityAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateUtilityAPI.Helpers
{
    public class DateHelperService :IDateHelperService
    {
        private readonly IDataRepository repository;

        public DateHelperService(IDataRepository repository)
        {
            this.repository = repository;
        }

        public bool IsWorkingDay(DateTime currentDate, List<DateTime> datesToExclude)
        {
            // Date is not a weekend or not in the list of public holidays then true else false.

            return !(currentDate.DayOfWeek == DayOfWeek.Saturday ||
                currentDate.DayOfWeek == DayOfWeek.Sunday ||
                (datesToExclude !=null && datesToExclude.Exists(x => x.Date.Equals(currentDate.Date))));
        }

        public int CountWorkingDays(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.AddDays(1); // To exculde startDate from calculation

            if (endDate < startDate) return 0;

            var publicHolidays =  repository.GetPublicHolidays(startDate, endDate);

            var totalDays = endDate.Subtract(startDate).Days;

            var totalWorkingDays = Enumerable.Range(0, totalDays).Count(x => IsWorkingDay(startDate.AddDays(x), publicHolidays));

            return totalWorkingDays;
        }

        public int CountWorkingDays(DateTime startDate, DateTime endDate,State state)
        {
            startDate = startDate.AddDays(1); // To exclude startDate from calculation

            if (endDate < startDate) return 0;

            var publicHolidays = repository.GetPublicHolidays(startDate, endDate,state);

            var totalDays = endDate.Subtract(startDate).Days;

            var totalWorkingDays = Enumerable.Range(0, totalDays).Count(x => IsWorkingDay(startDate.AddDays(x), publicHolidays));

            return totalWorkingDays;
        }
    }
}
