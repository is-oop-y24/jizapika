using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Isu.Tools;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class GroupExtra : Group
    {
        private List<StudentExtra> _students;
        private List<Pair> _timetable;

        public GroupExtra(GroupNameExtra groupName, List<Student> students, uint maxStudent, char megaFacultyLetter)
            : base(groupName, students, maxStudent)
        {
            _students = new List<StudentExtra>();
            MegaFacultyLetter = megaFacultyLetter;
            _timetable = new List<Pair>();
            GroupName = groupName;
        }

        public GroupNameExtra GroupName { get; }

        public ImmutableList<StudentExtra> ImmutableStudents => _students.ToImmutableList();
        public char MegaFacultyLetter { get; }

        public bool CanAddPair(Time time)
            => !_timetable.Any(pair => pair.IsCrossWithOtherPair(new Pair(GroupName, time, " ", 0)));

        public void AddPair(Pair pair)
        {
            _timetable.Add(pair);
        }
    }
}