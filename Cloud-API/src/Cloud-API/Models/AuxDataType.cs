using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CloudAPI.Models {
    /// <summary>
    /// Model class for AuxDataType table entries
    /// </summary>
    public partial class AuxDataType {
        public AuxDataType() {
            HistoricData = new HashSet<HistoricData>();
        }
        /// <summary>
        /// Data type identifier
        /// </summary>
        [Required]
        public int IdauxDataType { get; set; }
        /// <summary>
        /// Data type description
        /// </summary>
        public string DataType { get; set; }

        internal virtual ICollection<HistoricData> HistoricData { get; set; }
    }
}
