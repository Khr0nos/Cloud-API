using System;
using System.Threading;

namespace Cloud_Background {
    class Program {
        static void Main(string[] args) {
            while (true) {
                Console.WriteLine($"test data now: {DateTime.Now}");
                Thread.Sleep(2000);
            }
        }
    }
}
