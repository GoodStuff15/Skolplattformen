using Skolplattformen.Models;
namespace Skolplattformen
{
    public class StudentMenu : Menu
    {

        public StudentMenu()
        {
            MenuOptions = new MenuOptionDelegates[]
{
                new MenuOptionDelegates(ViewStudent),
                new MenuOptionDelegates(ViewAllStudents),
                new MenuOptionDelegates(RegisterGrade),
                new MenuOptionDelegates(ExitMenu)
};
        }
        public override void ViewMenu()
        {
            Console.WriteLine("1: View Student");
            Console.WriteLine("2: View all Students");
            Console.WriteLine("3: Register a grade for student");
            Console.WriteLine("4: Return to main menu");
        }

        public void ViewAllStudents()
        {
            var dbh = new DBHandler();

            var allStudents = dbh.GetAllStudents().OrderBy(s => s.Class.ClassName); // gets list as ienumerable, arranged by class
            Console.WriteLine($"{"First Name",Constants.columnSpacing}|{"Last Name",Constants.columnSpacing}|{"Person Number",Constants.columnSpacing}|{"Gender",Constants.columnSpacing / 2}|{"Class",Constants.columnSpacing}");
            foreach(Student s in allStudents)
            {
                Console.WriteLine($"{s.StudentName,Constants.columnSpacing} {s.StudentLname,Constants.columnSpacing} {s.PersonNumber,Constants.columnSpacing} {s.Gender,Constants.columnSpacing / 2} {s.Class.ClassName,Constants.columnSpacing}");
            }
            Console.WriteLine("Press any key to return to Student Menu");
            Console.ReadKey(true);
        }

        public void ViewStudent()
        {

        }

        public void RegisterGrade()
        {

        }
    }
}
