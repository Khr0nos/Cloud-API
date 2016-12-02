using System.Collections.Generic;

namespace Cloud_API.Models {
    public partial class AuxDeviceType {
        public AuxDeviceType() {
            Devices = new HashSet<Devices>();
        }

        public int IdauxDeviceType { get; set; }
        public string DeviceType { get; set; }

        public virtual ICollection<Devices> Devices { get; set; }
    }
}
