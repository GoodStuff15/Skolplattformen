using Skolplattformen.Models;
namespace Skolplattformen
{
    public class StudentMenu : Menu
    {
        public IOrderedEnumerable<Student>? allStudents;

        private Input _input;

        public StudentMenu()
        {
            _input = new Input();
            MenuOptions = new MenuOptionDelegates[]
            {
                new MenuOptionDelegates(ViewStudent),
                new MenuOptionDelegates(ViewAllStudents),
                new MenuOptionDelegates(RegisterGrade),
                new MenuOptionDelegates(ExitMenu)
            };
        }
        public override void ViewMenu()
        {
            Console.WriteLine("1: View Student");
            Console.WriteLine("2: View all Students");
            Console.WriteLine("3: Register a grade for student");
            Console.WriteLine("4: Return to main menu");
        }

        public void ViewAllStudents()
        {
            var dbh = new DBHandler();

            // var allStudents = dbh.GetAllStudents().OrderBy(s => s.Class.ClassName); // Original solution
            allStudents = dbh.GetAllStudents().OrderBy(s => s.Class.ClassName); // gets list as ienumerable, arranged by class
            Console.WriteLine($"{"First Name",Constants.columnSpacing}|{"Last Name",Constants.columnSpacing}|{"Person Number",Constants.columnSpacing}|{"Gender",Constants.columnSpacing / 2}|{"Class",Constants.columnSpacing}");
            foreach (Student s in allStudents)
            {
                Console.WriteLine($"{s.StudentName,Constants.columnSpacing} {s.StudentLname,Constants.columnSpacing} {s.PersonNumber,Constants.columnSpacing} {s.Gender,Constants.columnSpacing / 2} {s.Class.ClassName,Constants.columnSpacing}");
            }
            Console.WriteLine("Press any key to return to Student Menu");
            Console.ReadKey(true);
        }

