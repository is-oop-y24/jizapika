using System;
using System.Text;
using Isu.Exceptions;

namespace Isu.Tools
{
    public class GroupName
    {
        private StringBuilder _name;
        private CourseNumber _course;
        private uint _number;
        private int _maxCourseNum;
        private int _minCourseNum;

        public GroupName(int course, uint number, int minCourseNum = 1, int maxCourseNum = 9)
        {
            var crs = new CourseNumber(course, minCourseNum, maxCourseNum);
            _minCourseNum = minCourseNum;
            _maxCourseNum = maxCourseNum;
            _name = new StringBuilder("M3" + Convert.ToString((course * 100) + number));
            _course = crs;
            _number = number;
        }

        public GroupName(string name, int minCourseNum = 1, int maxCourseNum = 9)
        {
            if (name.Length != 5 || name[0] != 'M' || name[1] != '3') throw new IsuException("Неправильный формат группы.");
            _name = new StringBuilder(name);
            try
            {
                var crs = new CourseNumber(int.Parse(name.Substring(2, 1)), minCourseNum, maxCourseNum);
                _course = crs;
                _number = uint.Parse(name.Substring(2, 3)) % 100;
            }
            catch (FormatException)
            {
                throw new IsuException("Неправильный формат группы.");
            }
        }

        public StringBuilder GetName()
        {
            return _name;
        }

        public int Course()
        {
            return _course.Get_num();
        }

        public void ChangeCourseNumber(CourseNumber course)
        {
            _course = course;
        }

        public CourseNumber GetCourseNumber()
        {
            return _course;
        }
    }
}