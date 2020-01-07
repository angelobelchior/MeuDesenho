namespace MeuDesenho.Infra
{
    public class Parameters
    {
        public static string CustomVisionOnlineEndpoint = "";
        public static string CustomVisionOnlinePredictionKey = "";

        public static bool CanPredictOnLine = !string.IsNullOrEmpty(CustomVisionOnlineEndpoint) && !string.IsNullOrEmpty(CustomVisionOnlinePredictionKey);
        public static string OnnxFilePath = "ms-appx:///Assets/Models/MeuDesenho.onnx";
        public static int Threshold = 20;
    }
}
