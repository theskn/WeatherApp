using WeatherApp;

string userSelection;
do
{
    userSelection = ScreenManager.DisplayScreen("WelcomeScreen.txt");

    switch (userSelection)
    {
        case " 1":
            await Utilities.DisplayFavoriteCities();
            break;
        case " 2":
            {
                Console.Write("City name: ");
                string city = Console.ReadLine();
                Console.Write("City latitude: ");
                string latitude = Console.ReadLine();
                Console.Write("City longitude: ");
                string longitude = Console.ReadLine();

                Utilities.AddCityToFavorites(city, latitude, longitude);
            }
            break;
        case " 3":
            {
                Console.WriteLine("Search for a city:");
                string city = Console.ReadLine();
                await Utilities.SearchForCity(city);

            }
            break;
        case " 5":
            {
                string city = Console.ReadLine();
                Utilities.RemoveCityFromFavorites(city);
            }
            break;
        case " 0":
            break;
        default:
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Invalid selection. Please select one of the options above.");
            Console.ResetColor();
            break;
    }
}
while (userSelection != " 0");




// async void RunApiCall()
// {
//     using (HttpClient client = new HttpClient())
//     {
//         try
//         {
//             HttpResponseMessage response = await client.GetAsync(apiUrl);

//             if (response.IsSuccessStatusCode)
//             {
//                 string jsonResponse = await response.Content.ReadAsStringAsync();

//                 var data = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);

//                 Console.WriteLine(data.Hourly.Temperature2m[0]);
//             }
//         }
//         catch (Exception ex)
//         {
//             Console.WriteLine(ex.Message);
//         }
//     }

// }