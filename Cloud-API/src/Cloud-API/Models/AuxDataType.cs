using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cloud_API.Models {
    public partial class AuxDataType {
        public AuxDataType() {
            HistoricData = new HashSet<HistoricData>();
        }

        [Required]
        public int IdauxDataType { get; set; }
        public string DataType { get; set; }

        internal virtual ICollection<HistoricData> HistoricData { get; set; }
    }
}
