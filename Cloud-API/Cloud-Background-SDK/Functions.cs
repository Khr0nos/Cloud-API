using System.IO;
using Microsoft.Azure.WebJobs;

namespace Cloud_Background {
    public class Functions {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("DeviceQueue")] string message, TextWriter log) {
            log.WriteLine(message);
        }
    }
}
