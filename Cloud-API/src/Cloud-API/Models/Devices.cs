using System;
using System.Collections.Generic;

namespace Cloud_API.Models {
    public partial class Devices {
        public Devices() {
            HistoricData = new HashSet<HistoricData>();
            HistoricDevices = new HashSet<HistoricDevices>();
        }

        public int Iddevice { get; set; }
        public string DeviceName { get; set; }
        public int IdauxDeviceType { get; set; }
        public bool DeviceEnabled { get; set; }
        public bool DeviceConnected { get; set; }
        public bool DeviceNeedLogin { get; set; }
        public int DeviceInterval { get; set; }
        public DateTime DeviceCreationDate { get; set; }
        public string DeviceUsername { get; set; }
        public string DevicePassword { get; set; }
        public int IddeviceProtocol { get; set; }
        public string DeviceAux { get; set; }

        public virtual ICollection<HistoricData> HistoricData { get; set; }
        public virtual ICollection<HistoricDevices> HistoricDevices { get; set; }
        public virtual AuxDeviceType IdauxDeviceTypeNavigation { get; set; }
        public virtual AuxDeviceProtocols IddeviceProtocolNavigation { get; set; }
    }
}
