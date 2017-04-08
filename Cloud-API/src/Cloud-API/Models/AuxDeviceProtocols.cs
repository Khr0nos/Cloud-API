using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    /// <summary>
    /// Model class for AuxDeviceProtocols table entries
    /// </summary>
    public partial class AuxDeviceProtocols {
        public AuxDeviceProtocols() {
            Devices = new HashSet<Devices>();
        }
        /// <summary>
        /// Device protocol identifier
        /// </summary>
        [Required]
        public int IddeviceProtocol { get; set; }
        /// <summary>
        /// Device protocol description
        /// </summary>
        public string DeviceProtocol { get; set; }

        internal virtual ICollection<Devices> Devices { get; set; }
    }
}
