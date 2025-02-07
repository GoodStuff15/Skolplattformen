namespace Skolplattformen
{
   
    public class InputControl
    {
        private Menu _menu;

        private Input _input;
        
        public InputControl(Menu menu, Input input)
        {
            // Dependency inversion
            _menu = menu;
            _input = input;
            MenuView();
           
        }

        public void MenuView()
        {

            while(_menu.Viewing)
            {
                Console.Clear();
                _menu.ViewMenu();

                // Uses choice method to define what option to run
                var run = _menu.MenuOptions[_input.IntChoice(_menu.MenuOptions.Length)];
                Console.Clear();
                run();      // Runs the selected option

            }
           

        }

    }
}
