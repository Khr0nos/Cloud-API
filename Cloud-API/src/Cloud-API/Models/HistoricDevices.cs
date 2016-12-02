using System;

namespace Cloud_API.Models {
    public partial class HistoricDevices {
        public int IdhistoricDevices { get; set; }
        public int Iddevice { get; set; }
        public DateTime HistDeviceDate { get; set; }
        public int IddeviceAction { get; set; }
        public string HistDeviceIpaddress { get; set; }
        public string HistDeviceAux { get; set; }

        public virtual Devices IddeviceNavigation { get; set; }
        public virtual AuxDeviceActions IddeviceActionNavigation { get; set; }
    }
}
