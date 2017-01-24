
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
            /// Gets all devices definitions
            /// </summary>
            /// <remarks>
            /// Returns a JSON array of Devices
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static System.Collections.Generic.IList<Devices> ApiDevicesGet(this ICloudClient operations)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiDevicesGetAsync(), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets all devices definitions
            /// </summary>
            /// <remarks>
            /// Returns a JSON array of Devices
            /// </remarks>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<System.Collections.Generic.IList<Devices>> ApiDevicesGetAsync(this ICloudClient operations, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiDevicesGetWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Adds new Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nou'>
            /// new logic Device definition to be added
            /// </param>
            public static object ApiDevicesPost(this ICloudClient operations, Devices nou = default(Devices))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiDevicesPostAsync(nou), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Adds new Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='nou'>
            /// new logic Device definition to be added
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiDevicesPostAsync(this ICloudClient operations, Devices nou = default(Devices), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiDevicesPostWithHttpMessagesAsync(nou, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Gets specific Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            public static object ApiDevicesByIdGet(this ICloudClient operations, int id)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiDevicesByIdGetAsync(id), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Gets specific Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiDevicesByIdGetAsync(this ICloudClient operations, int id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiDevicesByIdGetWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Updates existing Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            /// <param name='nou'>
            /// Device to be updated
            /// </param>
            public static object ApiDevicesByIdPut(this ICloudClient operations, int id, Devices nou = default(Devices))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiDevicesByIdPutAsync(id, nou), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Updates existing Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            /// <param name='nou'>
            /// Device to be updated
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiDevicesByIdPutAsync(this ICloudClient operations, int id, Devices nou = default(Devices), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiDevicesByIdPutWithHttpMessagesAsync(id, nou, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Deletes specific Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            public static object ApiDevicesByIdDelete(this ICloudClient operations, int id)
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiDevicesByIdDeleteAsync(id), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Deletes specific Device
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiDevicesByIdDeleteAsync(this ICloudClient operations, int id, System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiDevicesByIdDeleteWithHttpMessagesAsync(id, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <summary>
            /// Updates some Device information
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            /// <param name='patch'>
            /// Device updated information
            /// </param>
            public static object ApiDevicesByIdPatch(this ICloudClient operations, int id, JsonPatchDocumentDevices patch = default(JsonPatchDocumentDevices))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiDevicesByIdPatchAsync(id, patch), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Updates some Device information
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// Device identifier
            /// </param>
            /// <param name='patch'>
            /// Device updated information
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiDevicesByIdPatchAsync(this ICloudClient operations, int id, JsonPatchDocumentDevices patch = default(JsonPatchDocumentDevices), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiDevicesByIdPatchWithHttpMessagesAsync(id, patch, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
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

            /// <summary>
            /// Updates some HistoricData information
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            /// <param name='patch'>
            /// HistoricData updated information
            /// </param>
            public static object ApiHistoricdataByIdPatch(this ICloudClient operations, int id, JsonPatchDocumentHistoricData patch = default(JsonPatchDocumentHistoricData))
            {
                return System.Threading.Tasks.Task.Factory.StartNew(s => ((ICloudClient)s).ApiHistoricdataByIdPatchAsync(id, patch), operations, System.Threading.CancellationToken.None, System.Threading.Tasks.TaskCreationOptions.None, System.Threading.Tasks.TaskScheduler.Default).Unwrap().GetAwaiter().GetResult();
            }

            /// <summary>
            /// Updates some HistoricData information
            /// </summary>
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='id'>
            /// HistoricData identifier
            /// </param>
            /// <param name='patch'>
            /// HistoricData updated information
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async System.Threading.Tasks.Task<object> ApiHistoricdataByIdPatchAsync(this ICloudClient operations, int id, JsonPatchDocumentHistoricData patch = default(JsonPatchDocumentHistoricData), System.Threading.CancellationToken cancellationToken = default(System.Threading.CancellationToken))
            {
                using (var _result = await operations.ApiHistoricdataByIdPatchWithHttpMessagesAsync(id, patch, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}
