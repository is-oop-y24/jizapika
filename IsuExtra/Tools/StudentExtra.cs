using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Isu.Tools;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class StudentExtra : Student
    {
        private char _megaFacultyLetter;
        private List<Pair> _timetable;
        private List<CourseOGNP> _coursesOGNP;
        private uint _maxCoursesOGNP;

        public StudentExtra(string name, GroupName groupName, char megaFacultyLetter, List<Pair> timetable, uint id, uint maxCoursesOGNP)
            : base(name, groupName, id)
        {
            _megaFacultyLetter = megaFacultyLetter;
            _timetable = timetable;
            _coursesOGNP = new List<CourseOGNP>();
            Id = id;
            _maxCoursesOGNP = maxCoursesOGNP;
        }

        public uint Id { get; }
        public ImmutableList<CourseOGNP> CoursesOGNP => _coursesOGNP.ToImmutableList();

        public bool IsTimetableWithNewPairsWillBeCorrect(IEnumerable<Pair> newPairs)
            => _timetable.All(timeTablePair => !newPairs.Any(newPair => timeTablePair.IsCrossWithOtherPair(newPair)));

        public bool CanEnrollToThisOGNP(CourseOGNP courseOGNP)
        {
            if (_megaFacultyLetter == courseOGNP.MegaFacultyLetter) return false;
            if (_coursesOGNP.Count >= _maxCoursesOGNP) return false;
            foreach (CourseOGNP course in _coursesOGNP)
            {
                if (course.IsIdsTheSame(courseOGNP)) return false;
            }

            return courseOGNP.ImmutableFlows.Any(flow =>
                IsTimetableWithNewPairsWillBeCorrect(flow.ImmutablePairs) && flow.HasFreePlaces());
        }

        public void AddCourseOGNP(CourseOGNP courseOGNP)
        {
            _coursesOGNP.Add(courseOGNP);
        }

        public bool IsWithoutAllCoursesOGNP()
            => _coursesOGNP.Count == _maxCoursesOGNP;
    }
}