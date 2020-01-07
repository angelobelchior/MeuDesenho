using Windows.UI.Xaml.Controls;

namespace MeuDesenho.Views
{
    public sealed partial class MainPage : Page
    {
        private readonly ViewModels.MainViewModel _viewModel;

        public MainPage()
        {
            this.InitializeComponent();

            this.inkCanvas.InkPresenter.InputDeviceTypes = Windows.UI.Core.CoreInputDeviceTypes.Mouse |
                                                           Windows.UI.Core.CoreInputDeviceTypes.Pen |
                                                           Windows.UI.Core.CoreInputDeviceTypes.Touch;

            var abstractCanvas = new Services.AbstractCanvas(this.inkCanvas);
            var customVision = new Services.CustomVision();

            this._viewModel = new ViewModels.MainViewModel(abstractCanvas, customVision);
            this.DataContext = this._viewModel;
        }
    }
}
