using System;
using System.Linq;
using System.Threading;

namespace Cloud_Background {
    public class Program {
        private static DatabaseContext db;

        public static void Main(string[] args) {
            db = new DatabaseContext();
            //var query = from d in db.Devices select d.IDDevice;

            var data = db.HistoricData.OrderByDescending(d => d.IDHistoricData)
                .First(d => d.IDDevice == 2);

            Console.WriteLine(DateTime.Now);
            Console.WriteLine($"last with id: {data.IDDevice} {data.HistDataDate}");
            Console.WriteLine();
            foreach (var d in db.HistoricData) {
                Console.WriteLine($"id: {d.IDDevice} date: {d.HistDataDate}");
            }
        }
    }
}
