using System;
using System.Collections.Generic;
using System.Text;

namespace ACT.Plugins.Text
{
    public class ACT_PatternEngine : ACT.Core.Interfaces.Text.I_PatternEngine
    {
        public SortedList<int, string> Patterns { get; set; }

        public bool AddPattern(string Pattern)
        {
            try
            {
                if (Patterns == null) { Patterns = new SortedList<int, string>(); }
                Patterns.Add(Patterns.Count + 1, Pattern);
                return true;
            }
            catch
            {
                //todo Log Error
                return false;
            }
        }

        public bool HasPattern(string Text, bool IgnoreCase = true)
        {
            throw new NotImplementedException();
        }

        public bool HasPattern(string Text, bool IgnoreCase = true, uint PatternsToTestUpTo = 0)
        {
            throw new NotImplementedException();
        }

        public uint MovePatternDown(string Pattern)
        {
            throw new NotImplementedException();
        }

        public uint MovePatternDown(int PatternIndex)
        {
            throw new NotImplementedException();
        }

        public uint MovePatternUp(string Pattern)
        {
            throw new NotImplementedException();
        }

        public uint MovePatternUp(int PatternIndex)
        {
            throw new NotImplementedException();
        }

        public SortedList<uint, uint> ProcessText(string Text, bool IgnoreCase = true)
        {
            throw new NotImplementedException();
        }

        public SortedList<uint, uint> ProcessText(string Text, bool IgnoreCase = true, bool OnlyCaptureLastPattern = false)
        {
            throw new NotImplementedException();
        }

        public bool RemovePattern(string Pattern)
        {
            throw new NotImplementedException();
        }

        public bool RemovePattern(int PatternIndex)
        {
            throw new NotImplementedException();
        }
    }
}
