using Newtonsoft.Json;
using WeatherApp;
using static WeatherApp.JsonConverter;

string userSelection;
string apiUrl;
do
{
    Console.WriteLine("--------------------");
    Console.WriteLine("Weather Forecast App");
    Console.WriteLine("--------------------");
    Console.WriteLine("1. See Favorite cities");
    Console.WriteLine("2. Add a city to Favorites");
    Console.WriteLine("3. Remove a city from Favorites");
    Console.WriteLine("9. Exit");

    Console.Write("Your selection: ");
    userSelection = Console.ReadLine();

    switch (userSelection)
    {
        case "1":
            Utilities.DisplayFavoriteCities();
            break;
        case "2":
            Console.Write("City name: ");
            string city = Console.ReadLine();
            Console.Write("City latitude: ");
            string latitude = Console.ReadLine();
            Console.Write("City longitude: ");
            string longitude = Console.ReadLine();

            Utilities.AddCityToFavorites(city, latitude, longitude);
            break;
        case "3":
            string input = Console.ReadLine();
            Utilities.RemoveCityFromFavorites(input);
            break;
        default:
            Console.WriteLine("Invalid selection. Please select one of the options above.");
            break;
    }
}
while (userSelection != "9");




async void RunApiCall()
{
    using (HttpClient client = new HttpClient())
    {
        try
        {
            HttpResponseMessage response = await client.GetAsync(apiUrl);

            if (response.IsSuccessStatusCode)
            {
                string jsonResponse = await response.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<WeatherData>(jsonResponse);

                Console.WriteLine(data.Hourly.Temperature2m[0]);
            }
            else
            {
                Console.WriteLine("Errpr");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}