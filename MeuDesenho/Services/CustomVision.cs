using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MeuDesenho.Services
{
    internal class CustomVision : ICustomVision
    {
        private CustomVisionLocal _customVisionOffLine;
        private CustomVisionOnLine _customVisionOnLine;

        public async Task<IEnumerable<Models.Tag>> Predict(IRandomAccessStream randomAccessStream, bool useOnLinePredicition)
        {
            if (useOnLinePredicition)
                return await this.OnLinePredict(randomAccessStream);
            else
                return await this.LocalPredict(randomAccessStream);
        }

        private async Task<IEnumerable<Models.Tag>> OnLinePredict(IRandomAccessStream randomAccessStream)
        {
            if (this._customVisionOnLine == null)
                this._customVisionOnLine = new CustomVisionOnLine();

            return await this._customVisionOnLine.Predict(randomAccessStream);
        }

        private async Task<IEnumerable<Models.Tag>> LocalPredict(IRandomAccessStream randomAccessStream)
        {
            if (this._customVisionOffLine == null)
                this._customVisionOffLine = await CustomVisionLocal.CreateModel();

            return await this._customVisionOffLine.Predict(randomAccessStream);
        }
    }
}
