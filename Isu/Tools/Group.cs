using System.Collections.Generic;
using System.Diagnostics;
using Isu.Exceptions;

namespace Isu.Tools
{
    public class Group
    {
        private GroupName _groupname;
        private List<Student> _students;
        private uint _maxSt;
        public Group(GroupName groupname, List<Student> stData, uint maxSt = 30)
        {
            if (_students != null && _students.Count > maxSt) throw new IsuException($"Группу с таким количеством студентов создать невозможно!");
            _maxSt = maxSt;
            _groupname = groupname;
            _students = stData;
        }

        public void AddStudent(Student student)
        {
            _students.Add(student);
        }

        public bool CanAddThisStudent(string studentName)
        {
            foreach (Student curSt in _students)
            {
                if (curSt.GetName() == studentName)
                    return false;
            }

            return _students.Count != _maxSt;
        }

        public List<Student> Get_stData()
        {
            return _students;
        }

        public GroupName Name()
        {
            return _groupname;
        }

        public void ChangeCourseNumber(CourseNumber course)
        {
            _groupname.ChangeCourseNumber(course);
        }
    }
}