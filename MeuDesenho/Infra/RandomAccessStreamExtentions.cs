using System;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Media;
using Windows.Storage.Streams;

namespace MeuDesenho.Infra
{
    public static class RandomAccessStreamExtentions
    {
        public static async Task<VideoFrame> ToViewFrame(this IRandomAccessStream randomAccessStream)
        {
            using (randomAccessStream)
            {
                var decoder = await BitmapDecoder.CreateAsync(randomAccessStream);
                var softwareBitmap = await decoder.GetSoftwareBitmapAsync();

                byte[] imageBytes = new byte[4 * decoder.PixelWidth * decoder.PixelHeight];
                softwareBitmap.CopyToBuffer(imageBytes.AsBuffer());

                var videoFrame = VideoFrame.CreateWithSoftwareBitmap(softwareBitmap);
                return videoFrame;
            }
        }
    }
}
