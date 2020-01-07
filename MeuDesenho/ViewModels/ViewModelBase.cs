using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MeuDesenho.ViewModels
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        private bool _isLoad = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isBusy;
        public bool IsBusy
        {
            get => this._isBusy;
            set => this.SetProperty(ref this._isBusy, value);
        }

        public void OnInitialize()
        {
            if (!_isLoad)
            {
                this.Initialize();
                this._isLoad = true;
            }
        }

        protected virtual void Initialize() { }

        protected void RaisePropertyChanged(string propertyName)
            => this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetProperty<T>(ref T backingField, T Value, [CallerMemberName] string propertyName = "")
        {
            var changed = !EqualityComparer<T>.Default.Equals(backingField, Value);
            if (changed)
            {
                backingField = Value;
                this.RaisePropertyChanged(propertyName);
            }
            return changed;
        }
    }
}
