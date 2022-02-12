using Isu.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;

namespace IsuExtra.Tools
{
    public class GroupNameExtra : GroupName
    {
        public GroupNameExtra(
            MegaFacultyType megaFacultyType,
            int course,
            uint number,
            int minCourseNum = 1,
            int maxCourseNum = 9)
            : base(course, number, minCourseNum, maxCourseNum)
        {
        }

        public GroupNameExtra(string name, int minCourseNum = 1, int maxCourseNum = 9)
            : base(name, minCourseNum, maxCourseNum)
        {
        }

        public MegaFacultyType MegaFacultyLetter { get; }
    }
}