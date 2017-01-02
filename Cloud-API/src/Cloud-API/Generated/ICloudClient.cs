
namespace CloudAPI.Rest.Client {
    using Models;

    /// <summary>
    /// ASP.NET Core Web service using a REST API
    /// </summary>
    public partial interface ICloudClient : System.IDisposable {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        System.Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        Newtonsoft.Json.JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        Newtonsoft.Json.JsonSerializerSettings DeserializationSettings { get; }


        /// <summary>
        /// Gets all HistoricData Values
        /// </summary>
        /// <remarks>
        /// Returns a JSON array of Historic Data items
        /// </remarks>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        System.Threading.Tasks.Task
            <Microsoft.Rest.HttpOperationResponse<System.Collections.Generic.IList<HistoricData>>>
            GetDataAsync(
                System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> customHeaders =
                    null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Adds new HistoricData
        /// </summary>
        /// <param name='nou'>
        /// new HistoricData to be added
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        System.Threading.Tasks.Task<Microsoft.Rest.HttpOperationResponse<HistoricData>>
            PostDataAsync(HistoricData nou = default(HistoricData),
                System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> customHeaders =
                    null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Gets specific HistoricData
        /// </summary>
        /// <param name='id'>
        /// HistoricData identifier
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        System.Threading.Tasks.Task<Microsoft.Rest.HttpOperationResponse<HistoricData>>
            GetDataAsync(int id,
                System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> customHeaders =
                    null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Updates existing HistoricData
        /// </summary>
        /// <param name='id'>
        /// HistoricData identifier
        /// </param>
        /// <param name='nou'>
        /// HistoricData to be updated
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        System.Threading.Tasks.Task<Microsoft.Rest.HttpOperationResponse> PutDataAsync(
            int id,
            HistoricData nou = default(HistoricData),
            System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> customHeaders = null,
            System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));

        /// <summary>
        /// Deletes specific HistoricData
        /// </summary>
        /// <param name='id'>
        /// HistoricData identifier
        /// </param>
        /// <param name='customHeaders'>
        /// The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        System.Threading.Tasks.Task<Microsoft.Rest.HttpOperationResponse<HistoricData>>
            DeleteDataAsync(int id,
                System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<string>> customHeaders =
                    null,
                System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken));
    }
}
