using System.Collections.Generic;

namespace Cloud_API.Models {
    public partial class AuxDeviceProtocols {
        public AuxDeviceProtocols() {
            Devices = new HashSet<Devices>();
        }

        public int IddeviceProtocol { get; set; }
        public string DeviceProtocol { get; set; }

        internal virtual ICollection<Devices> Devices { get; set; }
    }
}
