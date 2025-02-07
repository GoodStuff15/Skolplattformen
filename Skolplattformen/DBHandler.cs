using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Skolplattformen.Models;
using static Azure.Core.HttpHeader;
using System.Collections.Generic;
using System.Reflection;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace Skolplattformen
{
    public class DBHandler
    {

        // ADO.NET Methods
        public decimal TotalSalaryOfUnit(int unitId)
        {
            string query = @"SELECT SUM(Staff.Salary) FROM Staff 
                            JOIN Unit ON Unit_Id = Unit.Id WHERE Unit_Id = {unitId}";

            decimal sum = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            sum = reader.GetDecimal(0);
                        }
                    }
                    conn.Close();
                    return sum;
                }
            }
            catch
            {
                throw new Exception("Error getting salary totals from database.");
            }
        }
        public decimal AverageSalaryOfUnit(int unitId)
        {
            string query = @"SELECT AVG(Staff.Salary) FROM Staff JOIN Unit ON Unit_Id = Unit.Id WHERE Unit_Id = {unitId}";
            decimal avg = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            avg = reader.GetDecimal(0);
                        }
                    }
                    conn.Close();
                    return avg;
                }
            }
            catch
            {
                throw new Exception("Error getting average salaries from database");
            }
        }
        public List<Staff> GetAllStaff()
        {

            string query = @"SELECT StaffName, IsMentor, DateOfHire, Salary, JobTitle.TitleName, Unit.UnitName 
                            FROM Staff 
                            JOIN JobTitle ON JobTitle_Id = JobTitle.Id 
                            JOIN Unit ON Unit_Id = Unit.Id";
            try
            {
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
            catch
            {
                throw new Exception("Error getting staff from database.");
            }
        }

        public List<StudentGrade> GetStudentGrades(int id)
        {
            string query = $@"SELECT SetDate, Student.StudentName, Student.StudentLName ,Course.CourseName, 
                            GradeScale.GradeName, Staff.StaffName, SchoolSubject.SubjectName, Class.ClassName 
                            FROM StudentGrade 
                            JOIN Course ON Course_Id = Course.Id 
                            JOIN Student ON Student_Id = Student.Id 
                            JOIN Staff ON Staff_Id = Staff.Id 
                            JOIN GradeScale ON GradeScale_Id = GradeScale.Id 
                            JOIN SchoolSubject ON Course.Subject_Id = SchoolSubject.Id 
                            JOIN Class ON Student.Class_Id = Class.Id WHERE Student_Id = {id} ";
            try
            {
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
                                Student = new Student() { StudentName = reader.GetString(1), StudentLname = reader.GetString(2), Class = new Class() { ClassName = reader.GetString(7) } },
                                Course = new Course() { CourseName = reader.GetString(3), Subject = new SchoolSubject() { SubjectName = reader.GetString(6) } },
                                GradeScale = new GradeScale() { GradeName = reader.GetString(4) },
                                Staff = new Staff() { StaffName = reader.GetString(5) }
                            };
                            allStudentGrades.Add(grade);
                        }
                        conn.Close();
                        return allStudentGrades;
                    }
                }
            }
            catch
            {
                throw new Exception("Error getting grades from database.");
            }
        }

        public void InsertStaff(Staff staff)
        {
            string query = @"INSERT INTO Staff(StaffName, JobTitle_Id, Salary, IsMentor, DateOfHire, Unit_Id) VALUES
                            (@StaffName, @JobTitleId, @Salary, @IsMentor, @DateOfHire, @UnitId)";
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@StaffName", $"{staff.StaffName}");
                        command.Parameters.AddWithValue("@JobTitleId", $"{staff.JobTitleId}");
                        command.Parameters.AddWithValue("@Salary", $"{staff.Salary}");
                        command.Parameters.AddWithValue("@IsMentor", $"{staff.IsMentor}");
                        command.Parameters.AddWithValue("@DateOfHire", $"{staff.DateOfHire.ToString()}");
                        command.Parameters.AddWithValue("@UnitId", $"{staff.UnitId}");

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                    Console.WriteLine($"Successfully entered new staff: {staff.StaffName}! Press any key to return to staff menu.");
                    Console.ReadKey();
                }
            }
            catch
            {
                throw new Exception("Error inserting staff into database.");
            }
        }
        public void InsertStudent(Student student)
        {
            string query2 = @"
                            BEGIN TRY
                                BEGIN TRANSACTION
                                INSERT INTO Student(StudentName, StudentLName, Gender, PersonNumber, Class_Id) VALUES
                                (@StudentName, @StudentLName, @Gender, @PersonNumber, @Class_Id)
                                COMMIT
                                END TRY
                            BEGIN CATCH
                                ROLLBACK
                            END CATCH";
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    using (SqlCommand command = new SqlCommand(query2, conn))
                    {
                        command.Parameters.AddWithValue("@StudentName", $"{student.StudentName}");
                        command.Parameters.AddWithValue("@StudentLName", $"{student.StudentLname}");
                        command.Parameters.AddWithValue("@Gender", $"{student.Gender}");
                        command.Parameters.AddWithValue("@PersonNumber", $"{student.PersonNumber.ToString()}");
                        command.Parameters.AddWithValue("@Class_Id", $"{student.ClassId}");

                        conn.Open();
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch
            {
                throw new Exception("Error inserting student into database");
            }
        }

        public void EditGrade(StudentGrade newgrade)
        {
            string query2 = @$"
                            BEGIN TRY
                                BEGIN TRANSACTION
                                UPDATE StudentGrade
                                SET SetDate = '{newgrade.SetDate.ToString()}',
                                GradeScale_Id = {newgrade.GradeScaleId}
                                WHERE StudentGrade.Id = {newgrade.Student.Id}
                                COMMIT
                                END TRY
                            BEGIN CATCH
                                ROLLBACK
                            END CATCH";
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    SqlCommand command = new SqlCommand(query2, conn);

                        conn.Open();
                        command.ExecuteNonQuery();
                }
                
            }
            catch
            {
                throw new Exception("Error inserting edited grade in database");
            }
        }
        public Dictionary<string, string> GetAverageGradeByUnit()
        {
            string query = @"SELECT UnitName, FORMAT(AVG(CAST(GradeScale.GradeValue AS decimal(10,2))), 'N2')
                            FROM Unit
                            JOIN Staff ON Staff.Unit_Id = Unit.Id
                            JOIN StudentGrade ON StudentGrade.Staff_Id = Staff.Id
                            JOIN GradeScale ON GradeScale.Id = StudentGrade.GradeScale_Id 
                            GROUP BY UnitName";

            var AvgGradeByUnit = new Dictionary<string, string>();

            try
            {

                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand(query, conn);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            for (int i = 0; i < reader.FieldCount; i = i + 2)
                            {
                                AvgGradeByUnit.Add(reader.GetString(i), reader.GetString(i + 1));
                            }
                        }
                    }
                    conn.Close();
                }
                return AvgGradeByUnit;
            }
            catch
            {
                throw new Exception("Error getting average grade by unit");
            }
        }

        public Student GetStudentInfo(int id)


        {
            var student = new Student();
            string procedureQuery = @"CREATE OR ALTER PROCEDURE GetStudentInfo
                                    @StudentId INT
                                    AS
                                    SELECT StudentName, StudentLName, Gender, PersonNumber, Class.ClassName
                                    FROM Student 
                                    JOIN Class ON Student.Class_Id = Class.Id
                                    WHERE Student.Id = @StudentId
                                    ";

            string getQuery = $"Exec GetStudentInfo @StudentId = '{id}'";
            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    SqlCommand command = new SqlCommand(procedureQuery, conn);

                    conn.Open();

                    command.ExecuteNonQuery();
                }
            }
            catch
            {
                throw new Exception("Error handling database procedure");
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(Constants.connectionString))
                {
                    SqlCommand command = new SqlCommand(getQuery, conn);

                    conn.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            student.StudentName = reader.GetString(0);
                            student.StudentLname = reader.GetString(1);
                            student.Gender = reader.GetString(2);
                            student.PersonNumber = DateOnly.FromDateTime(reader.GetDateTime(3));
                            student.Class = new Class() { ClassName = reader.GetString(4) };
                        }
                    }
                }
            }
            catch
            {
                throw new Exception("Error getting student from database");
            }

            return student;
        }

        // Entity Framework Methods

        public List<Student> GetAllStudents()
        {
            try
            {
                using (var context = new Labb2Context())
                {
                    var allStudents = context.Students
                         .Include(c => c.Class)
                         .ToList();

                    return allStudents;
                }
            }
            catch
            {
                throw new Exception("Error getting all students");
            }

        }

        public List<Staff> GetStaffByUnit(int id)
        {
            try
            {
                using (var context = new Labb2Context())
                {
                    var unitStaff = context.Staff
                                    .Where(s => s.UnitId == id)
                                    .Include(t => t.JobTitle)
                                    .ToList();

                    return unitStaff;
                }
            }
            catch
            {
                throw new Exception("Error getting Staff and Unit from database");
            }
        }

        public List<Unit> GetAllUnits()
        {
            try
            {
                using (var context = new Labb2Context())
                {
                    var allUnits = context.Units.ToList();

                    return allUnits;
                }
            }
            catch
            {
                throw new Exception("Error getting all Units from database");
            }
        }

        public List<JobTitle> GetAllTitles()
        {
            try
            {
                using (var context = new Labb2Context())
                {
                    var allTitles = context.JobTitles.ToList();

                    return allTitles;
                }
            }
            catch
            {
                throw new Exception("Error getting all Job titles from database");
            }
        }

        public List<Unit> GetUnitAndStaff()
        {
            using (var context = new Labb2Context())
            {
                var allUnits = context.Units
                    .Include(s => context.Staff)
                    .ToList();

                return allUnits;
            }
        }

        public Student GetStudentAndClass(int id)
        {
            try
            {
                using (var context = new Labb2Context())
                {
                    var student = context.Students.FirstOrDefault(s => s.Id == id);

                    student.Class = context.Classes.FirstOrDefault(c => c.Id == student.ClassId);

                    return student;
                }
            }
            catch
            {
                throw new Exception("Error getting Student and class fromn database");
            }
        }

        public List<Course> GetActiveCourses()
        {
            try
            {
                using (var context = new Labb2Context())
                {
                    var activeCourses = context.Courses
                        .Where(m => m.StartDate < DateOnly.FromDateTime(DateTime.Now) && m.EndDate > DateOnly.FromDateTime(DateTime.Now))
                        .Include(s => s.Subject)
                        .ToList();

                    return activeCourses;
                }
            }
            catch
            {
                throw new Exception("Error getting all active courses from database");
            }

        }

        // Gustavs method
        public void Grade()
        {

            var r = new Random();
            using (var context = new Labb2Context())
            {
                var coursesToGrade = context.Courses
                    .Include(cl => cl.CurrentClass)
                    .ThenInclude(st => st.Students)
                    .ToList();


                foreach (Course c in coursesToGrade)
                {
                    var grades = new List<StudentGrade>();
                    foreach (Student s in c.CurrentClass.Students)
                    {
                        var newGrade = new StudentGrade()
                        {
                            SetDate = DateOnly.FromDateTime(DateTime.Now),
                            StaffId = c.TeacherId,
                            StudentId = s.Id,
                            GradeScaleId = r.Next(1, 5),
                            CourseId = c.Id
                        };
                        grades.Add(newGrade);
                    }
                    context.AddRange(grades);
                }

                context.SaveChanges();

            }
        }
    }
}
