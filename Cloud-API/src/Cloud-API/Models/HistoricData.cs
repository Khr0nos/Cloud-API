using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class HistoricData {
        public int IdhistoricData { get; set; }
        [Required(ErrorMessage = "Device id missing")]
        public int Iddevice { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime HistDataDate { get; set; }
        [Required(ErrorMessage = "Data Value missing")]
        public string HistDataValue { get; set; }
        [Required(ErrorMessage = "Data type id missing")]
        public int IddataType { get; set; }
        [DefaultValue(false)]
        public bool HistDataToDevice { get; set; } = false;
        [DefaultValue(false)]
        public bool HistDataAck { get; set; } = false;
        [DefaultValue(null)]
        public string HistDataAux { get; set; } = null;

        internal virtual AuxDataType IddataTypeNavigation { get; set; }
        internal virtual Devices IddeviceNavigation { get; set; }
    }
}
