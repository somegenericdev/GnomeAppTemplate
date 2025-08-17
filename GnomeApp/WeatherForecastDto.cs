namespace GnomeApp;

public class WeatherForecastDto
{
    public DateTime Date { get; set; }
    public int Temp { get; set; }
    public string Summary { get; set; }

    public WeatherForecastDto(DateTime date, int temp, string summary)
    {
        Date = date;
        Temp = temp;
        Summary = summary;
    }
}