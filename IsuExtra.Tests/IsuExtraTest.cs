using IsuExtra.OldIsu.Exceptions;
using IsuExtra.Exceptions;
using IsuExtra.OldIsu.Services;
using IsuExtra.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class Tests
    {
        [TestCase("Lev", "M3202")]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string name, string groupname)
        {
            var isu = new IsuExtraService(2);
            MegaFaculty megaFacultyM = isu.MakeMegaFaculty('M');
            GroupExtra group = isu.AddGroup(groupname, megaFacultyM);
            StudentExtra student = isu.AddStudent(group, name);
            group = isu.FindGroup(groupname);
            foreach (StudentExtra curSt in group.ImmutableStudents)
            {
                if (curSt == student)
                {
                    break;
                }
            }
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                var isu = new IsuExtraService(2);
                isu.Change_maxSt(3);
                MegaFaculty megaFacultyM = isu.MakeMegaFaculty('M');
                GroupExtra group = isu.AddGroup("M3202", megaFacultyM);
                StudentExtra lev = isu.AddStudent(group, "Lev");
                StudentExtra sergo = isu.AddStudent(group, "Sergo");
                StudentExtra artem = isu.AddStudent(group, "Artem");
                StudentExtra ruslan = isu.AddStudent(group, "Ruslan");
            });
        }

        [TestCase("M32-2", 1, 4)]
        [TestCase("M3-02", 1, 4)]
        [TestCase("M4122", 1, 4)]
        [TestCase("R3722", 1, 10)]
        [TestCase("M3902", 1, 7)]
        public void CreateGroupWithInvalidName_ThrowException(string groupName, int minCourse, int maxCourse)
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuExtraService(2);
                MegaFaculty megaFacultyM = isu.MakeMegaFaculty('M');
                isu.NewCourseLimit(minCourse, maxCourse);
                GroupExtra group = isu.AddGroup(groupName, megaFacultyM);
            });
        }

        [TestCase("M3202", "M3203")]
        public void TransferStudentToAnotherGroup_GroupChanged(string name1, string name2)
        {
            var isu = new IsuExtraService(2);
            MegaFaculty megaFacultyM = isu.MakeMegaFaculty('M');
            GroupExtra group1 = isu.AddGroup(name1, megaFacultyM);
            GroupExtra group2 = isu.AddGroup(name2, megaFacultyM);
            StudentExtra lev = isu.AddStudent(group1, "Lev");
            isu.ChangeStudentGroup(lev, group2);
            foreach (StudentExtra curSt in group2.ImmutableStudents)
            {
                Assert.IsTrue(curSt.GetName() == "Lev");
            }
        }

        [TestCase("M3202", 1, 4, 'R')]
        [TestCase("R3722", 1, 10, 'M')]
        public void CreateGroupWithNotCorrectNameForMegaFaculty_ThrowException(string groupName, int minCourse, int maxCourse, char MegaFacultyLetter)
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuExtraService(2);
                MegaFaculty megaFaculty = isu.MakeMegaFaculty(MegaFacultyLetter);
                isu.NewCourseLimit(minCourse, maxCourse);
                isu.AddGroup(groupName, megaFaculty);
            });
        }

        [Test]
        public void AddStudentToCourseOGNPHisMegaFaculty_ThrowException()
        {
            Assert.Catch<IsuExtraException>(() =>
            {
                var isu = new IsuExtraService(2);
                MegaFaculty megaFacultyM = isu.MakeMegaFaculty('M');
                GroupExtra group = isu.AddGroup("M3202", megaFacultyM);
                CourseOGNP courseOGNP = isu.AddNewOGNP(megaFacultyM, 3);
                StudentExtra student = isu.AddStudent(group, "student");
                isu.EnrollStudentToOGNP(courseOGNP, student);
            });
        }
    }
}