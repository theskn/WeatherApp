using System.Text;
using Newtonsoft.Json;

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
            string path = @"Cities.txt";
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
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"{city} added to Favorites.");
                Console.ResetColor();
            }
        }
        internal static void DisplayFavoriteCities()
        {
            string path = @"Cities.txt";
            string[] favoritesAsString = File.ReadAllLines(path);
            if (!File.Exists(path) || favoritesAsString.Length == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No cities saved in Favorites.");
                Console.ResetColor();
            }
            else
            {

                for (int i = 0; i < favoritesAsString.Length; i++)
                {
                    string[] strSplit = favoritesAsString[i].Split(";");
                    string city = strSplit[0];
                    string latitude = strSplit[1];
                    string longitude = strSplit[2];

                    Console.WriteLine("-----------");
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(city);
                    Console.WriteLine(latitude);
                    Console.WriteLine(longitude);
                    Console.ResetColor();
                    Console.WriteLine("-----------");
                }
            }
        }

        internal static void RemoveCityFromFavorites(string city)
        {
            string path = @"Cities.txt";
            List<string> favoritesAsList = new List<string>(File.ReadAllLines(path));

            favoritesAsList.RemoveAll(line => line.Contains(city));
            File.WriteAllLines(path, favoritesAsList);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"{city} has been removed from your Favorites.");
            Console.ResetColor();
        }

        internal static async Task SearchForCity(string name)
        {
            string apiURL = $"https://geocoding-api.open-meteo.com/v1/search?name={name}";
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiURL);
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        //Deserialize results into list
                        var allResults = JsonConvert.DeserializeObject<SearchRoot>(jsonResponse);
                        int counter = 1;

                        Console.Clear();
                        //Display the search results
                        foreach (SearchResult result in allResults.Results)
                        {
                            //Console.WriteLine("------------------------");
                            //Console.WriteLine("Possible Result:");
                            Console.WriteLine($"{counter}: {result.Name} in {result.Country}");
                            counter++;
                        }

                        //Wait for user's choice with index
                        /*Console.WriteLine("------------------------");
                        Console.WriteLine("Select one of the following options: ");
                        string userChoice = Console.ReadLine();
                        var userSelection = allResults.Results[int.Parse(userChoice)-1];
                        AddCityToFavorites(userSelection.Name, userSelection.Latitude.ToString(), userSelection.Longitude.ToString());
                        */

                        //User choice with the arrow keys
                        var userSelection = false;
                        int cursorPosition = 0;
                        Console.SetCursorPosition(0, cursorPosition);
                        while (!userSelection)
                        {
                            var key = Console.ReadKey(true);
                            switch (key.Key)
                            {
                                //Move cursor up
                                case ConsoleKey.UpArrow:
                                    if (cursorPosition != 0)
                                    {

                                        cursorPosition -= 1;
                                        Console.SetCursorPosition(0, cursorPosition);
                                    }
                                    break;
                                case ConsoleKey.DownArrow:
                                    if (cursorPosition < allResults.Results.Count - 1)
                                    {
                                        cursorPosition += 1;
                                        Console.SetCursorPosition(0, cursorPosition);
                                    }
                                    break;
                                case ConsoleKey.Enter:
                                    Console.Clear();
                                    AddCityToFavorites(allResults.Results[cursorPosition].Name, allResults.Results[cursorPosition].Latitude.ToString(), allResults.Results[cursorPosition].Longitude.ToString());
                                    userSelection = true;
                                    break;
                            }
                        }
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}

