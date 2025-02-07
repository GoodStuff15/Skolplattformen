namespace Skolplattformen
{
    public static class Constants
    {
        public static readonly ConsoleKey[] validKeys =
                                     {ConsoleKey.A,
                                      ConsoleKey.B,
                                      ConsoleKey.C,
                                      ConsoleKey.D,
                                      ConsoleKey.E,
                                      ConsoleKey.F,
                                      ConsoleKey.G,
                                      ConsoleKey.H,
                                      ConsoleKey.I,
                                      ConsoleKey.J,
                                      ConsoleKey.K,
                                      ConsoleKey.L,
                                      ConsoleKey.M,
                                      ConsoleKey.N,
                                      ConsoleKey.O,
                                      ConsoleKey.P,
                                      ConsoleKey.Q,
                                      ConsoleKey.R,
                                      ConsoleKey.S,
                                      ConsoleKey.T,
                                      ConsoleKey.U,
                                      ConsoleKey.V,
                                      ConsoleKey.X,
                                      ConsoleKey.Y,
                                      ConsoleKey.Z};

        public static readonly int[] years =
                                    {2015,
                                     2016,
                                     2017,
                                     2018,
                                     2019,
                                     2020,
                                     2021,
                                     2022,
                                     2023,
                                     2024,
                                     2025
                                    };

        public static readonly int[] months =
        {
            1,2,3,4,5,6,7,8,9,10,11,12
        };

        public static readonly int[] days =
        {
            1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31
        };

        public static readonly int[,] daysInMonth =
        {
            {1, 31},
            {2, 28},
            {3, 31},
            {4, 30},
            {5, 31},
            {6, 30},
            {7, 31},
            {8, 31},
            {9, 30},
            {10, 31},
            {11, 30},
            {12, 31},
        };

        public const int columnSpacing = -15;
        public const int columnPadding = 4;
        
        public const int nameMax = 50;
        public const int nameMin = 2;

        public const string connectionString = @"Data Source=DESKTOP-IGVAOCU;Database=Labb2; Integrated Security=True;Trust Server Certificate=True;";
    }
}
