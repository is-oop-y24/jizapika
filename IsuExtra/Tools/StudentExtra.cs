using System.Collections.Generic;
using System.Linq;
using Isu.Tools;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class StudentExtra : Student
    {
        private char _megaFacultyLetter;
        private List<Pair> _timetable;

        public StudentExtra(string name, GroupName groupName, char megaFacultyLetter, List<Pair> timetable, uint id)
            : base(name, groupName)
        {
            _megaFacultyLetter = megaFacultyLetter;
            _timetable = timetable;
            Id = id;
        }

        public uint Id { get; }

        public bool IsTimetableWithNewPairsWillBeCorrect(IEnumerable<Pair> newPairs)
            => _timetable.All(timeTablePair => !newPairs.Any(newPair => timeTablePair.IsCrossWithOtherPair(newPair)));

        public bool CanEnrollToThisOGNP(CourseOGNP courseOGNP)
        {
            if (_megaFacultyLetter == courseOGNP.MegaFacultyLetter) return false;
            return courseOGNP.ImmutableFlows.Any(flow => IsTimetableWithNewPairsWillBeCorrect(flow.ImmutablePairs) && flow.HasFreePlaces());
        }
    }
}