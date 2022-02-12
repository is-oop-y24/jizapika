using System.Collections.Generic;
using System.Linq;
using IsuExtra.Exceptions;

namespace IsuExtra.Tools
{
    public class CourseOGNP
    {
        public CourseOGNP(uint flowSize)
        {
            if (flowSize == 0) throw new IsuExtraException($"The flow can't be empty");
            Flows = new List<Flow>();
        }

        public uint FlowSize { get; set; }
        public List<Flow> Flows { get; }

        public void AddStudent(StudentExtra student)
        {
            foreach (Flow flow in Flows.Where(flow => flow.Students.Count != FlowSize))
            {
                flow.AddStudent(student);
                break;
            }
        }

        public Flow AddFlow()
        {
            
        }
    }
}