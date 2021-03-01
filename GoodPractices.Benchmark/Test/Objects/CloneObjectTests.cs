using System.Globalization;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using GoodPractices.Benchmark.Test.Objects.Examples;
using GoodPractices.Benchmark.Test.Objects.Models;
using Microsoft.IO;
using Newtonsoft.Json;
using JsonSerializer = Newtonsoft.Json.JsonSerializer;
using JsonTextWriter = Anixe.IO.JsonTextWriter;
using StreamReader = Anixe.IO.StreamReader;
using StreamWriter = Anixe.IO.StreamWriter;

namespace GoodPractices.Benchmark.Test.Objects
{
    public class CloneObjectTests
    {
        private static readonly RecyclableMemoryStreamManager Manager = new();
        private static readonly JsonSerializer NewtonsoftSerializer = new();
        private static readonly CloneableObject CloneableObject = CloneableObjectExamples.Get();
        private static Encoding UTF8NoBOM => new UTF8Encoding(false);
        
        [Benchmark]
        public CloneableObject Clone_UsingNewtonsoftJsonSerialization()
        {
            var serialized = JsonConvert.SerializeObject(CloneableObject);

            return JsonConvert.DeserializeObject<CloneableObject>(serialized);
        }

        [Benchmark]
        public CloneableObject Clone_UsingNewtonsoftJsonSerialization_WithMemoryStream()
        {
            using var stream = Manager.GetStream();
            
            using (var textWriter = new StreamWriter(stream, UTF8NoBOM, 1024, true))
            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                NewtonsoftSerializer.Serialize(jsonWriter, CloneableObject);
            }

            stream.Seek(0, SeekOrigin.Begin);

            using (var textReader = new StreamReader(stream))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                return NewtonsoftSerializer.Deserialize<CloneableObject>(jsonReader);
            }
        }
        
        [Benchmark]
        public CloneableObject Clone_UsingNewtonsoftJsonSerialization_WithStringBuilder()
        {
            var stringBuilderPool = Consts.StringBuilderPool;
            var stringBuilder = stringBuilderPool.Get();

            using (var textWriter = new StringWriter(stringBuilder, CultureInfo.InvariantCulture))
            using (var jsonWriter = new JsonTextWriter(textWriter))
            {
                NewtonsoftSerializer.Serialize(jsonWriter, CloneableObject);
            }

            CloneableObject clonedObject;
            using (var textReader = new StringReader(stringBuilder.ToString()))
            using (var jsonReader = new JsonTextReader(textReader))
            {
                clonedObject = NewtonsoftSerializer.Deserialize<CloneableObject>(jsonReader);
            }
            
            stringBuilderPool.Return(stringBuilder);

            return clonedObject;
        }

        [Benchmark]
        public CloneableObject Clone_UsingSystemTextJsonSerialization()
        {
            var serialized = System.Text.Json.JsonSerializer.Serialize(CloneableObject);

            return System.Text.Json.JsonSerializer.Deserialize<CloneableObject>(serialized);
        }

        [Benchmark]
        public CloneableObject Clone_UsingSystemTextJsonSerialization_WithByteArray()
        {
            var serialized = System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(CloneableObject);

            return System.Text.Json.JsonSerializer.Deserialize<CloneableObject>(serialized);
        }

        [Benchmark]
        public CloneableObject Clone_UsingSystemTextJsonSerialization_WithMemoryStream()
        {
            using var stream = Manager.GetStream();
            
            using (var writer = new Utf8JsonWriter(stream))
            {
                System.Text.Json.JsonSerializer.Serialize(writer, CloneableObject);
            }
            
            stream.Seek(0, SeekOrigin.Begin);

            return System.Text.Json.JsonSerializer.Deserialize<CloneableObject>(stream.ToArray());
        }

        [Benchmark]
        public async ValueTask<CloneableObject> Clone_UsingSystemTextJsonSerialization_WithMemoryStream_Async()
        {
            await using var stream = Manager.GetStream();
            await System.Text.Json.JsonSerializer.SerializeAsync(stream, CloneableObject);
            stream.Seek(0, SeekOrigin.Begin);

            return await System.Text.Json.JsonSerializer.DeserializeAsync<CloneableObject>(stream);
        }
    }
}