namespace Skolplattformen
{
   
    public class InputControl
    {
        private Menu _menu;
        
        public InputControl(Menu menu)
        {
            // Dependency inversion
            _menu = menu;
            MenuView();
            
        }

        public void MenuView()
        {

            while(_menu.Viewing)
            {
                Console.Clear();
                _menu.ViewMenu();

                // Uses choice method to define what option to run
                var run = _menu.MenuOptions[IntChoice(4)];
                Console.Clear();
                run();      // Runs the selected option

            }
           

        }


        public int IntChoice(int max)
        {
            bool isNumber;
            int num = 0;
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);

                // Checks if the choice is a number
                // If it is, converts keypress
                // into an int. 
                isNumber = Char.IsAsciiDigit(key.KeyChar);
                if (isNumber)
                {
                    num = Convert.ToInt32(key.KeyChar.ToString());
                }

            } while (!isNumber || num > max || num == 0); // exits while loop only if number is not bigger

            return num - 1;

        }
        public string StringInput(int minimumInput) // Method to only accept valid keypress
        {
            string toReturn = "";
            ConsoleKeyInfo cki;
            char character;

            do
            {   // Takes input 
                cki = Console.ReadKey(true);
                character = cki.KeyChar;

                bool validPress = Constants.validKeys.Contains(cki.Key);

                if (validPress && cki.Key != ConsoleKey.Backspace && cki.Key != ConsoleKey.Enter)
                {
                    // converts first character to upper case
                    if (toReturn.Length < 1)
                    {
                        character = Char.ToUpper(cki.KeyChar);
                    }
                    toReturn += character;
                    Console.Write(character);
                } // Backspace key delete characters from the string
                else if (cki.Key == ConsoleKey.Backspace && toReturn.Length > 0)
                {
                    toReturn = toReturn.Substring(0, (toReturn.Length - 1));
                    Console.Write("\b \b");
                }
                // Exits while loop when user presses enter after entering at least "minimumInput" characters
            } while (cki.Key != ConsoleKey.Enter || toReturn.Length < minimumInput);

            Console.WriteLine();

            return toReturn;
        }

        public bool BinaryChoice(ConsoleKey trueKey, ConsoleKey falseKey)
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            do
            {
                key = Console.ReadKey(true);


            } while (key.Key != trueKey && key.Key != falseKey);

            if (key.Key == trueKey)
            {
                return true;
            }
            else if (key.Key == falseKey)
            {
                return false;
            }
            else
            {
                throw new Exception("Error entering key");
            }
        }
    }
}
