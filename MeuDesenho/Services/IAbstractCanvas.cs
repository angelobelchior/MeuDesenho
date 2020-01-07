using System.Threading.Tasks;
using Windows.Media;
using Windows.Storage.Streams;

namespace MeuDesenho.Services
{
    public interface IAbstractCanvas
    {
        bool IsEmpty();
        void Clear();
        Task<IRandomAccessStream> GetRandomAccessStream();
    }
}
