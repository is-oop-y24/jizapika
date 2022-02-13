using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Isu.Services;
using Isu.Tools;
using IsuExtra.Exceptions;
using IsuExtra.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Services
{
    public class IsuExtraService : IsuService, IIsuExtraService
    {
        private List<MegaFaculty> _megaFaculties;
        private List<GroupExtra> _groupsExtra;

        public IsuExtraService()
        {
            _megaFaculties = new List<MegaFaculty>();
            _groupsExtra = new List<GroupExtra>();
        }

        public ImmutableList<MegaFaculty> MegaFaculties => _megaFaculties.ToImmutableList();
        public MegaFaculty MakeMegaFaculty(char megaFacultyLetter)
        {
            if (_megaFaculties.Any(megaFaculty => megaFaculty.MegaFacultyLetter == megaFacultyLetter))
            {
                throw new IsuExtraException("The letter is already taken");
            }

            return new MegaFaculty(megaFacultyLetter);
        }

        public CourseOGNP AddNewOGNP(MegaFaculty megaFaculty, uint flowSize)
            => megaFaculty.MakeOGNPCourse(flowSize);

        public void EnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student)
        {
            if (student.CanEnrollToThisOGNP(courseOGNP))
                courseOGNP.AddStudent(student);
            else throw new IsuExtraException("Student can't enroll to this OGNP.");
        }

        public void CancelEnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student)
        {
            if (courseOGNP.HasStudent(student))
                courseOGNP.RemoveStudent(student);
            else throw new IsuExtraException("Student wasn't on this course.");
        }

        public ImmutableList<Flow> FlowsInACourseOGNP(CourseOGNP courseOGNP)
            => courseOGNP.ImmutableFlows;

        public ImmutableList<StudentExtra> StudentsInACourseOGNP(CourseOGNP courseOGNP)
            => FlowsInACourseOGNP(courseOGNP).SelectMany(flow => flow.ImmutableStudents.ToList()).ToImmutableList();

        public List<StudentExtra> StudentsWithoutCoursesOGNP(GroupExtra group)
        {
            var students = new List<StudentExtra>();
            foreach (StudentExtra student in group.ImmutableStudents)
            {
                if (student.IsWithoutAllCoursesOGNP()) students.Add(student);
            }

            return students;
        }

        public Pair AddPairForGroup(GroupNameExtra groupNameExtra, Time time, string teacher, uint auditory)
        {
            foreach (GroupExtra group in _groupsExtra)
            {
                if (group.GroupName == groupNameExtra && group.CanAddPair(time))
                {
                    var pair = new Pair(groupNameExtra, time, teacher, auditory);
                    group.AddPair(pair);
                    return pair;
                }
            }

            throw new IsuExtraException("Pair can't add.");
        }
    }
}