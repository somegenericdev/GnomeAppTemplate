using Gio;
using Gtk;
using Application = Gtk.Application;

Application app = Gtk.Application.New("org.kashif-code-samples.gtk4app.app", Gio.ApplicationFlags.FlagsNone);
app.OnActivate += OnActivate;

return app.RunWithSynchronizationContext(null);

void OnActivate(Gio.Application sender, EventArgs e)
{
    var window = Gtk.ApplicationWindow.New((Gtk.Application)sender);

    // Main vertical box layout
    var vbox = Box.New(Orientation.Vertical, 0);

    // StackSwitcher (tab bar)
    var stackSwitcher = new StackSwitcher();

    // Stack (content of each tab)
    var stack = new Stack
    {
        TransitionType = StackTransitionType.SlideLeftRight
    };

    // Link the switcher to the stack
    stackSwitcher.Stack = stack;

    // Add some pages (tabs)
    var page1 = Label.New("This is the content of Tab 1");
    stack.AddTitled(page1, "tab1", "Tab 1");

    var page2 = Label.New("This is the content of Tab 2");
    stack.AddTitled(page2, "tab2", "Tab 2");

    var page3 = Label.New("This is the content of Tab 3");
    stack.AddTitled(page3, "tab3", "Tab 3");

    // Pack widgets
    vbox.Append(stackSwitcher);
    vbox.Append(stack);

    window.SetChild(vbox);
    window.Show();
}