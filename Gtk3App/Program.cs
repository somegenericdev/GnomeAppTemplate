using System;
using Gdk;
using GLib;
using Gtk3App;
using Gtk;
using Application = Gtk.Application;
using DateTime = System.DateTime;
using Window = Gtk.Window;

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
button.Name = "my-button";
var counterLabel = new Label($"Counter: {counter}");

button.Clicked += (sender, e) =>
{
    counter = counter + 1;
    counterLabel.Text = $"Counter: {counter}";
};

counterBox.PackStart(counterLabel, false, false, 10);
counterBox.PackStart(button, false, false, 10);
notebook.AppendPage(counterBox, new Label("Counter"));


//tab 3
var weatherBox = new VBox();

var grid = BuildGrid(new List<WeatherForecastDto>()
    { new WeatherForecastDto(DateTime.Now, 30, "Hot"), new WeatherForecastDto(DateTime.Now.AddDays(-10), 10, "Cold") });
weatherBox.PackStart(grid, true, true, 0);
notebook.AppendPage(weatherBox, new Label("Weather"));


window.Add(notebook);


// Create CSS provider and load CSS
var cssProvider = new CssProvider();

string css = @"
            #my-button {
                color: blue;
                font-weight: bold;
                border-radius: 50px;
                padding: 10px;
            }

            #my-button:hover {
                background-color: #2980b9;
            }
        ";

cssProvider.LoadFromData(css);

// Add CSS provider to the default screen
StyleContext.AddProviderForScreen(
    Screen.Default,
    cssProvider,
    StyleProviderPriority.Application
);


foreach (var c in notebook.Children)
{
    notebook.ChildSetProperty(c,"tab-expand", new Value(true));
}

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
    
    foreach(var columnName in new[]{"Date", "Temperature", "Summary"})
    {
        var nameColumn = new TreeViewColumn { Title = columnName };
        var nameCell = new CellRendererText();
        nameColumn.Resizable = true;
        nameColumn.Sizing = TreeViewColumnSizing.Autosize;
        nameColumn.Reorderable = true;
        nameColumn.PackStart(nameCell, true);
        nameColumn.AddAttribute(nameCell, "text", 0);
        treeView.AppendColumn(nameColumn);

    }

    return treeView;
}