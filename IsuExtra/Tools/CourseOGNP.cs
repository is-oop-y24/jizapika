using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using IsuExtra.Exceptions;

namespace IsuExtra.Tools
{
    public class CourseOGNP
    {
        private List<Flow> _flows;
        public CourseOGNP(uint flowSize)
        {
            if (flowSize == 0) throw new IsuExtraException($"The flow can't be empty");
            _flows = new List<Flow>();
        }

        public ImmutableList<Flow> Flows => _flows.ToImmutableList();
        public uint FlowSize { get; set; }

        public void AddStudent(StudentExtra student)
        {
            foreach (Flow flow in _flows.Where(flow => flow.Students.Count != FlowSize))
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