using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using IsuExtra.OldIsu.Tools;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class GroupExtra : Group
    {
        private List<StudentExtra> _students;
        private List<Pair> _timetable;

        public GroupExtra(GroupNameExtra groupName, List<StudentExtra> students, uint maxStudent, char megaFacultyLetter)
            : base(groupName, new List<Student>(students), maxStudent)
        {
            _students = new List<StudentExtra>();
            MegaFacultyLetter = megaFacultyLetter;
            _timetable = new List<Pair>();
            GroupName = groupName;
        }

        public GroupNameExtra GroupName { get; }

        public ImmutableList<StudentExtra> ImmutableStudents => _students.ToImmutableList();
        public ImmutableList<Pair> ImmutableTimeTable => _timetable.ToImmutableList();
        public char MegaFacultyLetter { get; }

        public bool CanAddPair(Time time)
            => !_timetable.Any(pair => pair.IsCrossWithOtherPair(new Pair(GroupName, time, " ", 0)));

        public void AddPair(Pair pair)
        {
            _timetable.Add(pair);
        }

        public void AddStudent(StudentExtra student)
        {
            _students.Add(student);
        }

        public new bool CanAddThisStudent(string studentName)
        {
            foreach (Student curSt in _students)
            {
                if (curSt.GetName() == studentName)
                    return false;
            }

            return _students.Count != MaxSt;
        }
    }
}