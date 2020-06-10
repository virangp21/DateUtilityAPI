using DateUtilityAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DateUtilityAPI.Helpers
{
    public interface IDateHelperService
    {
        public bool IsWorkingDay(DateTime currentDate, List<DateTime> datesToExclude);

        public int CountWorkingDays(DateTime startDate, DateTime endDate);

        public int CountWorkingDays(DateTime startDate, DateTime endDate, State state);
    }
}
