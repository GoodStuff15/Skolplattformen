using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Skolplattformen.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
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



        // ADO.NET Methods

        public List<Staff> GetAllStaff()
        {

            string query = "SELECT StaffName, IsMentor, DateOfHire, Salary, JobTitle.TitleName, Unit.UnitName FROM Staff JOIN JobTitle ON JobTitle_Id = JobTitle.Id JOIN Unit ON Unit_Id = Unit.Id";

            using (SqlConnection conn = new SqlConnection(Constants.connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var allStaff = new List<Staff>();
                    while (reader.Read())
                    {
                            var staff = new Staff()
                            {
                                StaffName = reader.GetString(0),
                                IsMentor = reader.GetBoolean(1),
                                DateOfHire = DateOnly.FromDateTime(reader.GetDateTime(2)),
                                Salary = reader.GetDecimal(3),
                                JobTitle = new JobTitle() { TitleName = reader.GetString(4) },
                                Unit = new Unit() { UnitName = reader.GetString(5) }
                            };
                            allStaff.Add(staff);

                    }

                    conn.Close();
                    return allStaff;
                }
            }
        }

        public List<StudentGrade> GetStudentGrades(int id)
        {
            string query = $"SELECT SetDate,Student.StudentName,Course.CourseName, GradeScale.GradeName, Staff.StaffName FROM StudentGrade JOIN Course ON Course_Id = Course.Id JOIN Student ON Student_Id = Student.Id JOIN Staff ON Staff_Id = Staff.Id JOIN GradeScale ON GradeScale_Id = GradeScale.Id JOIN SchoolSubject ON Course.Subject_Id = SchoolSubject.Id WHERE Student_Id = {id} ";

            using (SqlConnection conn = new SqlConnection(Constants.connectionString))
            {
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    var allStudentGrades = new List<StudentGrade>();
                    while (reader.Read())
                    {
                        var grade = new StudentGrade()
                        {
                            SetDate = DateOnly.FromDateTime(reader.GetDateTime(0)),
                            Student = new Student() { StudentName = reader.GetString(1) },
                            Course = new Course() { CourseName = reader.GetString(2) },
                            GradeScale = new GradeScale() { GradeName = reader.GetString(3) },
                            Staff = new Staff() { StaffName = reader.GetString(4) }

                        };
                        allStudentGrades.Add(grade);

                    }

                    conn.Close();
                    return allStudentGrades;
                }
            }
        }

        public void ExecuteQuery(string query)
        {

            using (SqlConnection conn = new SqlConnection(Constants.connectionString))
            {
                
                conn.Open();

                SqlCommand command = new SqlCommand(query, conn);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                  
                    while(reader.Read())
                    {
                        
                        for(int i = 0; i < reader.FieldCount; i++)
                        {
                         
                        }
                    }
                
                }
            }
        }

        // Entity Framework Methods

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
