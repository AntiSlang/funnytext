using Android.Content;
using Android.App;
using System;
using Android.Widget;
using funnytext.Services;

namespace funnytext.Android.Services;
public class ShareServiceAndroid : IShareService
{
    public void ShareText(string text)
    {
        var clipboard = Clipboard.Get();
        clipboard.SetTextAsync(text);
        try
        {
            var intent = new Intent(Intent.ActionSend);
            intent.SetType("text/plain");
            intent.PutExtra(Intent.ExtraText, text);
            intent.AddFlags(ActivityFlags.NewTask);
            var chooser = Intent.CreateChooser(intent, "Поделиться через...");
            chooser.AddFlags(ActivityFlags.NewTask);
            Application.Context.StartActivity(chooser);
        }
        catch (Exception ex)
        {
            Toast.MakeText(Application.Context, $"Текст скопирован, но не хватает разрешений", ToastLength.Long)?.Show();
        }
    }
}