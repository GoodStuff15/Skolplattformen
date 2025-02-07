namespace Skolplattformen
{
    public class Input
    {
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

            } while (!isNumber || num > max || num == 0); // exits while loop only if number is not 0 or bigger than maximum value

            return num - 1;

        }
        public string StringInput(int minimumInput, int maximumInput) // Method to only accept valid keypress
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
            } while (cki.Key != ConsoleKey.Enter || toReturn.Length < minimumInput || toReturn.Length > maximumInput);

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

        public char LetterInput(List<ConsoleKey> validLetters)
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            do
            {
                key = Console.ReadKey(true);


            } while (!validLetters.Contains(key.Key));

            return key.KeyChar;
        }

        public decimal FinanceInput(int minLength, int maxLength)
        {
            bool isNumber;
            string num = "";
            ConsoleKeyInfo key = Console.ReadKey(true);
            do
            {
                // Checks if the choice is a number
                // If it is, converts keypress
                // into an int. 
                key = Console.ReadKey(true);
                isNumber = Char.IsAsciiDigit(key.KeyChar);
                if (isNumber && key.Key != ConsoleKey.Enter && key.Key != ConsoleKey.Backspace && num.Length < maxLength)
                {
                    num += key.KeyChar;
                    Console.Write(key.KeyChar);
                }
                else if(key.Key == ConsoleKey.Backspace && num.Length > 0)
                    {
                        num = num.Substring(0, (num.Length - 1));
                        Console.Write("\b \b");
                    }

            } while (key.Key != ConsoleKey.Enter || num.Length < maxLength || num.Length < minLength); // exits while loop only if number is not 0 or bigger than maximum value

             Decimal.TryParse(num, out decimal result);

            return result;
        }
        public string NumberInput(int minLength, int maxLength)
        {
            bool isNumber;
            string num = "";
            ConsoleKeyInfo key = Console.ReadKey(true);
            do
            {


                // Checks if the choice is a number
                // If it is, converts keypress
                // into an int. 
                isNumber = Char.IsAsciiDigit(key.KeyChar);
                if (isNumber)
                {
                    num = (string)num.Append(key.KeyChar);
                }

            } while (!isNumber || num.Length > maxLength || num.Length < minLength || key.Key == ConsoleKey.Enter); // exits while loop only if number is not 0 or bigger than maximum value

            return num;
        }

        public DateOnly DateInput()
        {
            ConsoleKeyInfo key = new ConsoleKeyInfo();
            int year = DateTime.Today.Year;
            int month = DateTime.Today.Month;
            int day = DateTime.Today.Month;
            int choice = 0;
            while(true)
            {
                Console.WriteLine($"Year: {Constants.years[choice]}");
                (int left, int top) = Console.GetCursorPosition();
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow && choice > 0)
                {
                    choice--;
                }
                else if (key.Key == ConsoleKey.DownArrow && choice < Constants.years.Length - 1)
                {
                    choice++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {

                }
                Console.SetCursorPosition(left, top - 1);
            }
            year = Constants.years[choice];
            choice = 0;
            // Choose month
            while (true)
            {
                Console.WriteLine($"Month: {Constants.months[choice]}");
                (int left, int top) = Console.GetCursorPosition();
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow && choice > 0)
                {
                    choice--;
                }
                else if (key.Key == ConsoleKey.DownArrow && choice < Constants.months.Length -1)
                {
                    choice++;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {

                }
                Console.SetCursorPosition(left, top - 1);
            }
            month = Constants.months[choice];
            choice = 0;
            while (true)
            {
                Console.WriteLine($"Day: {Constants.days[choice]}");
                (int left, int top) = Console.GetCursorPosition();
                key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.UpArrow && choice > 0)
                {
                    choice--;
                }
                else if (key.Key == ConsoleKey.DownArrow && choice < Constants.days.Length -1)
                {
                    if(choice == Constants.daysInMonth[month-1,1])
                    {

                    }
                    else
                    {
                        choice++;

                    }
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    break;
                }
                else
                {

                }
                Console.SetCursorPosition(left, top - 1);
            }
            day = Constants.days[choice];

            return new DateOnly(year, month, day);
        }
    }
}
