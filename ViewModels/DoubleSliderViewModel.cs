using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using funnytext;
using funnytext.Services;
using ReactiveUI;

public class DoubleSliderViewModel : ReactiveObject
{
    private const int MinValue = -12; 
    private const int MaxValue = 12;
    private const int TotalSteps = MaxValue - MinValue + 1;
    
    private const double SliderWidth = 280; 
    private const double StepSize = SliderWidth / (TotalSteps - 1);
    public double GetStepSize() => StepSize;

    private int _leftValue = FileSettingsService.LoadInt("schedule_time_1", 7) - 12;
    public int LeftValue
    {
        get => _leftValue;
        set => this.RaiseAndSetIfChanged(ref _leftValue, value);
    }

    private int _rightValue = FileSettingsService.LoadInt("schedule_time_2", 21) - 12;
    public int RightValue
    {
        get => _rightValue;
        set => this.RaiseAndSetIfChanged(ref _rightValue, value);
    }

    public void MoveLeftThumb(Thumb thumb, Border activeRange, TranslateTransform leftTransform, TranslateTransform rightTransform, TextBlock textBlock)
    {
        thumb.DragDelta += (s, e) =>
        {
            double newX = Math.Clamp(leftTransform.X + e.Vector.X, 0, rightTransform.X);
            int newValue = MinValue + (int)Math.Round(newX / StepSize);

            if (newValue < RightValue)
            {
                LeftValue = newValue;
                leftTransform.X = (LeftValue - MinValue) * StepSize;
                UpdateActiveRange(activeRange, leftTransform.X, rightTransform.X);
            }
            UpdateData(textBlock);
        };
    }

    public void MoveRightThumb(Thumb thumb, Border activeRange, TranslateTransform rightTransform, TranslateTransform leftTransform, TextBlock textBlock)
    {
        thumb.DragDelta += (s, e) =>
        {
            double newX = Math.Clamp(rightTransform.X + e.Vector.X, leftTransform.X, SliderWidth);
            int newValue = MinValue + (int)Math.Round(newX / StepSize);

            if (newValue > LeftValue)
            {
                RightValue = newValue;
                rightTransform.X = (RightValue - MinValue) * StepSize;
                UpdateActiveRange(activeRange, leftTransform.X, rightTransform.X);
            }
            UpdateData(textBlock);
        };
    }

    public void UpdateData(TextBlock textBlock)
    {
        textBlock.Text = $"{LeftValue + 12}-{RightValue + 12}";
        FileSettingsService.Save("schedule_time_1", LeftValue + 12);
        FileSettingsService.Save("schedule_time_2", RightValue + 12);
        ((App)Application.Current).SetThemeShort();
    }

    public void UpdateActiveRange(Border activeRange, double leftX, double rightX)
    {
        activeRange.Margin = new Thickness(leftX, 0, SliderWidth - rightX, 0);
    }
}
