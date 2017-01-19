namespace Cloud_Background
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HistoricDevices
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDHistoricDevices { get; set; }

        public int IDDevice { get; set; }

        public DateTime HistDeviceDate { get; set; }

        public int IDDeviceAction { get; set; }

        [StringLength(16)]
        public string HistDeviceIPaddress { get; set; }

        [StringLength(1024)]
        public string HistDeviceAux { get; set; }

        public virtual AuxDeviceActions AuxDeviceActions { get; set; }

        public virtual Devices Devices { get; set; }
    }
}
