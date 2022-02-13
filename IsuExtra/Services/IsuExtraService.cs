using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using Isu.Exceptions;
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
        private uint _maxCoursesOGNPForStudents;

        public IsuExtraService(uint maxCoursesOGNPForStudents)
        {
            _megaFaculties = new List<MegaFaculty>();
            _groupsExtra = new List<GroupExtra>();
            _maxCoursesOGNPForStudents = maxCoursesOGNPForStudents;
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
        }=

        public GroupExtra AddGroup(string name, MegaFaculty megaFaculty)
        {
            var newGr = new GroupExtra(new GroupNameExtra(name, MinNumCourse, MaxNumCourse), new List<Student>(), MaxSt, megaFaculty.MegaFacultyLetter);
            _groupsExtra.Add(newGr);
            return newGr;
        }

        public StudentExtra AddStudent(GroupExtra group, string name)
        {
            foreach (GroupExtra cur in _groupsExtra)
            {
                if (cur.GroupName == group.GroupName && group.CanAddThisStudent(name))
                {
                    var student = new StudentExtra(
                        name,
                        group.GroupName,
                        group.MegaFacultyLetter,
                        group.ImmutableTimeTable.ToList(),
                        IdSt,
                        _maxCoursesOGNPForStudents);
                    group.AddStudent(student);
                    return student;
                }
            }

            throw new IsuExtraException($"Студент не может быть добавлен.");
        }

        public new StudentExtra GetStudent(int id)
        {
            foreach (GroupExtra curGr in _groupsExtra)
            {
                foreach (StudentExtra curSt in curGr.ImmutableStudents)
                {
                    if (curSt.Id == id) return curSt;
                }
            }

            throw new IsuExtraException($"Студента с id {id} нет в базе.");
        }

        public new StudentExtra FindStudent(string name)
        {
            foreach (GroupExtra curGr in _groupsExtra)
            {
                foreach (StudentExtra curSt in curGr.ImmutableStudents)
                {
                    if (curSt.GetName() == name) return curSt;
                }
            }

            throw new IsuExtraException($"Студента {name} нет в базе.");
        }

        public new List<StudentExtra> FindStudents(string groupName)
        {
            foreach (GroupExtra curGr in _groupsExtra)
            {
                if (curGr.GroupName.GetName().ToString() == groupName) return curGr.ImmutableStudents.ToList();
            }

            throw new IsuException($"Группы {groupName} нет в базе.");
        }

        public new List<StudentExtra> FindStudents(CourseNumber courseNumber)
        {
            var studentsOnCourse = new List<StudentExtra>();
            foreach (GroupExtra curGr in _groupsExtra)
            {
                if (curGr.GroupName.Course() == courseNumber.Get_num())
                {
                    studentsOnCourse.AddRange(curGr.ImmutableStudents.ToList());
                }
            }

            return studentsOnCourse;
        }

        public new GroupExtra FindGroup(string groupName)
        {
            foreach (GroupExtra curGr in _groupsExtra)
            {
                if (curGr.GroupName.GetName().ToString() == groupName) return curGr;
            }

            throw new IsuException($"Группы {groupName} нет в базе.");
        }

        public new List<GroupExtra> FindGroups(CourseNumber courseNumber)
            => _groupsExtra.Where(curGr => curGr.GroupName.Course() == courseNumber.Get_num()).ToList();

        public void KickStudent(StudentExtra student)
        {
            var stData = new List<StudentExtra>();
            foreach (GroupExtra curGr in _groupsExtra)
            {
                foreach (StudentExtra curSt in curGr.ImmutableStudents)
                {
                    if (curSt == student)
                    {
                        stData = curGr.ImmutableStudents.ToList();
                        var groupName = new GroupNameExtra(
                            curGr.GroupName.GetName().ToString(), MinNumCourse, MaxNumCourse);
                        stData.Remove(curSt);
                        _groupsExtra.Remove(curGr);
                        _groupsExtra.Add(new GroupExtra(groupName, stData, MaxSt));
                        return;
                    }
                }
            }

            throw new IsuException($"Этого студента не было в базе.");
        }

        public void ChangeStudentGroup(StudentExtra student, GroupExtra newGroup)
        {
            KickStudent(student);
            AddStudent(newGroup, student.GetName());
        }
    }
}