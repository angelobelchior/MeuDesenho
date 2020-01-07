using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Linq;
using MeuDesenho.Services;
using MeuDesenho.Models;
using System;
using MeuDesenho.Infra;

namespace MeuDesenho.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private readonly IAbstractCanvas _abstractCanvas;
        public readonly ICustomVision _customVision;

        public ICommand PredictCommand { get; set; }
        public ICommand ClearCommand { get; set; }

        private bool _canPredictOnLine;
        public bool CanPredictOnLine
        {
            get => this._canPredictOnLine;
            set => this.SetProperty(ref this._canPredictOnLine, value);
        }

        private bool _onLinePrediction;
        public bool OnLinePredicition
        {
            get => this._onLinePrediction;
            set => this.SetProperty(ref this._onLinePrediction, value);
        }

        private float _threshold;
        public float Threshold
        {
            get => this._threshold;
            set
            {
                this.SetProperty(ref this._threshold, value);
                this.LoadTags();
            }
        }

        private IEnumerable<Tag> _tags;
        public ObservableCollection<Tag> Tags { get; set; }

        public MainViewModel(IAbstractCanvas abstractCanvas, ICustomVision customVision)
        {
            this._abstractCanvas = abstractCanvas;
            this._customVision = customVision;

            //Verificar acesso com a internet
            this.CanPredictOnLine = Parameters.CanPredictOnLine;

            this.PredictCommand = new RelayCommand(this, async () => await Predict());
            this.ClearCommand = new RelayCommand(this, this.Clear);

            this.Tags = new ObservableCollection<Tag>();
            this.Threshold = Parameters.Threshold;
        }

        private async Task Predict()
        {
            if (this._abstractCanvas.IsEmpty()) return;

            try
            {
                var randomAccessStream = await this._abstractCanvas.GetRandomAccessStream();

                this.Tags.Clear();
                this.IsBusy = true;
                this._tags = await this._customVision.Predict(randomAccessStream, this.OnLinePredicition);
                this.LoadTags();
            }
            catch { }
            finally
            {
                this.IsBusy = false;
            }
        }

        private void Clear()
        {
            if (this._abstractCanvas.IsEmpty()) return;
            this._abstractCanvas.Clear();
            this.Tags.Clear();
            this._tags = null;
        }

        private void LoadTags()
        {
            if (this._tags == null) return;

            this.Tags.Clear();
            var threshold = this.Threshold / 100;
            foreach (var tag in this._tags.Where(t => t.Score >= threshold))
                this.Tags.Add(tag);
        }
    }
}