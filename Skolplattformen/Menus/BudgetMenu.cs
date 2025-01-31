namespace Skolplattformen
{
    public class BudgetMenu : Menu
    {

        public BudgetMenu()
        {
            MenuOptions = new MenuOptionDelegates[]
{
                new MenuOptionDelegates(ViewUnitSalaryStats),
                new MenuOptionDelegates(ViewUnitGradeStats),
                new MenuOptionDelegates(OtherStats),
                new MenuOptionDelegates(ExitMenu)
};
        }
        public override void ViewMenu()
        {
            Console.WriteLine("1: View Unit salary statistics");
            Console.WriteLine("2: View Unit grade statistics");
            Console.WriteLine("3: Other statistics");
            Console.WriteLine("4: Return to main menu");
        }

        public void ViewUnitSalaryStats()
        {

        }

        public void ViewUnitGradeStats()
        {

        }

        public void OtherStats()
        {

        }
    }
}
