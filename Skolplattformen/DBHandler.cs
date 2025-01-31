using Microsoft.EntityFrameworkCore;
using Skolplattformen.Models;
namespace Skolplattformen
{
    public class DBHandler
    {

        // -------------------EF ----------------------
        // View all active courses

        // View all students (all info)

        // Get number of teachers per unit

        // ------------------ ADO.NET -----------------

        // View all staff (name, role, time employed, maybe subjects?

        // Save students

        // See what class

        // Add grade for student in a class (also which teacher did it + date)  ( TRANSACTIONS )

        // Average salary per unit

        // Total salary per unit 

        // STored procedure: Returns student information by ID 


        public List<Student> GetAllStudents()
        {


            using(var context = new Labb2Context())
            {
                var allStudents = context.Students
                     .Include(c => c.Class)
                     .ToList();

                return allStudents;
            }

        }
    }
}
