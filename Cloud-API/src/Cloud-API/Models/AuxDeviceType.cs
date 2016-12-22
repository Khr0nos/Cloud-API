using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class AuxDeviceType {
        public AuxDeviceType() {
            Devices = new HashSet<Devices>();
        }

        [Required]
        public int IdauxDeviceType { get; set; }
        public string DeviceType { get; set; }

        internal virtual ICollection<Devices> Devices { get; set; }
    }
}
