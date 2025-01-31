using Skolplattformen.Models;

namespace Skolplattformen
{
    public class StaffMenu : Menu
    {
    

       

        public StaffMenu()
        {
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

        }

        public void AddStaffToDatabase()
        {

        }


    }
}
