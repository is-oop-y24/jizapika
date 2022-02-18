using System.Linq;
using Isu.Services;
using Isu.Tools;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        [TestCase("Lev", "M3202")]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent(string name, string groupname)
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                Group group = isu.AddGroup(groupname);
                Student student = isu.AddStudent(group, name);
                group = isu.FindGroup(groupname);
                bool t = @group.Get_stData().Any(curSt => curSt == student);

                if (!t) Assert.Fail("The group doesn't include this student.");
                if (student.GetGroup() != group.Name()) Assert.Fail("The student hasn't this group.");
            });
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                var isu = new IsuService();
                isu.Change_maxSt(3);
                Group group = isu.AddGroup("M3202");
                isu.AddStudent(group, "Lev");
                isu.AddStudent(group, "Sergo");
                isu.AddStudent(group, "Artem");
                isu.AddStudent(group, "Ruslan");
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
                var isu = new IsuService();
                isu.NewCourseLimit(minCourse, maxCourse);
                isu.AddGroup(groupName);
            });
        }

        [TestCase("M3202", "M3203")]
        public void TransferStudentToAnotherGroup_GroupChanged(string name1, string name2)
        {
            if (name1 == name2) throw new IsuException("Группы должны быть разными.");
            var isu = new IsuService();
            isu.Change_maxSt(3);
            Group group1 = isu.AddGroup(name1);
            Group group2 = isu.AddGroup(name2);
            Student lev = isu.AddStudent(group1, "Lev");
            isu.ChangeStudentGroup(lev, group2);
            foreach (Student curSt in group2.Get_stData())
            {
                Assert.IsTrue(curSt.GetName() == "Lev");
            }
        }
    }
}