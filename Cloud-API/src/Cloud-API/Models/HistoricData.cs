using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class HistoricData {
        public int IdhistoricData { get; set; }
        /// <value>1</value>
        [Required(ErrorMessage = "Device id missing")]
        public int Iddevice { get; set; }
        /// <value>null</value>
        public DateTime? HistDataDate { get; set; }
        /// <value>123</value>
        [Required(ErrorMessage = "Data Value missing")]
        public string HistDataValue { get; set; }
        /// <value>1</value>
        [Required(ErrorMessage = "Data type id missing")]
        public int IddataType { get; set; }
        /// <value>false</value>
        [DefaultValue(false)]
        public bool HistDataToDevice { get; set; } = false;
        /// <value>false</value>
        [DefaultValue(false)]
        public bool HistDataAck { get; set; } = false;
        /// <value>null</value>
        [DefaultValue(null)]
        public string HistDataAux { get; set; } = null;

        internal virtual AuxDataType IddataTypeNavigation { get; set; }
        internal virtual Devices IddeviceNavigation { get; set; }
    }
}
