using System.Collections.Generic;
using IsuExtra.Exceptions;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class Flow
    {
        public Flow(uint maxStudents)
        {
            Pairs = new List<Pair>();
            MaxStudents = maxStudents;
            Students = new List<StudentExtra>();
        }

        public List<Pair> Pairs { get; }
        public List<StudentExtra> Students { get; }
        public uint MaxStudents { get; }

        public void AddStudent(StudentExtra student)
        {
            Students.Add(student);
        }

        public void AddPair(Pair pair)
        {
            foreach (Pair currentPair in Pairs)
            {
                if (currentPair.IsCrossWithOtherPair(pair)) throw new IsuExtraException($"Not correct timetable.");
            }

            Pairs.Add(pair);
        }

        private bool IsTimetableNotCross()
        {
            
        }
    }
}