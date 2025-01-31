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

        }

        public void ViewStudent()
        {

        }

        public void RegisterGrade()
        {

        }
    }
}
