using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SnipeSharp.Serialization;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace SnipeSharp.Models
{
    /// <summary>
    /// A response from the API, which may indicate success or error, have messages, and possibly have a payload.
    /// </summary>
    /// <typeparam name="T">The payload type.</typeparam>
    public sealed class RequestResponse<T> : ApiObject where T : ApiObject
    {
        /// <value>Any messages returned by the API; the default key is "general".</value>
        //[DeserializeAs("messages", DeserializeAs.MessageDictionary)]
        public IReadOnlyDictionary<string, string> Messages { get; private set; }

        /// <value>Gets the payload value.</value>
        [DeserializeAs("payload")]
        public T Payload { get; private set; } = null;

        /// <value>The status of the request; usually "success" or "error".</value>
        [DeserializeAs("status")]
        public string Status { get; private set; } = string.Empty;

        [JsonExtensionData]
        private Dictionary<string, JToken> extensionData;

        [OnDeserialized]
        private void OnDeserialized(StreamingContext context)
        {
            if (null == extensionData)
                return;
            if (extensionData.TryGetValue("messages", out var token))
            {
                if (JTokenType.String == token.Type)
                    Messages = new Dictionary<string, string> { ["general"] = token.ToObject<string>() };
                else
                    Messages = token.ToObject<Dictionary<string, string>>();
            }
            else
            {
                Messages = new Dictionary<string, string>();
            }
            extensionData = null;
        }

        /// <inheritdoc />
        public override string ToString()
            => $"{Status}: {Messages.First().Value}";
    }
}
