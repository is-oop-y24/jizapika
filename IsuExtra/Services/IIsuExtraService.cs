using System.Collections.Generic;
using System.Collections.Immutable;
using Isu.Services;
using IsuExtra.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Services
{
    public interface IIsuExtraService : IIsuService
    {
        MegaFaculty MakeMegaFaculty(char megaFacultyLetter);
        CourseOGNP AddNewOGNP(MegaFaculty megaFaculty, uint flowSize);
        void EnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student);
        void CancelEnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student);
        ImmutableList<Flow> FlowsInACourseOGNP(CourseOGNP courseOGNP);
        ImmutableList<StudentExtra> StudentsInACourseOGNP(CourseOGNP courseOGNP);
        public List<StudentExtra> StudentsWithoutCoursesOGNP(GroupExtra group);
        public Pair AddPairForGroup(GroupNameExtra groupNameExtra, Time time, string teacher, uint auditory);
    }
}