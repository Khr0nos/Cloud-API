namespace Cloud_Background
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Devices
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Devices()
        {
            HistoricData = new HashSet<HistoricData>();
            HistoricDevices = new HashSet<HistoricDevices>();
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDDevice { get; set; }

        [Required]
        [StringLength(20)]
        public string DeviceName { get; set; }

        public int IDAuxDeviceType { get; set; }

        public bool DeviceEnabled { get; set; }

        public bool DeviceConnected { get; set; }

        public bool DeviceNeedLogin { get; set; }

        public int DeviceInterval { get; set; }

        public DateTime DeviceCreationDate { get; set; }

        [Required]
        [StringLength(20)]
        public string DeviceUsername { get; set; }

        [Required]
        [StringLength(20)]
        public string DevicePassword { get; set; }

        public int IDDeviceProtocol { get; set; }

        [StringLength(1024)]
        public string DeviceAux { get; set; }

        public virtual AuxDeviceProtocols AuxDeviceProtocols { get; set; }

        public virtual AuxDeviceType AuxDeviceType { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoricData> HistoricData { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HistoricDevices> HistoricDevices { get; set; }
    }
}
