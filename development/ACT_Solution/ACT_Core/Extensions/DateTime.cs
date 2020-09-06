// ***********************************************************************
// Assembly         : ACT_Core
// Author           : MarkAlicz
// Created          : 02-26-2019
//
// Last Modified By : MarkAlicz
// Last Modified On : 03-15-2019
// ***********************************************************************
// <copyright file="DateTime.cs" company="Stonegate Intel LLC">
//     Copyright ©  2019
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ACT.Core.Extensions
{
    /// <summary>
    /// DateTime Extension Methods
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// Convert from UNIX time to Standard Time
        /// </summary>
        /// <param name="unixTime">Unix Time Seconds Since 1/1/1970 0:0:0</param>
        /// <returns>DateTime</returns>
        /// <seealso cref="System.DateTime" />
        public static DateTime FromUnixTime(this ulong unixTime)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return epoch.AddSeconds(unixTime);
        }

        /// <summary>
        /// Converts the DateTime to UNIX TIME (1/1/1970 0:0:0)
        /// </summary>
        /// <param name="date">Date to Convert</param>
        /// <returns>System.UInt64.</returns>
        public static ulong ToUnixTime(this DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToUInt64((date - epoch).TotalSeconds);
        }

        /// <summary>
        /// Calculate total seconds from the 2 dates
        /// </summary>
        /// <param name="Date1">Date1</param>
        /// <param name="Date2">Date2</param>
        /// <returns>System.Int32.</returns>
        public static int TotalSeconds(this DateTime Date1, DateTime Date2)
        {
            return (Date1 - Date2).TotalSeconds.ToString().SplitString(".", StringSplitOptions.RemoveEmptyEntries).First().ToIntFast();
        }

        /// <summary>
        /// Determines whether the specified date2 is before.
        /// </summary>
        /// <param name="Date1">The date1.</param>
        /// <param name="Date2">The date2.</param>
        /// <returns><c>true</c> if the specified date2 is before; otherwise, <c>false</c>.</returns>
        public static bool IsBefore(this DateTime Date1, DateTime Date2)
        {
            if ((Date1 - Date2).TotalSeconds >= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// Returns the Total Number Of Months (Always Positive)
        /// </summary>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns>Int Number Of Monthsnetflix</returns>
        public static int TotalMonths(this DateTime start, DateTime end)
        {
            return Math.Abs((start.Year * 12 + start.Month) - (end.Year * 12 + end.Month));
        }


        /// <summary>
        /// Totals the age years.
        /// </summary>
        /// <param name="DOB">The dob.</param>
        /// <returns>System.Int32.</returns>
        public static int TotalAgeYears(this DateTime DOB)
        {
            var _AgeData = DOB.ToAge();
            return _AgeData.years;
        }

        /// <summary>
        /// Converts to age.
        /// </summary>
        /// <param name="DOB">The dob.</param>
        /// <returns>System.ValueTuple&lt;System.Int32, System.Int32, System.Int32&gt;.</returns>
        public static (int years, int months, int days) ToAge(this DateTime DOB)
        {
            DateTime today = DateTime.Today;

            int months = today.Month - DOB.Month;
            int years = today.Year - DOB.Year;

            if (today.Day < DOB.Day)
            {
                months--;
            }

            if (months < 0)
            {
                years--;
                months += 12;
            }

            int days = (today - DOB.AddMonths((years * 12) + months)).Days;

            return (years, months, days);
        }

        /// <summary>
        /// Calculates the age.
        /// </summary>
        /// <param name="BDate">The b date.</param>
        /// <param name="EndDate">The end date.</param>
        /// <returns>System.Int32.</returns>
        public static int CalculateAge(this DateTime BDate, DateTime? EndDate = null)
        {
            if (EndDate == null) { EndDate = DateTime.Today; }
                        
            int age = EndDate.Value.Year - BDate.Year;
            if (BDate > EndDate.Value.AddYears(-age)) age--;
            return age;
        }

        /// <summary>
        /// Returns the FULL Month with Zeros i.e 01,02,09,10,11,12
        /// </summary>
        /// <param name="d">Date to Use</param>
        /// <returns>2 Digit Month</returns>
        public static string FullMonth(this DateTime d)
        {
            int m = d.Month;
            if (m < 10) { return "0" + m.ToString(); }
            else { return m.ToString(); }
        }

        /// <summary>
        /// Returns the FULL Day with Zeros i.e 01,02,09,10,11,12
        /// </summary>
        /// <param name="d">Date to Use</param>
        /// <returns>2 Digit Day</returns>
        public static string FullDay(this DateTime d)
        {
            int m = d.Day;
            if (m < 10) { return "0" + m.ToString(); }
            else { return m.ToString(); }
        }

        /// <summary>
        /// Short Date Full = 01/02/2019  NOT 1/2/2018
        /// </summary>
        /// <param name="d">Date to Use</param>
        /// <returns>string</returns>
        public static string ToShortDateFull(this DateTime d)
        {
            return d.ToString("d", System.Globalization.CultureInfo.CreateSpecificCulture("es-ES"));
        }
    }
}
