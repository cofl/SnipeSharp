using SnipeSharp.Serialization.Converters;
using Newtonsoft.Json;
using Xunit;
using System.IO;
using SnipeSharp.Models;
using SnipeSharp.Serialization;

namespace SnipeSharp.Tests
{
    public sealed class SerializeToIdArrayConverterTests
    {
        [Theory]
        [InlineData(new int[] { }, "[]")]
        [InlineData(new int[] { 0 }, "[{\"id\":0}]")]
        [InlineData(new int[] { 0, 1, 2, 3, 4 }, "[{\"id\":0},{\"id\":1},{\"id\":2},{\"id\":3},{\"id\":4}]")]
        public void ReadJson(int[] ids, string json)
        {
            var converter = SerializeToIdArrayConverter.Instance;
            using(var stringReader = new StringReader(json))
            using(var jsonReader = new JsonTextReader(stringReader))
            {
                var array = converter.ReadJson(jsonReader, null, null, false, NewtonsoftJsonSerializer.Deserializer);
                Assert.Equal(ids.Length, array.Length);
                for(var i = 0; i < ids.Length; i += 1)
                    Assert.Equal(ids[i], array[i].Id);
            }
        }

        [Theory]
        [InlineData(new int[] { })]
        [InlineData(new int[] { 0 })]
        [InlineData(new int[] { 0, 1, 2, 3, 4 })]
        public void WriteJson(int[] ids)
        {
            var modelArray = new AbstractBaseModel[ids.Length];
            for(int i = 0; i < ids.Length; i += 1)
                modelArray[i] = new GenericEndPointModel { Id = ids[i] };

            var converter = SerializeToIdArrayConverter.Instance;
            using(var stringWriter = new StringWriter())
            using(var jsonWriter = new JsonTextWriter(stringWriter))
            {
                converter.WriteJson(jsonWriter, modelArray, null);
                Assert.Equal(JsonConvert.SerializeObject(ids), stringWriter.ToString());
            }
        }
    }
}
