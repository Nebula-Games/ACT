using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Core.Interfaces.Text
{
    public interface I_PatternEngine
    {
        SortedList<int,string> Patterns { get; set; }
        bool AddPattern(string Pattern);
        bool RemovePattern(string Pattern);
        bool RemovePattern(int PatternIndex);
        uint MovePatternUp(string Pattern);
        uint MovePatternUp(int PatternIndex);
        uint MovePatternDown(string Pattern);
        uint MovePatternDown(int PatternIndex);
        SortedList<uint, uint> ProcessText(string Text, bool IgnoreCase = true);
        bool HasPattern(string Text, bool IgnoreCase = true);
        SortedList<uint, uint> ProcessText(string Text, bool IgnoreCase = true, bool OnlyCaptureLastPattern = false);
        bool HasPattern(string Text, bool IgnoreCase = true, uint PatternsToTestUpTo = 0);

    }
}
