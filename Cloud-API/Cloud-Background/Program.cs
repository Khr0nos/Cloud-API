using System;
using System.Linq;
using System.Threading;

namespace Cloud_Background {
    public class Program {
        private static DatabaseContext db;

        public static void Main(string[] args) {
            db = new DatabaseContext();
            ProcessDb();
        }

        private static void ProcessDb() {
            var devs = from d in db.Devices
                where d.DeviceEnabled && d.DeviceConnected
                select d.IDDevice;

            foreach (var id in devs) {
                var data = db.HistoricData.OrderByDescending(d => d.IDHistoricData)
                    .First(d => d.IDDevice == id);
                Console.WriteLine(DateTime.Now);
                Console.WriteLine($"last with id: {data.IDDevice} {data.HistDataDate}");
                Console.WriteLine();
            }
        }
    }
}
