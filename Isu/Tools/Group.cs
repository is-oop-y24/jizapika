using System.Collections.Generic;
using System.Diagnostics;
using Isu.Exceptions;

namespace Isu.Tools
{
    public class Group
    {
        private GroupName _groupname;
        private List<Student> _stData;
        private uint _maxSt;
        public Group(GroupName groupname, List<Student> stData, uint maxSt = 30)
        {
            if (_stData != null && _stData.Count > maxSt) throw new IsuException($"Группу с таким количеством студентов создать невозможно!");
            _maxSt = maxSt;
            _groupname = groupname;
            _stData = stData;
        }

        public Student AddStudent(string nameNewSt, int idSt)
        {
            var newSt = new Student(nameNewSt, _groupname);
            foreach (Student curSt in _stData)
            {
                if (curSt.GetName() == nameNewSt)
                {
                    throw new IsuException($"Студент {newSt.GetName()} уже значится в группе {_groupname.GetName()}.");
                }
            }

            if (_stData.Count == _maxSt)
            {
                throw new IsuException($"В группе {_groupname.GetName()} уже слишком много студентов. Студент {newSt.GetName()} не был добавлен.");
            }

            newSt.Assign_id(idSt);
            _stData.Add(newSt);
            return newSt;
        }

        public void KickStudent(Student newSt)
        {
            foreach (Student curSt in _stData)
            {
                if (curSt.GetName() == newSt.GetName())
                {
                    _stData.Remove(newSt);
                    return;
                }
            }

            throw new IsuException($"Студент {newSt.GetName()} не приписан к группе {_groupname.GetName()}.");
        }

        public List<Student> Get_stData()
        {
            return _stData;
        }

        public void Set_stData(List<Student> newStData)
        {
            _stData = newStData;
        }

        public GroupName Name()
        {
            return _groupname;
        }

        public void ChangeCourseNumber(CourseNumber course)
        {
            _groupname.ChangeCourseNumber(course);
        }

        public void CourseUp()
        {
            CourseNumber course = _groupname.GetCourseNumber();
            course++;
            _groupname.ChangeCourseNumber(course);
        }
    }
}