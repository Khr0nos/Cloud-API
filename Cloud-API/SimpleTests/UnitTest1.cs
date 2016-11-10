using System;
using System.IO;
using System.Net;
using Xunit;

namespace SimpleTests {
    public class UnitTest1 {
        [Theory]
        [InlineData(55, 56, 57)]
        public void GetItem(int id) {
            var req = WebRequest.Create($"http://localhost:5009/api/items/{id}");
            req.Method = "GET";

            var res = req.GetResponse();
            Console.WriteLine(((HttpWebResponse) res).StatusCode);
            Assert.Equal(HttpStatusCode.OK, ((HttpWebResponse) res).StatusCode);
            var reader = new StreamReader(res.GetResponseStream());
            var response = reader.ReadToEnd();
            Console.WriteLine(response);
            reader.Close();
            res.Close();
        }
    }
}
