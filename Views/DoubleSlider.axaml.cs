using System;
using Avalonia.Controls;
using Avalonia.Media;
using Avalonia.Controls.Primitives;
using funnytext.Services;
using funnytext.ViewModels;

namespace funnytext.Views
{
    public partial class DoubleSlider : UserControl
    {
        public DoubleSlider()
        {
            InitializeComponent();
            var viewModel = new DoubleSliderViewModel();
            DataContext = viewModel;

            ((TranslateTransform)LeftThumb.RenderTransform).X = viewModel.GetStepSize() * FileSettingsService.LoadInt("schedule_time_1", 7);
            ((TranslateTransform)RightThumb.RenderTransform).X = viewModel.GetStepSize() * FileSettingsService.LoadInt("schedule_time_2", 21);
            viewModel.UpdateActiveRange(ActiveRange, ((TranslateTransform)LeftThumb.RenderTransform).X,
                ((TranslateTransform)RightThumb.RenderTransform).X);
            viewModel.MoveLeftThumb(LeftThumb, ActiveRange, 
                (TranslateTransform)LeftThumb.RenderTransform, 
                (TranslateTransform)RightThumb.RenderTransform, 
                LeftValueText);

            viewModel.MoveRightThumb(RightThumb, ActiveRange, 
                (TranslateTransform)RightThumb.RenderTransform, 
                (TranslateTransform)LeftThumb.RenderTransform, 
                LeftValueText);

            LeftThumb.PropertyChanged += (s, e) =>
            {
                double thumbX = ((TranslateTransform)LeftThumb.RenderTransform).X;
                Canvas.SetLeft(LeftValueText, thumbX - LeftValueText.Bounds.Width / 2);
                Canvas.SetTop(LeftValueText, -20);
                LeftValueText.Text = $"{viewModel.LeftValue + 12}-{viewModel.RightValue + 12}";
            };

            RightThumb.PropertyChanged += (s, e) =>
            {
                double thumbX = ((TranslateTransform)RightThumb.RenderTransform).X;
                LeftValueText.Text = $"{viewModel.LeftValue + 12}-{viewModel.RightValue + 12}";
            };
        }

    }
}