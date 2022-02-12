using Isu.Tools;

namespace IsuExtra.Tools.Timetable
{
    public class Pair
    {
        private GroupNameExtra _groupNameExtra;
        private Time _time;
        private string _teacher;
        private uint _auditory;

        public Pair(GroupNameExtra groupNameExtra, Time time, string teacher, uint auditory)
        {
            _groupNameExtra = groupNameExtra;
            _time = time;
            _teacher = teacher;
            _auditory = auditory;
        }

        public bool IsCrossWithOtherPair(Pair pair) => pair._time == _time;
    }
}