using System.Collections.Generic;
using Isu.Tools;

namespace IsuExtra.Tools
{
    public class GroupExtra : Group
    {
        public GroupExtra(GroupName groupName, List<Student> stData, uint maxSt = 30)
            : base(groupName, stData, maxSt)
        {
        }
    }
}