        public int StudentChoice(DBHandler dbh)
        {
            int columns = 3;
            int chosenId = 0;
            bool selecting = true;
            string marker = "==>";
            string noMarker = "   ";
            allStudents = dbh.GetAllStudents().OrderBy(s => s.Class.ClassName);

            int rows = allStudents.Count() / columns;

            // Creating positioning for menu selection here
            // Adds students to jagged array, matching position of menu marker.
            string[,] pos = new string[rows, columns];

            Student[][] studentCols = new Student[rows][];

            var studentArray = allStudents.ToArray();
            int x = 0; // Student index

            for (int i = 0; i < studentArray.Count() / columns; i++)
            {
                studentCols[i] = new Student[0]; // new row in jagged array, must be initialized (empty)
                for (int j = 0; j < columns; j++)
                {
                    if (x <= studentArray.Count())
                    {
                        studentCols[i] = studentCols[i].Append(studentArray[x]).ToArray();
                        x++;

                    }
                    // adds empty markers
                    pos[i, j] = noMarker;
                }
            }

            int selX = 0;
            int selY = 0;

            pos[selX, selY] = marker;

            ConsoleHelper.SetCurrentFont("Consolas", 15);


            while (selecting)
            {
                Console.SetCursorPosition(0, 0);
                Console.WriteLine("Use arrow keys to navigate to a student and press Enter to select.");
                Console.WriteLine("Press ESC to go back");
                Console.Write($"{"",5} {"First Name",Constants.columnSpacing} {"Last Name",Constants.columnSpacing} {"Class",Constants.columnSpacing / 4}{"",Constants.columnPadding}");
                Console.Write($"{"",5} {"First Name",Constants.columnSpacing} {"Last Name",Constants.columnSpacing} {"Class",Constants.columnSpacing / 4}{"",Constants.columnPadding}");
                Console.WriteLine($"{"",5} {"First Name",Constants.columnSpacing} {"Last Name",Constants.columnSpacing} {"Class",Constants.columnSpacing / 4}{"",Constants.columnPadding}");

                for (int i = 0; i < studentArray.Count() / 3; i++)
                {

                    Console.Write($"{pos[i, 0],5} {studentCols[i][0].StudentName,Constants.columnSpacing} {studentCols[i][0].StudentLname,Constants.columnSpacing} {studentCols[i][0].Class.ClassName,Constants.columnSpacing / 4} {"|",Constants.columnPadding} ");


                    for (int j = 0; j < 2; j++)
                    {

                        Console.Write($"{pos[i, j + 1],5} {studentCols[i][j + 1].StudentName,Constants.columnSpacing} {studentCols[i][j + 1].StudentLname,Constants.columnSpacing} {studentCols[i][j + 1].Class.ClassName,Constants.columnSpacing / 4} {"|",Constants.columnPadding} ");

                    }

                    Console.WriteLine();
                }

                var cki = new ConsoleKeyInfo();
                cki = Console.ReadKey(true);

                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (selX > 0)
                        {
                            pos[selX, selY] = noMarker;
                            selX--;
                            pos[selX, selY] = marker;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (selX < (studentCols.Length - 1))
                        {
                            pos[selX, selY] = noMarker;
                            selX++;
                            pos[selX, selY] = marker;
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (selY < 2)
                        {
                            pos[selX, selY] = noMarker;
                            selY++;
                            pos[selX, selY] = marker;
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (selY > 0)
                        {
                            pos[selX, selY] = noMarker;
                            selY--;
                            pos[selX, selY] = marker;
                        }
                        break;
                    case ConsoleKey.Enter:

                        int choice = (selX + 1) * (selY + 1);
                        chosenId = studentCols[selX][selY].Id;
                        selecting = false;
                        break;
                    case ConsoleKey.Escape:
                        chosenId = 0;
                        selecting = false;
                        break;

                }

            }
            Console.Clear();
            return chosenId;

        }
        public void ViewStudent()
        {
            var dbh = new DBHandler();

            int chosenId = StudentChoice(dbh);

            if (chosenId != 0)
            {
                var student = dbh.GetStudentAndClass(chosenId);
                Console.WriteLine(student.StudentName);
                Console.WriteLine(student.StudentLname);
                Console.WriteLine(student.Class.ClassName);
                var studentGrades = dbh.GetStudentGrades(chosenId);
                Console.WriteLine($"{"Course",Constants.columnSpacing / 2} {"Subject",Constants.columnSpacing * 2} {"Grade",Constants.columnSpacing / 3} {"Grading teacher",Constants.columnSpacing} {"Date",Constants.columnSpacing}");
                foreach (StudentGrade g in studentGrades)
                {
                    Console.WriteLine($"{g.Course.CourseName,Constants.columnSpacing / 2} {g.Course.Subject.SubjectName,Constants.columnSpacing * 2} {g.GradeScale.GradeName,Constants.columnSpacing / 3} {g.Staff.StaffName,Constants.columnSpacing} {g.SetDate.ToString(),Constants.columnSpacing}");
                }
                Console.WriteLine("Press any key to return to Student menu");
                Console.ReadKey();

            }
        }

        public void RegisterGrade()
        {
            var db = new DBHandler();
            int choice = 0;
            string marker = "==>";
            string noMarker = "  ";
            bool choosing = true;

            int chosenId = StudentChoice(db);
            var student = db.GetStudentInfo(chosenId);
            var studentGrades = db.GetStudentGrades(chosenId);
            string[] pos = new string[studentGrades.Count()];

            for (int i = 0; i < studentGrades.Count(); i++)
            {
                pos[i] = noMarker;
            }
            pos[0] = marker;

            Console.WriteLine(student.StudentName);
            Console.WriteLine(student.StudentLname);
            Console.WriteLine(student.Class.ClassName);

            while (choosing)
            {

                Console.WriteLine($"{"",5}{"Course",Constants.columnSpacing / 2} {"Subject",Constants.columnSpacing * 2} {"Grade",Constants.columnSpacing / 3} {"Grading teacher",Constants.columnSpacing} {"Date",Constants.columnSpacing}");
                for (int i = 0; i < studentGrades.Count(); i++)
                {
                    Console.WriteLine($"{pos[i],5} {studentGrades[i].Course.CourseName,Constants.columnSpacing / 2} {studentGrades[i].Course.Subject.SubjectName,Constants.columnSpacing * 2} {studentGrades[i].GradeScale.GradeName,Constants.columnSpacing / 3} {studentGrades[i].Staff.StaffName,Constants.columnSpacing} {studentGrades[i].SetDate.ToString(),Constants.columnSpacing}");
                }

                Console.WriteLine("Select a grade to edit (Use up and down arrows, Enter to select)");

                var cki = new ConsoleKeyInfo();
                cki = Console.ReadKey(true);
                int chosenGradeId = 0;
                switch (cki.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (choice > 0)
                        {
                            pos[choice] = noMarker;
                            choice--;
                            pos[choice] = marker;
                        }
                        break;
                    case ConsoleKey.DownArrow:
                        if (choice < (studentGrades.Count() - 1))
                        {
                            pos[choice] = noMarker;
                            choice++;
                            pos[choice] = marker;
                        }
                        break;
                    case ConsoleKey.Enter:
                        chosenGradeId = studentGrades[choice].Id;
                        choosing = false;
                        Console.Clear();

                        break;
                    default:
                        break;
                }
                Console.SetCursorPosition(0, 3);
            }
            Console.WriteLine(student.StudentName);
            Console.WriteLine(student.StudentLname);
            Console.WriteLine(student.Class.ClassName);
            Console.WriteLine($"Course = {studentGrades[choice].Course.CourseName}. Current grade: {studentGrades[choice].GradeScale.GradeName} ");
            Console.WriteLine("Enter a new grade for this student and course: ");
            Console.WriteLine("1. IG");
            Console.WriteLine("2. G");
            Console.WriteLine("3. VG");
            Console.WriteLine("4. MVG");
            Console.WriteLine("5. -");
            int newgrade = _input.IntChoice(5) + 1;

            studentGrades[choice].GradeScaleId = newgrade;
            studentGrades[choice].SetDate = DateOnly.FromDateTime(DateTime.Now);

            db.EditGrade(studentGrades[choice]);
            Console.WriteLine("Grade was edited! Press any key to return to previous menu.");
            Console.ReadKey();
        }

        public void InsertStudent()
        {
            //Console.WriteLine("First name:");
            //string fname = _input.StringInput(Constants.nameMin, Constants.nameMax);
            //Console.WriteLine("Last name:");
            //string lname = _input.StringInput(Constants.nameMin, Constants.nameMax);
            //Console.WriteLine("Gender (M/F):");
            //string gender = _input.LetterInput(new List<ConsoleKey> { ConsoleKey.M, ConsoleKey.F }).ToString();
            //Console.WriteLine("First name:");
            //string pnumber = _input.NumberInput(6, 6);
        }
    }
}
