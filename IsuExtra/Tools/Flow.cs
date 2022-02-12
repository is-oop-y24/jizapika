using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using IsuExtra.Exceptions;
using IsuExtra.Tools.Timetable;

namespace IsuExtra.Tools
{
    public class Flow
    {
        private List<Pair> _pairs;
        private List<StudentExtra> _students;
        public Flow(uint maxStudents)
        {
            _pairs = new List<Pair>();
            MaxStudents = maxStudents;
            _students = new List<StudentExtra>();
        }

        public ImmutableList<Pair> ImmutablePairs => _pairs.ToImmutableList();
        public ImmutableList<StudentExtra> ImmutableStudents => _students.ToImmutableList();
        public uint MaxStudents { get; }

        public void AddStudent(StudentExtra student)
        {
            if (!HasFreePlaces()) throw new IsuExtraException("The flow hasn't free places.");
            _students.Add(student);
        }

        public void AddPair(Pair pair)
        {
            if (_pairs.Any(currentPair => currentPair.IsCrossWithOtherPair(pair)))
                throw new IsuExtraException($"Not correct timetable.");

            _pairs.Add(pair);
            if (!IsPairNotCross())
            {
                _pairs.Remove(pair);
                throw new IsuExtraException("Pair can't add.");
            }
        }

        public bool HasFreePlaces()
            => MaxStudents != _students.Count;

        public bool HasStudent(StudentExtra student)
            => _students.Contains(student);

        public void RemoveStudent(StudentExtra student)
        {
            if (HasStudent(student))
                _students.Remove(student);
            else
                throw new IsuExtraException("The flow hasn't student.");
        }

        private bool IsPairNotCross()
            => _pairs.All(pair1 => !_pairs.Any(pair2 => !pair1.IsIdsTheSame(pair2) && pair1.IsCrossWithOtherPair(pair2)));
    }
}