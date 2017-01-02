

namespace CloudAPI.Rest.Client.Models {
    using System.Linq;

    public partial class HistoricData {
        /// <summary>
        /// Initializes a new instance of the HistoricData class.
        /// </summary>
        public HistoricData() {}

        /// <summary>
        /// Initializes a new instance of the HistoricData class.
        /// </summary>
        public HistoricData(int iddevice,
            string histDataValue,
            int iddataType,
            int? idhistoricData = default(int?),
            System.DateTime? histDataDate = default(System.DateTime?),
            bool? histDataToDevice = default(bool?),
            bool? histDataAck = default(bool?),
            string histDataAux = default(string)) {
            IdhistoricData = idhistoricData;
            Iddevice = iddevice;
            HistDataDate = histDataDate;
            HistDataValue = histDataValue;
            IddataType = iddataType;
            HistDataToDevice = histDataToDevice;
            HistDataAck = histDataAck;
            HistDataAux = histDataAux;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "idhistoricData")]
        public int? IdhistoricData { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "iddevice")]
        public int Iddevice { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "histDataDate")]
        public System.DateTime? HistDataDate { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "histDataValue")]
        public string HistDataValue { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "iddataType")]
        public int IddataType { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "histDataToDevice")]
        public bool? HistDataToDevice { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "histDataAck")]
        public bool? HistDataAck { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "histDataAux")]
        public string HistDataAux { get; set; }

        /// <summary>
        /// Validate the object.
        /// </summary>
        /// <exception cref="Microsoft.Rest.ValidationException">
        /// Thrown if validation fails
        /// </exception>
        public virtual void Validate() {
            if (HistDataValue == null) {
                throw new Microsoft.Rest.ValidationException(Microsoft.Rest.ValidationRules.CannotBeNull,
                    "HistDataValue");
            }
        }
    }
}
