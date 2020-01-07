using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Microsoft.Graphics.Canvas;
using System;
using Windows.UI;

namespace MeuDesenho.Services
{
    internal class AbstractCanvas : IAbstractCanvas
    {
        private readonly InkCanvas _inkCanvas;

        internal AbstractCanvas(InkCanvas inkCanvas)
            => this._inkCanvas = inkCanvas;

        public void Clear() 
            => this._inkCanvas.InkPresenter.StrokeContainer.Clear();

        public bool IsEmpty() 
            => this._inkCanvas.InkPresenter.StrokeContainer.GetStrokes().Count == 0;


        public async Task<IRandomAccessStream> GetRandomAccessStream()
        {
            var device = CanvasDevice.GetSharedDevice();
            var renderTarget = new CanvasRenderTarget(device, (int)this._inkCanvas.ActualWidth, (int)this._inkCanvas.ActualHeight, 96);
            using (var ds = renderTarget.CreateDrawingSession())
            {
                ds.Clear(Colors.White);
                ds.DrawInk(this._inkCanvas.InkPresenter.StrokeContainer.GetStrokes());
            }

            IRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            await renderTarget.SaveAsync(randomAccessStream, CanvasBitmapFileFormat.Jpeg, 1f);

            return randomAccessStream;
        }
    }
}
