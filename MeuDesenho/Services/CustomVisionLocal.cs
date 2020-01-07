using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Windows.Storage;
using Windows.AI.MachineLearning;
using MeuDesenho.Models;
using Windows.Storage.Streams;
using MeuDesenho.Infra;

namespace MeuDesenho.Services
{
    internal sealed class Output
    {
        public TensorString classLabel;
        public IEnumerable<IReadOnlyDictionary<string, float>> loss;
    }

    internal sealed class CustomVisionLocal
    {
        private LearningModel _model;
        private LearningModelSession _session;
        private LearningModelBinding _binding;

        public static async Task<CustomVisionLocal> CreateModel()
        {
            var file = await StorageFile.GetFileFromApplicationUriAsync(new Uri(Parameters.OnnxFilePath));
            var learningModel = new CustomVisionLocal();
            learningModel._model = await LearningModel.LoadFromStorageFileAsync(file);
            learningModel._session = new LearningModelSession(learningModel._model);
            learningModel._binding = new LearningModelBinding(learningModel._session);

            if (learningModel._model == null) return null;
            return learningModel;
        }

        public async Task<IEnumerable<Tag>> Predict(IRandomAccessStream randomAccessStream)
        {
            var imageFeatureValue = await this.CovertRandomAccessStreamToImageFeatureValue(randomAccessStream).ConfigureAwait(false);
            var output = await this.Evaluate(imageFeatureValue).ConfigureAwait(false);
            var tags = this.ConvertToResult(output.loss);
            return tags;
        }

        private async Task<ImageFeatureValue> CovertRandomAccessStreamToImageFeatureValue(IRandomAccessStream randomAccessStream)
        {
            var videoFrame = await randomAccessStream.ToViewFrame().ConfigureAwait(false);
            var imageFeatureValue = ImageFeatureValue.CreateFromVideoFrame(videoFrame);
            return imageFeatureValue;
        }

        private async Task<Output> Evaluate(ImageFeatureValue data)
        {
            this._binding.Bind("data", data);
            var result = await this._session.EvaluateAsync(this._binding, "0");

            var outputs = result.Outputs.ToDictionary(k => k.Key, v => v.Value);
            var classLabel = outputs["classLabel"] as TensorString;
            var list = outputs["loss"] as IEnumerable<object>;

            var dictionary = new Dictionary<string, float>();
            foreach (var item in list)
                if (item is IReadOnlyDictionary<string, float> readOnlyDictionary)
                    dictionary = readOnlyDictionary.ToDictionary(k => k.Key, v => v.Value);

            var output = new Output
            {
                classLabel = classLabel,
                loss = new List<IReadOnlyDictionary<string, float>> { dictionary }
            };

            return output;
        }

        private IEnumerable<Tag> ConvertToResult(IEnumerable<IReadOnlyDictionary<string, float>> loss)
        {
            var list = new List<Tag>();
            foreach (var item in loss)
            {
                var tags = item.OrderByDescending(l => l.Value)
                               .Select(l => new Tag(l.Key, l.Value));
                list.AddRange(tags);
            }
            return list;
        }
    }
}