using System;
using System.IO;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleTests {
    [TestClass]
    public class UnitTest1 {
        [TestMethod]
        public void TestMethod1() {
            var req = WebRequest.Create("http://localhost:5009/api/items");
            req.Method = "GET";

            var res = req.GetResponse();
            Console.WriteLine(((HttpWebResponse)res).StatusCode);
            var reader = new StreamReader(res.GetResponseStream());
            var response = reader.ReadToEnd();
            Console.WriteLine(response);
            reader.Close();
            res.Close();
        }
    }
}
