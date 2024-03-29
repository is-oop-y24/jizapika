namespace IsuExtra.Tools.Timetable
{
    public class Time
    {
        private DayOfTimetable _dayOfTimetable;
        private PairNumber _pairNumber;
        public Time(DayOfTimetable dayOfTimetable, PairNumber pairNumber)
        {
            _dayOfTimetable = dayOfTimetable;
            _pairNumber = pairNumber;
        }

        public bool IsSameTime(Time time)
            => time._dayOfTimetable == _dayOfTimetable && time._pairNumber == _pairNumber;
    }
}