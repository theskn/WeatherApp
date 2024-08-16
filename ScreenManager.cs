namespace WeatherApp
{
    internal class ScreenManager
    {
        public static string DisplayScreen(string screenName)
        {
            Console.Clear();
            string path = screenName;
            string[] screen = File.ReadAllLines($@"C:\Users\simon\Desktop\C#\WeatherApp\Screens\{screenName}");
            foreach (string line in screen)
            {
                Console.WriteLine(line);
            }
            string userSelection = KeyboardNavigation(screenName, screen);
            return userSelection;
        }

        internal static string KeyboardNavigation(string screenName, string[] selection, int multiplier = 1)
        {

            //Count number of lines on screen
            int numberOfLines = File.ReadAllLines($@"C:\Users\simon\Desktop\C#\WeatherApp\Screens\{screenName}").Count();

            var Selection = false;
            string userSelection = "";
            int cursorPosition = 3;
            Console.SetCursorPosition(0, cursorPosition);
            while (!Selection)
            {
                var key = Console.ReadKey(true);
                switch (key.Key)
                {
                    //Move cursor up
                    case ConsoleKey.UpArrow:
                        if (cursorPosition > 3)
                        {
                            cursorPosition -= 1;
                            Console.SetCursorPosition(0, cursorPosition * multiplier);
                        }
                        break;
                    //Move cursor down
                    case ConsoleKey.DownArrow:
                        if (cursorPosition < numberOfLines - 1)
                        {
                            cursorPosition += 1;
                            Console.SetCursorPosition(0, cursorPosition * multiplier);
                        }
                        break;
                    //Make selection
                    case ConsoleKey.Enter:
                        Console.Clear();
                        userSelection = selection[cursorPosition].Split('.')[0];
                        Selection = true;
                        break;
                }
            }
            return userSelection;
        }
        internal static void GenerateHeader()
        {
            string[] header = File.ReadAllLines($@"C:\Users\simon\Desktop\C#\WeatherApp\Screens\Header.txt");
            foreach (string line in header)
            {
                Console.WriteLine(line);
            }
        }
        internal static void PressAnyKeyToContinue()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey(true);
        }
    }
}
