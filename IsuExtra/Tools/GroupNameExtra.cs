using Isu.Tools;
using IsuExtra.Exceptions;
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
        }

        public GroupNameExtra(string name, int minCourseNum = 1, int maxCourseNum = 9)
            : base(name, minCourseNum, maxCourseNum)
        {
            if (name[0] != MegaFacultyLetter) throw new IsuExtraException("Not correct groupName.");
        }

        public char MegaFacultyLetter { get; }
    }
}