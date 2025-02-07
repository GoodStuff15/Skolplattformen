using Skolplattformen.Models;
namespace Skolplattformen
{
    public class StudentFactory
    {
        public Student CreateStudent(string fname, string lname, string gender, DateOnly pnumber, int classid)
        {
            var student = new Student()
            {
                StudentName = fname,
                StudentLname = lname,
                Gender = gender,
                PersonNumber = pnumber,
                ClassId = classid
            };

            return student;
        }
    }
}
