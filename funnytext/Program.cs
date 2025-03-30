using Avalonia;
using System;
using System.Runtime.InteropServices;
using funnytext;

internal class Program
{
    [STAThread]
    public static void Main(string[] args) =>
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>()
            .With(new OSPlatform())
            .LogToTrace();
}