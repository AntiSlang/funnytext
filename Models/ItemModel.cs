using System;
using Avalonia.Media.Imaging;
using Avalonia.Platform;

namespace funnytext.Models;

using ReactiveUI;

public class ItemModel
{
    public string Name { get; }
    public Bitmap Image { get; }
    public string Parameter { get; }

    public ItemModel(string name, string imagePath, string fontName)
    {
        Name = name;
        Image = new Bitmap(AssetLoader.Open(new Uri(imagePath)));
        Parameter = fontName;
    }
}