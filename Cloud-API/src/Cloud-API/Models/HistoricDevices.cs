using System;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class HistoricDevices {
        [Required]
        public int IdhistoricDevices { get; set; }
        public int Iddevice { get; set; }
        public DateTime HistDeviceDate { get; set; }
        public int IddeviceAction { get; set; }
        public string HistDeviceIpaddress { get; set; }
        public string HistDeviceAux { get; set; }

        internal virtual Devices IddeviceNavigation { get; set; }
        internal virtual AuxDeviceActions IddeviceActionNavigation { get; set; }
    }
}
