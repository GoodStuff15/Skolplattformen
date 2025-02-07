using Skolplattformen.Models;

namespace Skolplattformen
{
    public class BudgetMenu : Menu
    {
        public List<Unit> allUnits;
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

        public void AllUnits()
        {


        }

        public void ViewUnitSalaryStats()
        {
            var db = new DBHandler();
            var units = db.GetAllUnits();

            foreach (Unit u in units)
            {
                Console.WriteLine($"{u.UnitName}\n" +
                                  $"Total Salaries: {db.TotalSalaryOfUnit(u.Id).ToString("C"), 2} \n" +
                                  $"Average Salary: {db.AverageSalaryOfUnit(u.Id).ToString("C2"), 2} \n");
            }
            Console.WriteLine("Press any key to return to Budget Menu");
            Console.ReadKey();
        }

        public void ViewUnitGradeStats()
        {
            var db = new DBHandler();
            foreach (var unitavg in db.GetAverageGradeByUnit())
            {
                Console.WriteLine($"{unitavg.Key,Constants.columnSpacing} {unitavg.Value,Constants.columnSpacing}");
            }
            Console.ReadKey();
        }

        public void OtherStats()
        {
            Console.Beep();
            Console.WriteLine("Please upgrade to Skolplattformen Pro to view other statistics");
            Console.WriteLine("Press any key to return to Budget Menu");
            Console.ReadKey();
        }
    }
}
