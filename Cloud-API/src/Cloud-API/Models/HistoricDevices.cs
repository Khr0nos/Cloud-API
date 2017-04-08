using System;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    /// <summary>
    /// Model class for HistoricDevices table entries
    /// </summary>
    public partial class HistoricDevices {
        /// <summary>
        /// historic of devices identifier
        /// </summary>
        [Required]
        public int IdhistoricDevices { get; set; }
        /// <summary>
        /// Device identifier
        /// </summary>
        public int Iddevice { get; set; }
        /// <summary>
        /// Date and time of historic entry creation
        /// </summary>
        public DateTime HistDeviceDate { get; set; }
        /// <summary>
        /// Device Action identifier
        /// </summary>
        public int IddeviceAction { get; set; }
        /// <summary>
        /// IP address from the device of this action. Optional
        /// </summary>
        public string HistDeviceIpaddress { get; set; }
        /// <summary>
        /// Auxiliar field. Optional
        /// </summary>
        public string HistDeviceAux { get; set; }

        internal virtual Devices IddeviceNavigation { get; set; }
        internal virtual AuxDeviceActions IddeviceActionNavigation { get; set; }
    }
}
