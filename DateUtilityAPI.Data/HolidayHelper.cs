using DateUtilityAPI.Domain;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;

namespace DateUtilityAPI.Data
{
    /// <summary>
    /// Purpose of this class is to generate Public Holidays for Australia for a given Year
    /// </summary>
    public class HolidayHelper
    {

        public HolidayHelper()
        { 
        
        }
        /// <summary>
        /// Get Next Monday if input date is a weekend else return date as is
        /// </summary>
        /// <param name="currentDate"></param>
        /// <returns>returns next working day</returns>
        private  DateTime NextWeekDayIfWeekend(DateTime currentDate)
        {
            if (currentDate.DayOfWeek == DayOfWeek.Saturday) return currentDate.AddDays(2); // Add 2 days for next Monday 
            if (currentDate.DayOfWeek == DayOfWeek.Sunday) return currentDate.AddDays(1); // Add 1 day for next Monday

            return currentDate; // As is
        }


        /// <summary>
        /// Get next date that falls on a given day of the week starting from input date
        /// </summary>
        /// <param name="currentDate">input date</param>
        /// <param name="day"> day of the week</param>
        /// <returns>return date that falls on a given day of the week</returns>
        private  DateTime GetNextDate(DateTime currentDate, DayOfWeek day)
        {
            while (currentDate.DayOfWeek != day)
                currentDate = currentDate.AddDays(1);

            return currentDate;
        }

        /// <summary>
        /// Get previous date that falls on a given day of the week starting from input date going backword
        /// </summary>
        /// <param name="currentDate">input date</param>
        /// <param name="day">day of the week</param>
        /// <returns>return date that falls on given day of the week</returns>
        private  DateTime GetPreviousDate(DateTime currentDate, DayOfWeek day)
        {
            while (currentDate.DayOfWeek != day)
                currentDate = currentDate.AddDays(-1);
            return currentDate;
        }


