using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class AuxDeviceActions {
        public AuxDeviceActions() {
            HistoricDevices = new HashSet<HistoricDevices>();
        }

        [Required]
        public int IdauxDeviceAction { get; set; }
        public string DeviceAction { get; set; }

        internal virtual ICollection<HistoricDevices> HistoricDevices { get; set; }
    }
}
