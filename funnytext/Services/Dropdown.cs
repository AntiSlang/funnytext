using System;
using System.Collections.ObjectModel;
using funnytext.Models;

namespace funnytext.Services;

public class Dropdown
{
    public ObservableCollection<EmptyItemModel> Values { get; }
    private EmptyItemModel _selected;
    private readonly Action<EmptyItemModel> _onSelectionChanged;

    public EmptyItemModel Selected
    {
        get => _selected;
        set
        {
            _selected = value;
            _onSelectionChanged?.Invoke(value);
        }
    }

    public Dropdown(ObservableCollection<EmptyItemModel> values, EmptyItemModel selected, Action<EmptyItemModel> onSelectionChanged)
    {
        Values = values;
        _selected = selected;
        _onSelectionChanged = onSelectionChanged;
        Set(0);
    }

    public void Set(int which)
    {
        if (which >= 0 && which < Values.Count)
        {
            Selected = Values[which];
        }
    }

    public int Get()
    {
        for (int i = 0; i < Values.Count; i++)
        {
            if (Selected.Number == Values[i].Number)
            {
                return i;
            }
        }
        return 0;
    }
}