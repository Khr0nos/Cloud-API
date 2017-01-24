
namespace CloudAPI.Rest.Client.Models
{
    using System.Linq;

    public partial class JsonPatchDocumentHistoricData
    {
        /// <summary>
        /// Initializes a new instance of the JsonPatchDocumentHistoricData
        /// class.
        /// </summary>
        public JsonPatchDocumentHistoricData() { }

        /// <summary>
        /// Initializes a new instance of the JsonPatchDocumentHistoricData
        /// class.
        /// </summary>
        public JsonPatchDocumentHistoricData(System.Collections.Generic.IList<OperationHistoricData> operations = default(System.Collections.Generic.IList<OperationHistoricData>))
        {
            Operations = operations;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "operations")]
        public System.Collections.Generic.IList<OperationHistoricData> Operations { get; private set; }

    }
}
