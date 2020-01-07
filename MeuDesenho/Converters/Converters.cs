using System;
using Windows.UI.Xaml.Data;

namespace MeuDesenho.Converters
{
    public class ScoreToBarConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is float score)
                return score * 100;

            return 0;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => 0;
    }

    public class ScoreToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is float score)
                return $"{score * 100} %";

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => null;
    }

    public class ThresholdToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is double threshold)
                return $"Threshold de {threshold}%";

            return 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => 0;
    }

    public class PredictModeToLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool online)
                if (online)
                    return "Predição On-Line";

            return "Predição Off-Line";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
            => string.Empty;
    }
}
