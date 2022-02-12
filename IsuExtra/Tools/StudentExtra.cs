using Isu.Tools;

namespace IsuExtra.Tools
{
    public class StudentExtra : Student
    {
        private MegaFaculty _megaFaculty;

        public StudentExtra(string name, GroupName groupName, MegaFaculty megaFaculty)
            : base(name, groupName)
        {
        }
    }
}