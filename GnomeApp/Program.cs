using System;
using Gtk;


Application.Init();

var window = new Window("Hello GNOME with C#");
window.DeleteEvent += (o, e) => Application.Quit();
window.SetDefaultSize(400, 200);

var label = new Label("Welcome to GNOME app in C#!");
window.Add(label);

window.ShowAll();
Application.Run();