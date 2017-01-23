using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CloudAPI.Rest.Client.Models;
using Microsoft.Rest;
using Newtonsoft.Json;

namespace CloudAPI.Rest.Client {
    /// <summary>
    /// ASP.NET Core Web service using a REST API
    /// </summary>
    public interface ICloudClient : IDisposable {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        JsonSerializerSettings SerializationSettings { get; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        JsonSerializerSettings DeserializationSettings { get; }


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
        Task
            <HttpOperationResponse<IList<HistoricData>>>
            GetDataAsync(
                Dictionary<string, List<string>> customHeaders =
                    null,
                CancellationToken cancellationToken = default(CancellationToken));

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
        Task<HttpOperationResponse<object>> PostDataAsync(HistoricData nou = default(HistoricData),
            Dictionary<string, List<string>> customHeaders =
                null,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets specific HistoricData
        /// </summary>
        /// <param name='id'>
        ///     HistoricData identifier
        /// </param>
        /// <param name='customHeaders'>
        ///     The headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        ///     The cancellation token.
        /// </param>
        Task<HttpOperationResponse<object>> GetDataAsync(int id,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken));

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
        Task<HttpOperationResponse> PutDataAsync(
            int id,
            HistoricData nou = default(HistoricData),
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken));

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
        Task<HttpOperationResponse<object>>
            DeleteDataAsync(int id,
                Dictionary<string, List<string>> customHeaders =
                    null,
                CancellationToken cancellationToken = default(CancellationToken));
    }
}
