using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace SimpleTests {
    public class SimpleTests {
        [Theory]
        [InlineData(55, 56, 57)]
        public void Get(int id) {
            var client = new HttpClient {BaseAddress = new Uri("http://localhost:5009/")};
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var res = client.GetAsync($"api/items/{id}");

            Assert.Equal(HttpStatusCode.OK, res.Result.StatusCode);
        }
    }
}
