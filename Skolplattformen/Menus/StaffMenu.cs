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
            Console.WriteLine("TEST");
            Console.ReadLine();
        }

        public void ViewStaffByUnit()
        {

        }

        public void AddStaffToDatabase()
        {

        }


    }
}
