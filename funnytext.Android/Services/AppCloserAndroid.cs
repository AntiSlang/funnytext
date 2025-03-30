using System;
using Android.OS;
using Android.Util;

namespace funnytext.Android.Services;
public class AppCloserAndroid : IAppCloser
{
    public void Close()
    {
        Process.KillProcess(Process.MyPid());
        System.Environment.Exit(0);
    }
}