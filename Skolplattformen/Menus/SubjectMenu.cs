using Skolplattformen.Models;
namespace Skolplattformen.Menus
{
    public class SubjectMenu : Menu
    {
        public SubjectMenu()
        {
            MenuOptions = new MenuOptionDelegates[]
            {
                new MenuOptionDelegates(ViewActiveCourses),
                new MenuOptionDelegates(ExitMenu)
            };
        }
        public override void ViewMenu()
        {
            Console.WriteLine("1: View Active Courses");
            Console.WriteLine("2: Exit Menu");
        }
        public void ViewActiveCourses()
        {
            var db = new DBHandler();

            var activeCourses = db.GetActiveCourses();
            Console.WriteLine($"{"Course",Constants.columnSpacing} {"Subject",Constants.columnSpacing - 5} {"Start date",Constants.columnSpacing} {"End date",Constants.columnSpacing}");

            foreach (Course c in activeCourses)
            {
                Console.WriteLine($"{c.CourseName,Constants.columnSpacing} {c.Subject.SubjectName,Constants.columnSpacing - 5} {c.StartDate.ToString(),Constants.columnSpacing} {c.EndDate.ToString(),Constants.columnSpacing}");
            }
            Console.WriteLine("\nPress any key to return to Subjects and Courses Menu");
            Console.ReadKey();
        }
    }
}
