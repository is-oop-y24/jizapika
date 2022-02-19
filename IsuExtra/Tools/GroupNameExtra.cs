using System;
using System.Text;
using IsuExtra.Exceptions;
using IsuExtra.OldIsu.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;

namespace IsuExtra.Tools
{
    public class GroupNameExtra : GroupName
    {
        public GroupNameExtra(
            char megaFacultyLetter,
            int course,
            uint number,
            int minCourseNum = 1,
            int maxCourseNum = 9)
            : base(course, number, minCourseNum, maxCourseNum)
        {
            MegaFacultyLetter = megaFacultyLetter;
            Name = new StringBuilder(megaFacultyLetter + "3" + Convert.ToString((course * 100) + number));
        }

        public GroupNameExtra(string name, char megaFacultyLetter, int minCourseNum = 1, int maxCourseNum = 9)
            : base(name, minCourseNum, maxCourseNum)
        {
            MegaFacultyLetter = megaFacultyLetter;
            if (name[0] != MegaFacultyLetter) throw new IsuExtraException("Not correct groupName.");
        }

        public char MegaFacultyLetter { get; }
    }
}