using Skolplattformen.Models;

namespace Skolplattformen
{
    public class StaffMenu : Menu
    {
        private Input _input;
        public StaffMenu(Input input)
        {
            _input = input;
            MenuOptions = new MenuOptionDelegates[]
                {

                new MenuOptionDelegates(ViewAllStaff),
                new MenuOptionDelegates(ViewStaffByUnit),
                new MenuOptionDelegates(AddStaffToDatabase),
                new MenuOptionDelegates(ExitMenu)
                };
        }
        public override void ViewMenu()
        {
            Console.WriteLine("1: View all Staff");
            Console.WriteLine("2: View Staff by Unit");
            Console.WriteLine("3: Add new Staff to database");
            Console.WriteLine("4: Return to main menu");
        }
        public void ViewAllStaff()
        {
            var dbh = new DBHandler();

            var allStaff = dbh.GetAllStaff();

            Console.WriteLine($"{"Name",Constants.columnSpacing}|{"Job Title ",Constants.columnSpacing}|{"Unit",Constants.columnSpacing}|{"Mentor?",Constants.columnSpacing / 2}|{"Salary",Constants.columnSpacing}|{"Years employed",Constants.columnSpacing}");
            foreach (Staff s in allStaff)
            {
                // Calculating how many years Staff has been employed

                var start = (DateOnly)s.DateOfHire;
                int yearsEmployed = DateTime.Now.Year - start.Year;

                Console.WriteLine($"{s.StaffName,Constants.columnSpacing} {s.JobTitle.TitleName,Constants.columnSpacing} {s.Unit.UnitName,Constants.columnSpacing} {s.IsMentor,Constants.columnSpacing / 2} {s.Salary,Constants.columnSpacing} {yearsEmployed,Constants.columnSpacing}");
            }
            Console.WriteLine("Press any key to return to Staff Menu");
            Console.ReadKey(true);

        }
        public void ViewStaffByUnit()
        {
            var db = new DBHandler();

            var allunits = db.GetAllUnits();

            foreach (Unit u in allunits)
            {
                u.Staff = db.GetStaffByUnit(u.Id);
                Console.WriteLine($"{u.UnitName}.\nEmployee count: {u.Staff.Count()} ");

                foreach (Staff s in u.Staff)
                {
                    Console.WriteLine($"{s.StaffName,Constants.columnSpacing} {s.JobTitle.TitleName}");
                }
                Console.WriteLine();
            }
            Console.WriteLine("Press any key to return to Staff Menu");
            Console.ReadKey();
        }

        public void AddStaffToDatabase()
        {
            var db = new DBHandler();

            Console.WriteLine("Enter new employee");
            Console.WriteLine("Enter Name:");
            string name = _input.StringInput(Constants.nameMin, Constants.nameMax);
            Console.WriteLine("Type of employee (select by id):");

            foreach (JobTitle t in db.GetAllTitles())
            {
                Console.WriteLine($"{t.Id,Constants.columnSpacing / 4} {t.TitleName,Constants.columnSpacing}");
            }
            int jobtitle = _input.IntChoice(db.GetAllTitles().Count()) + 1;
            bool ismentor = false;
            if (jobtitle == 1)
            {
                Console.WriteLine("Is this teacher going to have a mentorship role? (Y/N)");
                ismentor = _input.BinaryChoice(ConsoleKey.Y, ConsoleKey.N);

            }

            Console.WriteLine("Enter Salary (5 figures):");
            decimal salary = _input.FinanceInput(4, 5);
            Console.WriteLine();
            Console.WriteLine("Enter Date of hire (use arrow up / arrow down keys:");
            DateOnly date = _input.DateInput();
            Console.WriteLine("Choose Unit for employee (select by id):");

            foreach (Unit u in db.GetAllUnits())
            {
                Console.WriteLine($"{u.Id,Constants.columnSpacing / 4} {u.UnitName,Constants.columnSpacing}");
            }
            int unit = _input.IntChoice(db.GetAllUnits().Count()) + 1;

            var creator = new StaffFactory();
            db.InsertStaff(creator.NewStaff(name, jobtitle, salary, ismentor, date, unit));
        }
    }
}
