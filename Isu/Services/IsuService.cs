using System.Collections.Generic;
using System.Text;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private int _idSt = 1;
        private List<Group> _grData;
        private uint _maxSt = 30;
        private int _maxNumCourse = 9;
        private int _minNumCourse = 1;

        public IsuService()
        {
            _grData = new List<Group>();
        }

        public Group AddGroup(string name)
        {
            var newGr = new Group(new GroupName(name, _minNumCourse, _maxNumCourse), new List<Student>(), _maxSt);
            _grData.Add(newGr);
            return newGr;
        }

        public Student AddStudent(Group group, string name)
        {
            int grNum = -1;
            int curPos = 0;
            foreach (Group cur in _grData)
            {
                if (cur.Name() == group.Name()) grNum = curPos;
                curPos++;
            }

            if (grNum == -1) throw new IsuException($"Группы {group.Name()} не существует.");
            var st = _grData[grNum].AddStudent(name, _idSt);
            _idSt++;
            return st;
        }

        public Student GetStudent(int id)
        {
            foreach (Group curGr in _grData)
            {
                foreach (Student curSt in curGr.Get_stData())
                {
                    if (curSt.Get_id() == id) return curSt;
                }
            }

            throw new IsuException($"Студента с id {id} нет в базе.");
        }

        public Student FindStudent(string name)
        {
            foreach (Group curGr in _grData)
            {
                foreach (Student curSt in curGr.Get_stData())
                {
                    if (curSt.GetName() == name) return curSt;
                }
            }

            throw new IsuException($"Студента {name} нет в базе.");
        }

        public List<Student> FindStudents(string groupName)
        {
            var buildingGroupName = new StringBuilder(groupName);
            foreach (Group curGr in _grData)
            {
                if (curGr.Name().GetName() == buildingGroupName) return curGr.Get_stData();
            }

            throw new IsuException($"Группы {groupName} нет в базе.");
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var studentsOnCourse = new List<Student>();
            foreach (Group curGr in _grData)
            {
                if (curGr.Name().Course() == courseNumber.Get_num())
                {
                    foreach (Student curSt in curGr.Get_stData())
                    {
                        studentsOnCourse.Add(curSt);
                    }
                }
            }

            return studentsOnCourse;
        }

        public Group FindGroup(string groupName)
        {
            var buildingGroupName = new StringBuilder(groupName);
            foreach (Group curGr in _grData)
            {
                if (curGr.Name().GetName() == buildingGroupName) return curGr;
            }

            throw new IsuException($"Группы {groupName} нет в базе.");
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            var groupsOnCourse = new List<Group>();
            foreach (Group curGr in _grData)
            {
                if (curGr.Name().Course() == courseNumber.Get_num())
                {
                    groupsOnCourse.Add(curGr);
                }
            }

            return groupsOnCourse;
        }

        public void KickStudent(Student student)
        {
            List<Student> stData = null;
            GroupName groupName = null;
            foreach (Group curGr in _grData)
            {
                foreach (Student curSt in curGr.Get_stData())
                {
                    if (curSt == student)
                    {
                        stData = curGr.Get_stData();
                        groupName = curGr.Name();
                        stData.Remove(curSt);
                        _grData.Remove(curGr);
                        _grData.Add(new Group(groupName, stData, _maxSt));
                        return;
                    }
                }
            }

            throw new IsuException($"Этого студента не было в базе.");
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            int idSt = student.Get_id();
            KickStudent(student);
            newGroup.AddStudent(student.GetName(), idSt);
        }

        public void Change_maxSt(uint newMaxSt)
        {
            _maxSt = newMaxSt;
        }

        public void NewCourseLimit(int newMin, int newMax)
        {
            if (newMin < 1) throw new IsuException($"Наименьшее значение курса не может быть {newMin}");
            if (newMin > 9) throw new IsuException($"Наибольшее значение курса не может быть {newMax}");
            _maxNumCourse = newMax;
            _minNumCourse = newMin;
            foreach (var curGr in _grData)
            {
                var course = new CourseNumber(curGr.Name().Course(), newMin, newMax);
                curGr.ChangeCourseNumber(course);
            }
        }
    }
}