using Microsoft.VisualStudio.Jdt;
using System;
using System.IO;

namespace DotNet.Jdt
{
    internal class JsonTransformer : IJsonTransformer
    {
        private readonly IJsonTransformationLogger _logger;

        public JsonTransformer(IJsonTransformationLogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Stream Transform(Stream input, Stream transform)
        {
            var jdt = new JsonTransformation(transform, _logger);
            return jdt.Apply(input);
        }
    }
}
