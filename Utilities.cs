using System.Text;

namespace WeatherApp
{
    internal class Utilities
    {
        internal static string CityToString(string city, string latitude, string longitude)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"City:{city};");
            sb.Append($"Latitude:{latitude};");
            sb.AppendLine($"Longitude:{longitude}");

            return sb.ToString();
        }

        internal static void AddCityToFavorites(string city, string latitude, string longitude)
        {
            string path = @"C:\Users\simon\Desktop\C#\WeatherApp\Cities.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("No saved cities found. Creating new Favorites list.");
                using (StreamWriter sw = File.CreateText(path))
                {
                }
                File.AppendAllText(path, CityToString(city, latitude, longitude));
                Console.WriteLine($"{city} added to Favorites.");
            }
            else
            {
                File.AppendAllText(path, CityToString(city, latitude, longitude));
                Console.WriteLine($"{city} added to Favorites.");
            }
        }
        internal static void DisplayFavoriteCities()
        {
            string path = @"C:\Users\simon\Desktop\C#\WeatherApp\Cities.txt";
            if (!File.Exists(path))
            {
                Console.WriteLine("No cities saved in Favorites.");
            }
            else
            {
                string[] favoritesAsString = File.ReadAllLines(path);

                for (int i = 0; i < favoritesAsString.Length; i++)
                {
                    string[] strSplit = favoritesAsString[i].Split(";");
                    string city = strSplit[0];
                    string latitude = strSplit[1];
                    string longitude = strSplit[2];

                    Console.WriteLine("-----------");
                    Console.WriteLine(city);
                    Console.WriteLine(latitude);
                    Console.WriteLine(longitude);
                    Console.WriteLine("-----------");
                }
            }
        }

        internal static void RemoveCityFromFavorites(string city)
        {
            string path = @"C:\Users\simon\Desktop\C#\WeatherApp\Cities.txt";
            List<string> favoritesAsList = new List<string>(File.ReadAllLines(path));

            favoritesAsList.RemoveAll(line => line.Contains(city));
            File.WriteAllLines(path, favoritesAsList);
            Console.WriteLine($"{city} has been removed from your Favorites.");
        }
    }
}

