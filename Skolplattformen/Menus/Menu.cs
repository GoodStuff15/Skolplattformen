namespace Skolplattformen
{
    public abstract class Menu
    {
        public abstract void ViewMenu();

        public delegate void MenuOptionDelegates();

        public MenuOptionDelegates[]? MenuOptions;

        public bool Viewing { get; private set; } = true;

        public void ExitMenu()
        {
            Viewing = false;
        }

    }
}
