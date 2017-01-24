
namespace CloudAPI.Rest.Client.Models
{
    using System.Linq;

    public partial class JsonPatchDocumentDevices
    {
        /// <summary>
        /// Initializes a new instance of the JsonPatchDocumentDevices class.
        /// </summary>
        public JsonPatchDocumentDevices() { }

        /// <summary>
        /// Initializes a new instance of the JsonPatchDocumentDevices class.
        /// </summary>
        public JsonPatchDocumentDevices(System.Collections.Generic.IList<OperationDevices> operations = default(System.Collections.Generic.IList<OperationDevices>))
        {
            Operations = operations;
        }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "operations")]
        public System.Collections.Generic.IList<OperationDevices> Operations { get; private set; }

    }
}
