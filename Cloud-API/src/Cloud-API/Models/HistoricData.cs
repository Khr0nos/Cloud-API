using System;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class HistoricData {
        [Required]
        public int IdhistoricData { get; set; }
        public int Iddevice { get; set; }
        public DateTime HistDataDate { get; set; }
        public string HistDataValue { get; set; }
        public int IddataType { get; set; }
        public bool HistDataToDevice { get; set; }
        public bool HistDataAck { get; set; }
        public string HistDataAux { get; set; }

        internal virtual AuxDataType IddataTypeNavigation { get; set; }
        internal virtual Devices IddeviceNavigation { get; set; }
    }
}
