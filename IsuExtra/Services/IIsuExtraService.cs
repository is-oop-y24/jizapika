using Isu.Services;
using IsuExtra.Tools;
using IsuExtra.Tools.MegaFacultyDirectory;

namespace IsuExtra.Services
{
    public interface IIsuExtraService : IIsuService
    {
        MegaFaculty MakeMegaFaculty(char megaFacultyLetter);
        CourseOGNP AddNewOGNP(MegaFaculty megaFaculty, uint flowSize);
        void EnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student);
        void CancelEnrollStudentToOGNP(CourseOGNP courseOGNP, StudentExtra student);
    }
}