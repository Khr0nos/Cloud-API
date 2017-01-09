
namespace CloudAPI.Rest.Client
{
    using System.Threading.Tasks;
   using Models;

    /// <summary>
    /// Extension methods for CloudClient.
    /// </summary>
    public static partial class CloudClientExtensions
    {
            /// <summary>
            /// Gets all HistoricData Values
            /// </summary>
            /// <remarks>
            /// Returns a JSON array of Historic Data items
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static System.Collections.Generic.IList<HistoricData> ApiHistoricdataGet(this ICloudClient operations)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiHistoricdataGetAsync(), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets all HistoricData Values
            /// </summary>
            /// <remarks>
            /// Returns a JSON array of Historic Data items
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<System.Collections.Generic.IList<HistoricData>> ApiHistoricdataGetAsync(this ICloudClient operations, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiHistoricdataGetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Adds new HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nou'>
            /// new HistoricData to be added
            /// </param>
            public static object ApiHistoricdataPost(this ICloudClient operations, HistoricData nou = default(HistoricData))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiHistoricdataPostAsync(nou), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Adds new HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nou'>
            /// new HistoricData to be added
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiHistoricdataPostAsync(this ICloudClient operations, HistoricData nou = default(HistoricData), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiHistoricdataPostWithHttpMessagesAsync(nou, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Gets specific HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            public static object ApiHistoricdataByIdGet(this ICloudClient operations, int id)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiHistoricdataByIdGetAsync(id), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets specific HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiHistoricdataByIdGetAsync(this ICloudClient operations, int id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiHistoricdataByIdGetWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Updates existing HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            /// <param name='nou'>
            /// HistoricData to be updated
            /// </param>
            public static object ApiHistoricdataByIdPut(this ICloudClient operations, int id, HistoricData nou = default(HistoricData))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiHistoricdataByIdPutAsync(id, nou), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Updates existing HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            /// <param name='nou'>
            /// HistoricData to be updated
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiHistoricdataByIdPutAsync(this ICloudClient operations, int id, HistoricData nou = default(HistoricData), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiHistoricdataByIdPutWithHttpMessagesAsync(id, nou, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes specific HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            public static object ApiHistoricdataByIdDelete(this ICloudClient operations, int id)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiHistoricdataByIdDeleteAsync(id), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes specific HistoricData
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiHistoricdataByIdDeleteAsync(this ICloudClient operations, int id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiHistoricdataByIdDeleteWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
