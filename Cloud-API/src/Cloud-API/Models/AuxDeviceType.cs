using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    /// <summary>
    /// Model class for AuxDeviceType table entries
    /// </summary>
    public partial class AuxDeviceType {
        public AuxDeviceType() {
            Devices = new HashSet<Devices>();
        }
        /// <summary>
        /// Device type identifier
        /// </summary>
        [Required]
        public int IdauxDeviceType { get; set; }
        /// <summary>
        /// Device type description
        /// </summary>
        public string DeviceType { get; set; }

        internal virtual ICollection<Devices> Devices { get; set; }
    }
}
