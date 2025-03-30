using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace funnytext.Models;

using ReactiveUI;

public class EmptyItemModel
{
    public string Name { get; }
    public int Number { get; }

    public EmptyItemModel(string name, int number)
    {
        Name = name;
        Number = number;
    }
}