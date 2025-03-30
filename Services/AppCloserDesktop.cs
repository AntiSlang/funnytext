using System;
using Avalonia.Controls.ApplicationLifetimes;

public class AppCloserDesktop : IAppCloser
{
    public void Close()
    {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.Shutdown();
        }
    }
}