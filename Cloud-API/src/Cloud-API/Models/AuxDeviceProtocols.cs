using System.Collections.Generic;

namespace Cloud_API.Models {
    public partial class AuxDeviceProtocols {
        public AuxDeviceProtocols() {
            Devices = new HashSet<Devices>();
        }

        public int IddeviceProtocol { get; set; }
        public string DeviceProtocol { get; set; }

        public virtual ICollection<Devices> Devices { get; set; }
    }
}
