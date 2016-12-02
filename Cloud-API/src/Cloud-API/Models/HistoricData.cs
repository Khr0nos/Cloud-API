using System;

namespace Cloud_API.Models {
    public partial class HistoricData {
        public int IdhistoricData { get; set; }
        public int Iddevice { get; set; }
        public DateTime HistDataDate { get; set; }
        public string HistDataValue { get; set; }
        public int IddataType { get; set; }
        public bool HistDataToDevice { get; set; }
        public bool HistDataAck { get; set; }
        public string HistDataAux { get; set; }

        public virtual AuxDataType IddataTypeNavigation { get; set; }
        public virtual Devices IddeviceNavigation { get; set; }
    }
}
