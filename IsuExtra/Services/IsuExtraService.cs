using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Isu.Services;
using IsuExtra.Exceptions;
using IsuExtra.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;

namespace IsuExtra.Services
{
    public class IsuExtraService : IsuService, IIsuExtraService
    {
        private List<MegaFaculty> _megaFaculties;

        public IsuExtraService()
        {
            _megaFaculties = new List<MegaFaculty>();
        }

        public ImmutableList<MegaFaculty> MegaFaculties => _megaFaculties.ToImmutableList();
        public MegaFaculty MakeMegaFaculty(char megaFacultyLetter)
        {
            if (_megaFaculties.Any(megaFaculty => megaFaculty.MegaFacultyLetter == megaFacultyLetter))
            {
                throw new IsuExtraException("The letter is already taken");
            }

            return new MegaFaculty(megaFacultyLetter);
        }

        public CourseOGNP AddNewOGNP(MegaFaculty megaFaculty, uint flowSize)
            => megaFaculty.MakeOGNPCourse(flowSize);

        public void EnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student)
        {
            if (student.CanEnrollToThisOGNP(courseOGNP))
                courseOGNP.AddStudent(student);
            else throw new IsuExtraException("Student can't enroll to this OGNP.");
        }

        public void CancelEnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student)
        {
            if (courseOGNP.HasStudent(student))
                courseOGNP.RemoveStudent(student);
            else throw new IsuExtraException("Student wasn't on this course.");
        }
    }
}