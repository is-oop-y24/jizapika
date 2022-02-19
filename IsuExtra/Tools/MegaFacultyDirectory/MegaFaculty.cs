using System.Collections.Generic;
using System.Collections.Immutable;

namespace IsuExtra.Tools.MegaFacultyDirectory
{
    public class MegaFaculty
    {
        private List<CourseOGNP> _coursesOGNP;
        public MegaFaculty(char megaFacultyType)
        {
            MegaFacultyLetter = megaFacultyType;
            _coursesOGNP = new List<CourseOGNP>();
        }

        public char MegaFacultyLetter { get; }
        public ImmutableList<CourseOGNP> ImmutableCoursesOGNP => _coursesOGNP.ToImmutableList();

        public CourseOGNP MakeOGNPCourse(uint flowSize)
        {
            var courseOGNP = new CourseOGNP(flowSize, MegaFacultyLetter);
            _coursesOGNP.Add(courseOGNP);
            return courseOGNP;
        }
    }
}