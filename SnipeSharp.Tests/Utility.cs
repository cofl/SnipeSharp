using System;
using System.Collections.Generic;
using System.Net;
using RestSharp;

namespace SnipeSharp.Tests
{
    internal static class Utility
    {
        internal const string TEST_URI_STRING = "http://test.example.net";
        internal const string TEST_TOKEN = "xxxxx";
        internal static readonly Uri TEST_URI = new Uri(TEST_URI_STRING);

        internal static SnipeItApi SingleUseApi()
        {
            return new SnipeItApi(new FakeRestClient()) { Token = TEST_TOKEN, Uri = TEST_URI };
        }

        internal static SnipeItApi SingleUseApi(string response, bool isSuccessful = true, HttpStatusCode? statusCode = null)
        {
            var client = new FakeRestClient();
            client.Responses.Enqueue(new FakeResponse(response, isSuccessful, statusCode));
            return new SnipeItApi(restClient: client) { Token = TEST_TOKEN, Uri = TEST_URI };
        }

        internal static SnipeItApi SingleUseApiFromFile(string filePath, bool isSuccessful = true, HttpStatusCode? statusCode = null)
        {
            var client = new FakeRestClient();
            client.Responses.Enqueue(FakeResponse.FromFile(filePath, isSuccessful, statusCode));
            return new SnipeItApi(restClient: client) { Token = TEST_TOKEN, Uri = TEST_URI };
        }

        internal static SnipeItApi MultipleUseApi(out Queue<RestResponse> responseQueue)
        {
            var client = new FakeRestClient();
            responseQueue = client.Responses;
            return new SnipeItApi(client)
            {
                Token = TEST_TOKEN,
                Uri = TEST_URI
            };
        }
    }
}
