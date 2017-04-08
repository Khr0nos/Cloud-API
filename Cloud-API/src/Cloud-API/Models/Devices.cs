using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    /// <summary>
    /// Model class for Devices table entries
    /// </summary>
    public partial class Devices {
        public Devices() {
            HistoricData = new HashSet<HistoricData>();
            HistoricDevices = new HashSet<HistoricDevices>();
        }
        /// <summary>
        /// Device identifier
        /// </summary>
        [Required]
        public int Iddevice { get; set; }
        /// <summary>
        /// Device Name
        /// </summary>
        public string DeviceName { get; set; }
        /// <summary>
        /// Device Type identifier
        /// </summary>
        public int IdauxDeviceType { get; set; }
        /// <summary>
        /// Boolean to indicate state of activation of device
        /// </summary>
        public bool DeviceEnabled { get; set; }
        /// <summary>
        /// Boolean to indicate state of connection of device
        /// </summary>
        public bool DeviceConnected { get; set; }
        /// <summary>
        /// Boolean to indicate use of login of device
        /// </summary>
        public bool DeviceNeedLogin { get; set; }
        /// <summary>
        /// Interval of data sending
        /// </summary>
        public int DeviceInterval { get; set; }
        /// <summary>
        /// Date and time of device creation
        /// </summary>
        public DateTime DeviceCreationDate { get; set; }
        /// <summary>
        /// Username for login
        /// </summary>
        public string DeviceUsername { get; set; }
        /// <summary>
        /// Password for login
        /// </summary>
        public string DevicePassword { get; set; }
        /// <summary>
        /// Device Protocol identifier
        /// </summary>
        public int IddeviceProtocol { get; set; }
        /// <summary>
        /// Auxiliar field. Optional
        /// </summary>
        public string DeviceAux { get; set; }

        internal virtual ICollection<HistoricData> HistoricData { get; set; }
        internal virtual ICollection<HistoricDevices> HistoricDevices { get; set; }
        internal virtual AuxDeviceType IdauxDeviceTypeNavigation { get; set; }
        internal virtual AuxDeviceProtocols IddeviceProtocolNavigation { get; set; }
    }
}
