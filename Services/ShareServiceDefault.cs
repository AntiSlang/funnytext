using System;
using funnytext.Services;

public class ShareServiceDefault : IShareService
{
    public void ShareText(string text)
    {
        var clipboard = Clipboard.Get();
        clipboard.SetTextAsync(text);
    }
}