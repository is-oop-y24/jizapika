namespace IsuExtra.Tools.MegaFacultyDirectory
{
    public class MegaFaculty
    {
        public MegaFaculty(char megaFacultyType)
        {
            MegaFacultyLetter = megaFacultyType;
        }

        public char MegaFacultyLetter { get; }

        public CourseOGNP MakeOGNPCourse(uint flowSize)
        {
            
        }
    }
}