        /// <summary>
        /// Retruns an Easter Sunday Date.
        /// Logic based on Algorithum from https://www.codeproject.com/Articles/10860/Calculating-Christian-Holidays
        /// </summary>
        /// <param name="year"></param>
        /// <returns>An Easter Sunday Date</returns>
        private  DateTime GetEasterSunday(int year)
        {
            int g = year % 19;
            int c = year / 100;
            int h = h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25)
                                                + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) *
                        (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            int day = i - ((year + (int)(year / 4) +
                          i + 2 - c + (int)(c / 4)) % 7) + 28;
            int month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }
            return new DateTime(year, month, day);
        }

        /// <summary>
        /// Get Queen's Birthday Date - This is state specific public holiday
        /// https://en.wikipedia.org/wiki/Queen%27s_Official_Birthday
        /// </summary>
        /// <param name="year"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private  DateTime GetQueenBirthday(int year, State state)
        {
            switch (state)
            {
                case State.ACT:
                case State.NSW:
                case State.NT:
                case State.SA:
                case State.TAS:
                case State.VIC:
                    DateTime juneStart = new DateTime(year, 6, 1);
                    return GetNextDate(juneStart, DayOfWeek.Monday).AddDays(7);  //Second Monday in June
                case State.QLD:
                    if (year >= 2016 || year == 2012)
                    {
                        return GetNextDate(new DateTime(year, 10, 1), DayOfWeek.Monday);    //First Monday in October
                    }
                    return GetNextDate(new DateTime(year, 6, 1), DayOfWeek.Monday).AddDays(7); //Second Monday in June
                case State.WA:
                    return GetPreviousDate(new DateTime(year, 9, 30), DayOfWeek.Monday);  //Last Monday of September
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, "Invalid state");
            }
        }

        /// <summary>
        /// Get Labour Day - This is state specific public holiday
        /// https://www.officeholidays.com/holidays/australia/australia-labour-day
        /// </summary>
        /// <param name="year"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        private  DateTime GetLabourDay(int year, State state)
        {
            switch (state)
            {
                case State.ACT:
                case State.NSW:
                case State.SA:
                    return GetNextDate(new DateTime(year, 10, 1), DayOfWeek.Monday); // First Monday in October               
                case State.TAS:
                case State.VIC:
                    return GetNextDate(new DateTime(year, 3, 1), DayOfWeek.Monday).AddDays(7); // Second Monday in March
                case State.NT:
                case State.QLD:
                    return GetNextDate(new DateTime(year, 5, 1), DayOfWeek.Monday); // First Monday in May
                case State.WA:
                    return GetNextDate(new DateTime(year, 3, 1), DayOfWeek.Monday); // First Monday in March
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, "Invalid state");
            }
        }


        public List<DateTime> GetNationalPublicHolidays(int year)
        {
            List<DateTime> publicHolidays = new List<DateTime>();

            //New Year - 01/01/year unless its a weekend then next Monday
            DateTime newYearDate = new DateTime(year, 1, 1);
            newYearDate = NextWeekDayIfWeekend(newYearDate);
            publicHolidays.Add(newYearDate);

            //Australia Day - 26/01/year unless its a weekend then next Monday
            DateTime australiaDay = new DateTime(year, 1, 26);
            australiaDay = NextWeekDayIfWeekend(australiaDay);
            publicHolidays.Add(australiaDay);

            //Easter Friday and Easter Monday
            DateTime easterSunday = GetEasterSunday(year);
            DateTime easterFriday = easterSunday.AddDays(-2);
            DateTime easterMonday = easterSunday.AddDays(1);
            publicHolidays.Add(easterFriday);
            publicHolidays.Add(easterMonday);

            //ANZAC Day - Same Date no matter if its a weekend
            DateTime anzacDayDate = new DateTime(year, 4, 25);
            publicHolidays.Add(anzacDayDate);

            //Christmas Day - 25/12/year unless its a weekend then next Monday 
            DateTime christmasDate = new DateTime(year, 12, 25);
            christmasDate = NextWeekDayIfWeekend(christmasDate);
            publicHolidays.Add(christmasDate);

            //Boxing Day - 26/12/year - Special case when christmas is on weekend
            DateTime boxingDay = new DateTime(year, 12, 26);
            //If christmas falls on weekend then public holiday for christmas is on Monday which makes boxing day on Tuesday.
            bool isBoxingDayOnTuesday = (boxingDay.DayOfWeek == DayOfWeek.Sunday || boxingDay.DayOfWeek == DayOfWeek.Monday );
            boxingDay = NextWeekDayIfWeekend(boxingDay);
            if (isBoxingDayOnTuesday) boxingDay.AddDays(1);
            publicHolidays.Add(boxingDay);

            return publicHolidays;
        }

        public List<DateTime> GetStateSpecificPublicHolidays(int year, State state)
        {
            List<DateTime> publicHolidays = new List<DateTime>();

            if (state == State.All)
            {
                publicHolidays = GetNationalPublicHolidays(year);
            }
            else // Specific States
            {
                #region All States - Common Dates
                
                publicHolidays = GetNationalPublicHolidays(year);

                #endregion

                #region All States - Specific Dates
                // Add Queen's Birthday
                publicHolidays.Add(GetQueenBirthday(year, state));

                // Add Labour Day
                publicHolidays.Add(GetLabourDay(year, state));

                #endregion

                #region Individual State  - ACT
                if (state == State.ACT)
                {
                    //Canberra Day - Second Monday in March
                    // before 2008 it was third Monday in March
                    // This kind of scenario is precisely the reason why this data should be stored in a database table with list of dates as public holidays 
                    // instead of calculating in application logic.
                    DateTime marchStart = new DateTime(year, 3, 1);
                    DateTime canberraDay = GetNextDate(marchStart, DayOfWeek.Monday).AddDays(7); // Add 7 days to get to 2nd Monday
                    if (year < 2008)
                    {
                        canberraDay = canberraDay.AddDays(7);
                    }
                    publicHolidays.Add(canberraDay);

                    // Reconciliation Day - only after 2018 - First Monday after 27th May - Replaced Familiy and Community Day
                    // This kind of scenario is precisely the reason why this data should be stored in a database table with list of dates as public holidays 
                    // instead of calculating in application logic.

                    if (year >= 2018)
                    {
                        DateTime reconDate = new DateTime(year, 5, 27);
                        reconDate = GetNextDate(reconDate, DayOfWeek.Monday);
                        publicHolidays.Add(reconDate);
                    }
                    else
                    {
                        // Family and Community Day - from 2008 to 2017
                        // https://publicholidays.com.au/family-community-day/

                        if (year >= 2007 && year <= 2009)
                        {
                            DateTime familDayDate =  GetNextDate(new DateTime(year, 11, 1), DayOfWeek.Tuesday); // First Tuesday of November
                            publicHolidays.Add(familDayDate);

                        }
                        else  // From 2010 to 2017 - First Monday of September unless it falls on labour day then next Monday
                        {
                            DateTime familyDayDate = GetNextDate(new DateTime(year, 9, 25), DayOfWeek.Monday);
                            if (familyDayDate == GetNextDate(new DateTime(year, 10, 1), DayOfWeek.Monday))
                            {
                                familyDayDate = familyDayDate.AddDays(7);
                            }
                            publicHolidays.Add(familyDayDate);

                        }
                    }

                }

                #endregion

                #region  Individual State  - WA
                if (state == State.WA)
                {
                    // Wastern Australia Day - First Monday of June
                    DateTime juneStart = new DateTime(year, 6, 1);
                    publicHolidays.Add(GetNextDate(juneStart, DayOfWeek.Monday));

                }
                #endregion

                #region Individual State - NT
                if (state == State.NT)
                {
                    //Picnic Day - First Monday of August
                    DateTime augustStart = new DateTime(year, 8, 1);
                    publicHolidays.Add(GetNextDate(augustStart, DayOfWeek.Monday));
                }
                #endregion

                #region Individual State - VIC
                if (state == State.VIC)
                {
                    //Melbourne Cup Day - First Tuesday of November
                    DateTime novStart = new DateTime(year, 11, 1);
                    publicHolidays.Add(GetNextDate(novStart, DayOfWeek.Tuesday));
                }
                #endregion
            }

            return publicHolidays;
        }
    }
}
