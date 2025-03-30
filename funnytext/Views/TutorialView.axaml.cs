using Avalonia.Controls;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using funnytext.Services;

namespace funnytext.Views
{
    public partial class TutorialView : UserControl
    {
        public TutorialView()
        {
            InitializeComponent();
            DataContext = new TutorialViewModel();
        }
    }

    public class TutorialViewModel : ReactiveObject
    {
        private int _currentStepIndex = 0;
        private List<string> _steps = new();
        private bool _isVisible;

        public bool IsVisible
        {
            get => _isVisible;
            set => this.RaiseAndSetIfChanged(ref _isVisible, value);
        }

        public string CurrentStepText => _steps.Count > 0 ? _steps[_currentStepIndex] : string.Empty;
        public bool CanGoNext => _currentStepIndex < _steps.Count - 1;
        public bool CanComplete => _currentStepIndex == _steps.Count - 1;

        public ReactiveCommand<Unit, Unit> NextStepCommand { get; }
        public ReactiveCommand<Unit, Unit> CompleteTutorialCommand { get; }

        public TutorialViewModel()
        {
            NextStepCommand = ReactiveCommand.Create(NextStep);
            CompleteTutorialCommand = ReactiveCommand.Create(CompleteTutorial);
        }

        public void StartTutorial(List<string> steps)
        {
            _steps = steps;
            _currentStepIndex = 0;
            IsVisible = true;
            this.RaisePropertyChanged(nameof(CurrentStepText));
        }

        private void NextStep()
        {
            if (_currentStepIndex < _steps.Count - 1)
            {
                _currentStepIndex++;
                this.RaisePropertyChanged(nameof(CurrentStepText));
                this.RaisePropertyChanged(nameof(CanGoNext));
                this.RaisePropertyChanged(nameof(CanComplete));
            }
        }

        private void CompleteTutorial()
        {
            IsVisible = false;
            TutorialService.CompleteTutorial();
        }
    }
}