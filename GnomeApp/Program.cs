using System;
using GnomeApp;
using Gtk;

Application.Init();

var window = new Window("GNOME App with Tab Bar");
window.SetDefaultSize(600, 400);
window.DeleteEvent += (o, e) => Application.Quit();

//tab bar
var notebook = new Notebook();

//tab 1
var tab1Label = new Label("Hello World. Welcome to your new app.");
notebook.AppendPage(tab1Label, new Label("Home"));

//tab 2
int counter = 0;
var counterBox = new VBox();
var button = new Button("Click Me");
var counterLabel = new Label($"Counter: {counter}");

button.Clicked += (sender, e) =>
{
    counter = counter + 1;
    counterLabel.Text = $"Counter: {counter}";
    Console.WriteLine("Button clicked in Tab 3!");
};

counterBox.PackStart(counterLabel, false, false, 10);
counterBox.PackStart(button, false, false, 10);
notebook.AppendPage(counterBox, new Label("Counter"));


//tab 3
var weatherBox = new VBox();

var grid = BuildGrid(new List<WeatherForecastDto>(){new WeatherForecastDto(DateTime.Now, 30, "Hot"), new WeatherForecastDto(DateTime.Now.AddDays(-10), 10, "Cold")});
weatherBox.PackStart(grid, true, true, 0);
notebook.AppendPage(weatherBox, new Label("Weather"));


window.Add(notebook);
window.ShowAll();
Application.Run();

TreeView BuildGrid(List<WeatherForecastDto> weatherForecasts)
{
    var treeView = new TreeView();
    var store = new ListStore(typeof(string), typeof(int), typeof(string));

    foreach (var forecast in weatherForecasts)
    {
        store.AppendValues(forecast.Date.ToString("dd/MM/yyyy"), forecast.Temp, forecast.Summary);
    }
    treeView.Model = store;

    var nameColumn = new TreeViewColumn { Title = "Date" };
    var nameCell = new CellRendererText();
    nameColumn.PackStart(nameCell, true);
    nameColumn.AddAttribute(nameCell, "text", 0);
    treeView.AppendColumn(nameColumn);

    var ageColumn = new TreeViewColumn { Title = "Temperature" };
    var ageCell = new CellRendererText();
    ageColumn.PackStart(ageCell, true);
    ageColumn.AddAttribute(ageCell, "text", 1);
    treeView.AppendColumn(ageColumn);
    
    
    var summaryColumn = new TreeViewColumn { Title = "Summary" };
    var summaryCell = new CellRendererText();
    summaryColumn.PackStart(summaryCell, true);
    summaryColumn.AddAttribute(summaryCell, "text", 1);
    treeView.AppendColumn(summaryColumn);

    return treeView;
}