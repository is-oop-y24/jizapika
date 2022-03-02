using System.Collections.Generic;
using System.Collections.Immutable;
using IsuExtra.OldIsu.Services;
using IsuExtra.OldIsu.Tools;
using IsuExtra.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.OldIsu.Services
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
        GroupExtra AddGroup(string name, MegaFaculty megaFaculty);
        StudentExtra AddStudent(GroupExtra group, string name);
        new StudentExtra GetStudent(int id);
        new StudentExtra FindStudent(string name);
        new List<StudentExtra> FindStudents(string groupName);
        new List<StudentExtra> FindStudents(CourseNumber courseNumber);

        new GroupExtra FindGroup(string groupName);
        new List<GroupExtra> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(StudentExtra student, GroupExtra newGroup);
    }
}