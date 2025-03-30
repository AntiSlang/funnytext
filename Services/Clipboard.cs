using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input.Platform;
using Avalonia.VisualTree;

namespace funnytext.Services;
public class Clipboard {
    public static IClipboard Get() {
        if (Avalonia.Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime { MainWindow: { } window }) {
            return window.Clipboard!;
        }
        if (Avalonia.Application.Current?.ApplicationLifetime is ISingleViewApplicationLifetime { MainView: { } mainView }) {
            var visualRoot = mainView.GetVisualRoot();
            if (visualRoot is TopLevel topLevel) {
                return topLevel.Clipboard!;
            }
        }
        return null!;
    }
}