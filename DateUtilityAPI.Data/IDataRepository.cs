using DateUtilityAPI.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DateUtilityAPI.Data
{
    public interface IDataRepository
    {
        public List<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate);

        public List<DateTime> GetPublicHolidays(DateTime startDate, DateTime endDate,State state);
    }
}
