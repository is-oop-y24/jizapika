using System.Collections.Generic;
using Isu.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class StudentExtra : Student
    {
        private MegaFaculty _megaFaculty;
        private List<Pair> _timetable;

        public StudentExtra(string name, GroupName groupName, MegaFaculty megaFaculty, List<Pair> timetable)
            : base(name, groupName)
        {
            _megaFaculty = megaFaculty;
            _timetable = timetable;
        }
    }
}