using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Types.Common
{
    public class ACT_ScheduleData
    {
        public string Name { get; set; }
        public bool Daily { get; set; }
        public List<ACT.Core.Enums.DateTimes.DayOfWeek> DayOfWeek { get; set; }

        public int Hour { get; set; }
        public int Minute { get; set; }
        public ACT.Core.Enums.DateTimes.HourIndicator Indicator { get; set; }

        public bool Weekly { get; set; }
        public List<ACT.Core.Enums.DateTimes.Week> WeekSelection { get; set; }

        public bool Monthly { get; set; }
        public List<ACT.Core.Enums.DateTimes.Months> MonthSelection { get; set; }

        public string ToJSON()
        {
            return "";
        }

        public void FromJSON(string JSON) { }
    }
}
