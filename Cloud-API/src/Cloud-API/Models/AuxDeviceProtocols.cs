using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class AuxDeviceProtocols {
        public AuxDeviceProtocols() {
            Devices = new HashSet<Devices>();
        }

        [Required]
        public int IddeviceProtocol { get; set; }
        public string DeviceProtocol { get; set; }

        internal virtual ICollection<Devices> Devices { get; set; }
    }
}
