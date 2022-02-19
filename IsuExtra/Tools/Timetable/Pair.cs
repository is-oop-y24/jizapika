using System;
using IsuExtra.OldIsu.Tools;

namespace IsuExtra.Tools.Timetable
{
    public class Pair
    {
        private GroupNameExtra _groupNameExtra;
        private Time _time;
        private string _teacher;
        private uint _auditory;
        private Guid _id;

        public Pair(GroupNameExtra groupNameExtra, Time time, string teacher, uint auditory)
        {
            _groupNameExtra = groupNameExtra;
            _time = time;
            _teacher = teacher;
            _auditory = auditory;
            _id = Guid.NewGuid();
        }

        public bool IsCrossWithOtherPair(Pair pair) => pair._time == _time;
        public bool IsIdsTheSame(Pair pair) => pair._id == _id;
    }
}