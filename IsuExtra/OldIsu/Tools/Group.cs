using System.Collections.Generic;
using System.Diagnostics;
using IsuExtra.OldIsu.Exceptions;

namespace IsuExtra.OldIsu.Tools
{
    public class Group
    {
        private GroupName _groupname;
        private List<Student> _students;

        public Group(GroupName groupname, List<Student> stData, uint maxSt = 30)
        {
            if (_students != null && _students.Count > maxSt)
                throw new IsuException($"Группу с таким количеством студентов создать невозможно!");
            MaxSt = maxSt;
            _groupname = groupname;
            _students = stData;
        }

        protected uint MaxSt { get; set; }

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

            return _students.Count != MaxSt;
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