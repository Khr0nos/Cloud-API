using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    public partial class Devices {
        public Devices() {
            HistoricData = new HashSet<HistoricData>();
            HistoricDevices = new HashSet<HistoricDevices>();
        }

        [Required]
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

        internal virtual ICollection<HistoricData> HistoricData { get; set; }
        internal virtual ICollection<HistoricDevices> HistoricDevices { get; set; }
        internal virtual AuxDeviceType IdauxDeviceTypeNavigation { get; set; }
        internal virtual AuxDeviceProtocols IddeviceProtocolNavigation { get; set; }
    }
}
