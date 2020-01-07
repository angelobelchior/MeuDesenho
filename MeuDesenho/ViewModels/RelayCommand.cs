using System;
using System.Windows.Input;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

namespace MeuDesenho.ViewModels
{
    public class RelayCommand : ICommand
    {
        private CoreDispatcher _dispatcher = CoreApplication.MainView.Dispatcher;

        private readonly ViewModelBase _viewModelBase;
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public event EventHandler CanExecuteChanged;

        public RelayCommand(ViewModelBase viewModelBase, Action execute)
            : this(viewModelBase, execute, null)
        {
        }

        public RelayCommand(ViewModelBase viewModelBase, Action execute, Func<bool> canExecute)
        {
            _viewModelBase = viewModelBase ?? throw new ArgumentNullException("viewModelBase");
            _execute = execute ?? throw new ArgumentNullException("execute");
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
            => _canExecute == null ? true : _canExecute();

        public void Execute(object parameter)
            => this.Run(_execute);

        public void RaiseCanExecuteChanged()
            => CanExecuteChanged?.Invoke(this, EventArgs.Empty);

        private async void Run(Action action)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                action();
            });
        }
    }
}