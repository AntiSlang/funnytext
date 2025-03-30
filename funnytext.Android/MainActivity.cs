using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Provider;
using Android.Util;
using Android.Views;
using Avalonia;
using Avalonia.Android;
using funnytext.Android.Services;
using Org.Apache.Http.Impl.IO;

namespace funnytext.Android;

[Activity(
    Label = "funnytext.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<App>
{

    protected override void OnCreate(Bundle savedInstanceState)
    {
        App.mactivity = this;
        /* if (Build.VERSION.SdkInt >= BuildVersionCodes.R)
        {
            if (!Environment.IsExternalStorageManager)
            {
                var intent = new Intent(Settings.ActionManageAllFilesAccessPermission);
                StartActivity(intent);
            }
        } */
        Window.SetDecorFitsSystemWindows(false);
        Window.SetFlags(WindowManagerFlags.LayoutNoLimits, WindowManagerFlags.LayoutNoLimits);
        base.OnCreate(savedInstanceState);
        
    }
    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        App.ShareService = new ShareServiceAndroid();
        App.AppCloser = new AppCloserAndroid();
        //App.AndActivity = new ActivityMediator();
        //App.AndActivity.Init(this);
        return base.CustomizeAppBuilder(builder).WithInterFont();
    }
}