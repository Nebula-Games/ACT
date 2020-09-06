using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Enums.DateTimes
{
    [Flags()]
    public enum DayOfWeek
    {
        monday,tuesday,wednesday,thursday,friday,saturday,sunday
    }

    [Flags()]
    public enum Months
    {
        jan,feb,mar,apr,may,jun,jul,aug,sep,oct,nov,dec
    }

    [Flags()]
    public enum Week
    {
        first,second,third,fourth
    }

    [Flags()]
    public enum HourIndicator
    {
        am,pm
    }
}
