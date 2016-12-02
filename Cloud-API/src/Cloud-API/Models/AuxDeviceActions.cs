using System.Collections.Generic;

namespace Cloud_API.Models {
    public partial class AuxDeviceActions {
        public AuxDeviceActions() {
            HistoricDevices = new HashSet<HistoricDevices>();
        }

        public int IdauxDeviceAction { get; set; }
        public string DeviceAction { get; set; }

        public virtual ICollection<HistoricDevices> HistoricDevices { get; set; }
    }
}
