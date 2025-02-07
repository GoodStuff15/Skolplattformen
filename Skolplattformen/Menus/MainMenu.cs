
using Skolplattformen.Menus;

namespace Skolplattformen
{
    public class MainMenu : Menu
    {
        public MainMenu()
        {
            MenuOptions = new MenuOptionDelegates[]
            {
                new MenuOptionDelegates(EnterStaffMenu),
                new MenuOptionDelegates(EnterStudentMenu),
                new MenuOptionDelegates(EnterSubjectMenu),
                new MenuOptionDelegates(EnterBudgetMenu),
                new MenuOptionDelegates(ExitMenu)
            };
        }
        public override void ViewMenu()
        {
            Console.WriteLine("1: Enter Staff Administration Menu");
            Console.WriteLine("2: Enter Student Administration Menu");
            Console.WriteLine("3. Enter Subjects and Courses menu");
            Console.WriteLine("3: Enter Budget Administration Menu");
            Console.WriteLine("4: Log Out");
        }

        public void EnterStaffMenu()
        {
            _ = new InputControl(new StaffMenu(new Input()), new Input());
        }
        public void EnterStudentMenu()
        {
            _ = new InputControl(new StudentMenu(), new Input());
        }
        public void EnterSubjectMenu()
        {
            _ = new InputControl(new SubjectMenu(), new Input());
        }
        public void EnterBudgetMenu()
        {
            _ = new InputControl(new BudgetMenu(), new Input());
        }
    }

}
