using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CloudAPI.Rest.Client.Models;
using Microsoft.Rest;
using Microsoft.Rest.Serialization;
using Newtonsoft.Json;

namespace CloudAPI.Rest.Client {
    /// <summary>
    /// ASP.NET Core Web service using a REST API
    /// </summary>
    public partial class CloudClient : ServiceClient<CloudClient>, ICloudClient {
        /// <summary>
        /// The base URI of the service.
        /// </summary>
        public Uri BaseUri { get; set; }

        /// <summary>
        /// Gets or sets json serialization settings.
        /// </summary>
        public JsonSerializerSettings SerializationSettings { get; private set; }

        /// <summary>
        /// Gets or sets json deserialization settings.
        /// </summary>
        public JsonSerializerSettings DeserializationSettings { get; private set; }

        /// <summary>
        /// Initializes a new instance of the CloudClient class.
        /// </summary>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public CloudClient(params DelegatingHandler[] handlers) : base(handlers) {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CloudClient class.
        /// </summary>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        public CloudClient(HttpClientHandler rootHandler,
            params DelegatingHandler[] handlers) : base(rootHandler, handlers) {
            Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the CloudClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public CloudClient(Uri baseUri, params DelegatingHandler[] handlers) : this(handlers) {
            if (baseUri == null) {
                throw new ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// Initializes a new instance of the CloudClient class.
        /// </summary>
        /// <param name='baseUri'>
        /// Optional. The base URI of the service.
        /// </param>
        /// <param name='rootHandler'>
        /// Optional. The http client handler used to handle http transport.
        /// </param>
        /// <param name='handlers'>
        /// Optional. The delegating handlers to add to the http client pipeline.
        /// </param>
        /// <exception cref="System.ArgumentNullException">
        /// Thrown when a required parameter is null
        /// </exception>
        public CloudClient(Uri baseUri,
            HttpClientHandler rootHandler,
            params DelegatingHandler[] handlers) : this(rootHandler, handlers) {
            if (baseUri == null) {
                throw new ArgumentNullException("baseUri");
            }
            BaseUri = baseUri;
        }

        /// <summary>
        /// An optional partial-method to perform custom initialization.
        ///</summary> 
        partial void CustomInitialize();

        /// <summary>
        /// Initializes client properties.
        /// </summary>
        private void Initialize() {
            BaseUri = new Uri("http://localhost:5000/");
            SerializationSettings = new JsonSerializerSettings {
                Formatting = Formatting.Indented,
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter> {
                    new Iso8601TimeSpanConverter()
                }
            };
            DeserializationSettings = new JsonSerializerSettings {
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                NullValueHandling = NullValueHandling.Ignore,
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                ContractResolver = new ReadOnlyJsonContractResolver(),
                Converters = new List<JsonConverter> {
                    new Iso8601TimeSpanConverter()
                }
            };
            CustomInitialize();
        }

        /// <summary>
        /// Gets all HistoricData Values
        /// </summary>
        /// <remarks>
        /// Returns a JSON array of Historic Data items
        /// </remarks>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async
            Task
            <HttpOperationResponse<IList<HistoricData>>> GetDataAsync(
                Dictionary<string, List<string>> customHeaders =
                    null,
                CancellationToken cancellationToken = default(CancellationToken)) {
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace) {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters =
                    new Dictionary<string, object>();
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "GetData", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url =
                new Uri(new Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "api/historicdata")
                    .ToString();
            // Create HTTP transport objects
            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new Uri(_url);
            // Set Headers
            if (customHeaders != null) {
                foreach (var _header in customHeaders) {
                    if (_httpRequest.Headers.Contains(_header.Key)) {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            // Send Request
            if (_shouldTrace) {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace) {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int) _statusCode != 200) {
                var ex =
                    new HttpOperationException(
                        $"Operation returned an invalid status code '{_statusCode}'");
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                } else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace) {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null) {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<IList<HistoricData>>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int) _statusCode == 200) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body =
                        SafeJsonConvert
                            .DeserializeObject<IList<HistoricData>>(_responseContent,
                                DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.",
                        _responseContent, ex);
                }
            }
            if (_shouldTrace) {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

        /// <summary>
        /// Adds new HistoricData
        /// </summary>
        /// <param name='nou'>
        /// new HistoricData to be added
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<object>> PostDataAsync(
            HistoricData nou = default(HistoricData),
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) {
            nou?.Validate();
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace) {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters =
                    new Dictionary<string, object>();
                tracingParameters.Add("nou", nou);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "PostDataAsync", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url =
                new Uri(new Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "api/historicdata")
                    .ToString();
            // Create HTTP transport objects
            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("POST");
            _httpRequest.RequestUri = new Uri(_url);
            // Set Headers
            if (customHeaders != null) {
                foreach (var _header in customHeaders) {
                    if (_httpRequest.Headers.Contains(_header.Key)) {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            if (nou != null) {
                _requestContent = SafeJsonConvert.SerializeObject(nou,
                    SerializationSettings);
                _httpRequest.Content = new StringContent(_requestContent, Encoding.UTF8);
                _httpRequest.Content.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            }
            // Send Request
            if (_shouldTrace) {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace) {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int) _statusCode != 201 && (int) _statusCode != 400 && (int) _statusCode != 409) {
                var ex =
                    new HttpOperationException(
                        $"Operation returned an invalid status code '{_statusCode}'");
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                } else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace) {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null) {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int) _statusCode == 201) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body =
                        SafeJsonConvert.DeserializeObject<HistoricData>(_responseContent,
                            DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.",
                        _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int) _statusCode == 400) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body =
                        SafeJsonConvert.DeserializeObject<object>(_responseContent,
                            DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.",
                        _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int) _statusCode == 409) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body =
                        SafeJsonConvert.DeserializeObject<object>(_responseContent,
                            DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.",
                        _responseContent, ex);
                }
            }
            if (_shouldTrace) {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

        /// <summary>
        /// Gets specific HistoricData
        /// </summary>
        /// <param name='id'>
        ///     HistoricData identifier
        /// </param>
        /// <param name='customHeaders'>
        ///     Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        ///     The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<object>> GetDataAsync(int id,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) {
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace) {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters =
                    new Dictionary<string, object>();
                tracingParameters.Add("id", id);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "GetData", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url =
                new Uri(new Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "api/historicdata/{id}")
                    .ToString();
            _url = _url.Replace("{id}",
                Uri.EscapeDataString(
                    SafeJsonConvert.SerializeObject(id, SerializationSettings)
                        .Trim('"')));
            // Create HTTP transport objects
            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("GET");
            _httpRequest.RequestUri = new Uri(_url);
            // Set Headers
            if (customHeaders != null) {
                foreach (var _header in customHeaders) {
                    if (_httpRequest.Headers.Contains(_header.Key)) {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            // Send Request
            if (_shouldTrace) {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace) {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int) _statusCode != 200 && (int) _statusCode != 404) {
                var ex =
                    new HttpOperationException(
                        $"Operation returned an invalid status code '{_statusCode}'");
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                } else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace) {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null) {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int) _statusCode == 200) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body =
                        SafeJsonConvert.DeserializeObject<HistoricData>(_responseContent,
                            DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.",
                        _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int) _statusCode == 404) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body = SafeJsonConvert.DeserializeObject<object>(_responseContent, DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace) {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

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
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse> PutDataAsync(int id,
            HistoricData nou = default(HistoricData),
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) {
            nou?.Validate();
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace) {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters =
                    new Dictionary<string, object>();
                tracingParameters.Add("id", id);
                tracingParameters.Add("nou", nou);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "PutData", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url =
                new Uri(new Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "api/historicdata/{id}")
                    .ToString();
            _url = _url.Replace("{id}",
                Uri.EscapeDataString(
                    SafeJsonConvert.SerializeObject(id, SerializationSettings)
                        .Trim('"')));
            // Create HTTP transport objects
            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("PUT");
            _httpRequest.RequestUri = new Uri(_url);
            // Set Headers
            if (customHeaders != null) {
                foreach (var _header in customHeaders) {
                    if (_httpRequest.Headers.Contains(_header.Key)) {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            if (nou != null) {
                _requestContent = SafeJsonConvert.SerializeObject(nou,
                    SerializationSettings);
                _httpRequest.Content = new StringContent(_requestContent, Encoding.UTF8);
                _httpRequest.Content.Headers.ContentType =
                    MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
            }
            // Send Request
            if (_shouldTrace) {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace) {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int) _statusCode != 204 && (int) _statusCode != 404 && (int) _statusCode != 400) {
                var ex =
                    new HttpOperationException(
                        $"Operation returned an invalid status code '{_statusCode}'");
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                } else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace) {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null) {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int) _statusCode == 404) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body = SafeJsonConvert.DeserializeObject<object>(_responseContent, DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int) _statusCode == 400) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body = SafeJsonConvert.DeserializeObject<object>(_responseContent, DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace) {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }

        /// <summary>
        /// Deletes specific HistoricData
        /// </summary>
        /// <param name='id'>
        /// HistoricData identifier
        /// </param>
        /// <param name='customHeaders'>
        /// Headers that will be added to request.
        /// </param>
        /// <param name='cancellationToken'>
        /// The cancellation token.
        /// </param>
        /// <exception cref="Microsoft.Rest.HttpOperationException">
        /// Thrown when the operation returned an invalid status code
        /// </exception>
        /// <exception cref="SerializationException">
        /// Thrown when unable to deserialize the response
        /// </exception>
        /// <return>
        /// A response object containing the response body and response headers.
        /// </return>
        public async Task<HttpOperationResponse<object>> DeleteDataAsync(
            int id,
            Dictionary<string, List<string>> customHeaders = null,
            CancellationToken cancellationToken = default(CancellationToken)) {
            // Tracing
            bool _shouldTrace = ServiceClientTracing.IsEnabled;
            string _invocationId = null;
            if (_shouldTrace) {
                _invocationId = ServiceClientTracing.NextInvocationId.ToString();
                Dictionary<string, object> tracingParameters =
                    new Dictionary<string, object>();
                tracingParameters.Add("id", id);
                tracingParameters.Add("cancellationToken", cancellationToken);
                ServiceClientTracing.Enter(_invocationId, this, "DeleteData", tracingParameters);
            }
            // Construct URL
            var _baseUrl = BaseUri.AbsoluteUri;
            var _url =
                new Uri(new Uri(_baseUrl + (_baseUrl.EndsWith("/") ? "" : "/")), "api/historicdata/{id}")
                    .ToString();
            _url = _url.Replace("{id}",
                Uri.EscapeDataString(
                    SafeJsonConvert.SerializeObject(id, SerializationSettings)
                        .Trim('"')));
            // Create HTTP transport objects
            HttpRequestMessage _httpRequest = new HttpRequestMessage();
            HttpResponseMessage _httpResponse = null;
            _httpRequest.Method = new HttpMethod("DELETE");
            _httpRequest.RequestUri = new Uri(_url);
            // Set Headers
            if (customHeaders != null) {
                foreach (var _header in customHeaders) {
                    if (_httpRequest.Headers.Contains(_header.Key)) {
                        _httpRequest.Headers.Remove(_header.Key);
                    }
                    _httpRequest.Headers.TryAddWithoutValidation(_header.Key, _header.Value);
                }
            }

            // Serialize Request
            string _requestContent = null;
            // Send Request
            if (_shouldTrace) {
                ServiceClientTracing.SendRequest(_invocationId, _httpRequest);
            }
            cancellationToken.ThrowIfCancellationRequested();
            _httpResponse = await HttpClient.SendAsync(_httpRequest, cancellationToken).ConfigureAwait(false);
            if (_shouldTrace) {
                ServiceClientTracing.ReceiveResponse(_invocationId, _httpResponse);
            }
            HttpStatusCode _statusCode = _httpResponse.StatusCode;
            cancellationToken.ThrowIfCancellationRequested();
            string _responseContent = null;
            if ((int) _statusCode != 200 && (int) _statusCode != 404) {
                var ex =
                    new HttpOperationException(
                        $"Operation returned an invalid status code '{_statusCode}'");
                if (_httpResponse.Content != null) {
                    _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                } else {
                    _responseContent = string.Empty;
                }
                ex.Request = new HttpRequestMessageWrapper(_httpRequest, _requestContent);
                ex.Response = new HttpResponseMessageWrapper(_httpResponse, _responseContent);
                if (_shouldTrace) {
                    ServiceClientTracing.Error(_invocationId, ex);
                }
                _httpRequest.Dispose();
                if (_httpResponse != null) {
                    _httpResponse.Dispose();
                }
                throw ex;
            }
            // Create Result
            var _result = new HttpOperationResponse<object>();
            _result.Request = _httpRequest;
            _result.Response = _httpResponse;
            // Deserialize Response
            if ((int) _statusCode == 200) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body =
                        SafeJsonConvert.DeserializeObject<HistoricData>(_responseContent,
                            DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.",
                        _responseContent, ex);
                }
            }
            // Deserialize Response
            if ((int) _statusCode == 404) {
                _responseContent = await _httpResponse.Content.ReadAsStringAsync().ConfigureAwait(false);
                try {
                    _result.Body = SafeJsonConvert.DeserializeObject<object>(_responseContent, DeserializationSettings);
                } catch (JsonException ex) {
                    _httpRequest.Dispose();
                    if (_httpResponse != null) {
                        _httpResponse.Dispose();
                    }
                    throw new SerializationException("Unable to deserialize the response.", _responseContent, ex);
                }
            }
            if (_shouldTrace) {
                ServiceClientTracing.Exit(_invocationId, _result);
            }
            return _result;
        }
    }
}
