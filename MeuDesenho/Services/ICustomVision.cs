using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Storage.Streams;

namespace MeuDesenho.Services
{
    public interface ICustomVision
    {
        Task<IEnumerable<Models.Tag>> Predict(IRandomAccessStream randomAccessStream, bool useOnLinePredicition);
    }
}
