namespace Cloud_Background
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistoricData")]
    public partial class HistoricData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDHistoricData { get; set; }

        public int IDDevice { get; set; }

        public DateTime HistDataDate { get; set; }

        [Required]
        [StringLength(128)]
        public string HistDataValue { get; set; }

        public int IDDataType { get; set; }

        public bool HistDataToDevice { get; set; }

        public bool HistDataAck { get; set; }

        [StringLength(512)]
        public string HistDataAux { get; set; }

        public virtual AuxDataType AuxDataType { get; set; }

        public virtual Devices Devices { get; set; }
    }
}
