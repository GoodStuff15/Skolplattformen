
namespace Skolplattformen
{
    public abstract class Menu
    {
        public abstract void ViewMenu();

        public delegate void MenuOptionDelegates();

        public MenuOptionDelegates[]? MenuOptions { get; set; }
        public bool Viewing { get; set; } = true;
        
        public void ExitMenu()
        {
            Viewing = false;
        }

    }
}
