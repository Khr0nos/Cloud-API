using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    /// <summary>
    /// Model class for HistoricData table entries
    /// </summary>
    public partial class HistoricData {
        /// <summary>
        /// Data identifier
        /// </summary>
        public int IdhistoricData { get; set; }

        /// <summary>
        /// Device identifier
        /// <remarks>This identifier marks which device sent this data</remarks>
        /// </summary>
        [Required(ErrorMessage = "Device id missing")]
        public int Iddevice { get; set; }

        /// <summary>
        /// Date and time of data creation
        /// </summary>
        public DateTime? HistDataDate { get; set; }

        /// <summary>
        /// Data value
        /// </summary>
        [Required(ErrorMessage = "Data Value missing")]
        public string HistDataValue { get; set; }

        /// <summary>
        /// Data type identifier
        /// </summary>
        [Required(ErrorMessage = "Data type id missing")]
        public int IddataType { get; set; }

        /// <summary>
        /// Boolean to indicate if it's some data to be sent to device
        /// </summary>
        [DefaultValue(false)]
        public bool HistDataToDevice { get; set; } = false;

        /// <summary>
        /// Boolean to indicate confirmation from the device
        /// <remarks>This field marks if some data sent to the device has been confirmed</remarks>
        /// </summary>
        [DefaultValue(false)]
        public bool HistDataAck { get; set; } = false;

        /// <summary>
        /// Auxiliar field
        /// </summary>
        [DefaultValue(null)]
        public string HistDataAux { get; set; } = null;

        internal virtual AuxDataType IddataTypeNavigation { get; set; }
        internal virtual Devices IddeviceNavigation { get; set; }
    }
}
