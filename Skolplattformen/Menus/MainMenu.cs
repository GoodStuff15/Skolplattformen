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
                new MenuOptionDelegates(EnterBudgetMenu),
                new MenuOptionDelegates(ExitMenu)
            };

        }

        public override void ViewMenu()
        {
            Console.WriteLine("1: Enter Staff Administration Menu");
            Console.WriteLine("2: Enter Student Administration Menu");
            Console.WriteLine("3: Enter Budget Administration Menu");
            Console.WriteLine("4: Log Out");
        }

        public void EnterStaffMenu()
        {
            var choice = new InputControl(new StaffMenu());
            
        }

        public void EnterStudentMenu()
        {
            var choice = new InputControl(new StudentMenu());
        }

        public void EnterBudgetMenu()
        {
            var choice = new InputControl(new BudgetMenu());
        }


    }

}
