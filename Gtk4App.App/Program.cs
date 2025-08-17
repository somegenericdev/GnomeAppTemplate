var application = Gtk.Application.New("org.kashif-code-samples.gtk4app.app", Gio.ApplicationFlags.FlagsNone);
application.OnActivate += (sender, args) =>
{
    var label = Gtk.Label.New("Hello World!");
    label.SetMarginTop(12);
    label.SetMarginBottom(12);
    label.SetMarginStart(12);
    label.SetMarginEnd(12);

    var window = Gtk.ApplicationWindow.New((Gtk.Application)sender);
    window.Title = "GTK Gtk4App App";
    window.SetDefaultSize(300, 300);
    window.Child = label;
    window.Show();
};
return application.RunWithSynchronizationContext(null);