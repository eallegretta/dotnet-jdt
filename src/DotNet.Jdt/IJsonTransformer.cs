using System.IO;

namespace DotNet.Jdt
{
	public interface IJsonTransformer
	{
		Stream Transform(Stream input, Stream transform);
	}
}
