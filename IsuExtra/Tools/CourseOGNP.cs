using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using IsuExtra.Exceptions;
using IsuExtra.Tools.MegaFacultyDirectory;

namespace IsuExtra.Tools
{
    public class CourseOGNP
    {
        private List<Flow> _flows;
        public CourseOGNP(uint flowSize, char megaFacultyLetter)
        {
            if (flowSize == 0) throw new IsuExtraException($"The flow can't be empty");
            _flows = new List<Flow>();
            MegaFacultyLetter = megaFacultyLetter;
        }

        public ImmutableList<Flow> ImmutableFlows => _flows.ToImmutableList();
        public uint FlowSize { get; set; }
        public char MegaFacultyLetter { get; }

        public void AddStudent(StudentExtra student)
        {
            bool wasAdd = false;
            foreach (Flow flow in _flows.Where(flow => flow.ImmutableStudents.Count != FlowSize))
            {
                wasAdd = true;
                flow.AddStudent(student);
                break;
            }

            if (!wasAdd) throw new Exception("OGNP hasn't free places.");
        }

        public Flow AddFlow()
        {
            var flow = new Flow(FlowSize);
            _flows.Add(flow);
            return flow;
        }

        public bool HasStudent(StudentExtra student)
            => _flows.SelectMany(flow => flow.ImmutableStudents).Any(currentStudent => currentStudent.Id == student.Id);

        public void RemoveStudent(StudentExtra student)
        {
            foreach (Flow flow in _flows.Where(flow => flow.HasStudent(student)))
            {
                flow.RemoveStudent(student);
            }

            throw new IsuExtraException("OGNP course hasn't student.");
        }
    }
}