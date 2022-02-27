using System.Text.Json;

var handler = new HttpClientHandler();
handler.ServerCertificateCustomValidationCallback =
              (message, cert, chain, errors) => 
              { return true; };
using var client = new HttpClient(handler);
var json = await client.GetStringAsync("https://localhost:7219/weatherforecast");
var forecasts = JsonSerializer.Deserialize<List<WeatherForecast>>(json);
if (forecasts == null)
    return;
foreach (var forecast in forecasts)
    Console.WriteLine($"{forecast.date:dd/MM/yyyy}  {forecast.temperatureC}  {forecast.summary}");

public class WeatherForecast
{
    public DateTime date { get; set; }
    public int temperatureC { get; set; }
    public int temperatureF => 32 + (int)(temperatureC / 0.5556);
    public string? summary { get; set; }
} 