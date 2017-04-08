using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    /// <summary>
    /// Model class for AuxDeviceActions table entries
    /// </summary>
    public partial class AuxDeviceActions {
        public AuxDeviceActions() {
            HistoricDevices = new HashSet<HistoricDevices>();
        }
        /// <summary>
        /// Device action identifier
        /// </summary>
        [Required]
        public int IdauxDeviceAction { get; set; }
        /// <summary>
        /// Device action description
        /// </summary>
        public string DeviceAction { get; set; }

        internal virtual ICollection<HistoricDevices> HistoricDevices { get; set; }
    }
}
