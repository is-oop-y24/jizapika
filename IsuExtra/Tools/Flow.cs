using System.Collections.Generic;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class Flow
    {
        public Flow()
        {
            Pairs = new List<Pair>();
        }

        public List<Pair> Pairs { get; }
    }
}