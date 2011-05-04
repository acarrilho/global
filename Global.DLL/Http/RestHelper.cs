using System;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Web;
using Microsoft.Http;

namespace Global.Http
{
    /// <summary>
    /// A collection of methods that allows performing tasks on REST services such as GET, POST, PUT and DELETE operations.
    /// </summary>
    public static class RestHelper
    {
        /// <summary>
        /// Makes a GET request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <param name="url">The full url for the request.</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestGet<TReturn>(string url)
        {
            return HttpRestGet<TReturn>(url, WebMessageFormat.Xml);
        }
        /// <summary>
        /// Makes a GET request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestGet<TReturn>(string baseUrl, string url)
        {
            return HttpRestGet<TReturn>(baseUrl, url, WebMessageFormat.Xml);
        }
        /// <summary>
        /// Makes a GET request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <param name="url">The full url for the request.</param>
        /// <param name="messageFormat">The message format of the return object (XML or Json).</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestGet<TReturn>(string url, WebMessageFormat messageFormat)
        {
            return HttpRestGet<TReturn>(url, null, messageFormat);
        }
        /// <summary>
        /// Makes a GET request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <param name="messageFormat">The message format of the return object (XML or Json).</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestGet<TReturn>(string baseUrl, string url, WebMessageFormat messageFormat)
        {
            HttpClient httpClient = new HttpClient(baseUrl);
            HttpResponseMessage httpResponse = (!string.IsNullOrEmpty(url))
                ? httpClient.Get(url) : httpClient.Get();
            httpResponse.EnsureStatusIsSuccessful();
            return httpResponse.ConvertHttpContentToObject<TReturn>(messageFormat);
        }

        /// <summary>
        /// Makes a POST request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <typeparam name="TContentObject">The object type passed into the request.</typeparam>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <param name="contentObject">The actual object passed into th request.</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestPost<TReturn, TContentObject>(string baseUrl, string url, TContentObject contentObject)
        {
            return HttpRestPost<TReturn, TContentObject>(baseUrl, url, contentObject, WebMessageFormat.Xml);
        }
        /// <summary>
        /// Makes a POST request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <typeparam name="TContentObject">The object type passed into the request.</typeparam>
        /// <param name="url">The full url for the request.</param>
        /// <param name="contentObject">The actual object passed into th request.</param>
        /// <param name="messageFormat">The message format of both the income and return object (XML or Json).</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestPost<TReturn, TContentObject>(string url, TContentObject contentObject, WebMessageFormat messageFormat)
        {
            return HttpRestPost<TReturn, TContentObject>(url, null, contentObject, messageFormat);
        }
        /// <summary>
        /// Makes a POST request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <typeparam name="TContentObject">The object type passed into the request.</typeparam>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <param name="contentObject">The actual object passed into th request.</param>
        /// <param name="messageFormat">The message format of both the income and return object (XML or Json).</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestPost<TReturn, TContentObject>(string baseUrl, string url, TContentObject contentObject, WebMessageFormat messageFormat)
        {
            HttpClient httpClient = new HttpClient(baseUrl);
            HttpResponseMessage httpResponse;
            
            if (!string.IsNullOrEmpty(url))
            {
                httpResponse = httpClient.Post(url, 
                    ConvertObjectToHttpContent<TContentObject>(contentObject, messageFormat));
            }
            else
            {
                httpResponse = httpClient.Post(String.Empty, 
                    ConvertObjectToHttpContent<TContentObject>(contentObject, messageFormat));
            }

            httpResponse.EnsureStatusIsSuccessful();
            return httpResponse.ConvertHttpContentToObject<TReturn>(messageFormat);
        }

        /// <summary>
        /// Makes a PUT request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <typeparam name="TContentObject">The object type passed into the request.</typeparam>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <param name="contentObject">The actual object passed into th request.</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestPut<TReturn, TContentObject>(string baseUrl, string url, TContentObject contentObject)
        {
            return HttpRestPut<TReturn, TContentObject>(baseUrl, url, contentObject, WebMessageFormat.Xml);
        }
        /// <summary>
        /// Makes a PUT request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <typeparam name="TContentObject">The object type passed into the request.</typeparam>
        /// <param name="url">The full url for the request.</param>
        /// <param name="contentObject">The actual object passed into th request.</param>
        /// <param name="messageFormat">The message format of both the income and return object (XML or Json).</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestPut<TReturn, TContentObject>(string url, TContentObject contentObject, WebMessageFormat messageFormat)
        {
            return HttpRestPut<TReturn, TContentObject>(url, null, contentObject, messageFormat);
        }
        /// <summary>
        /// Makes a PUT request to a REST service.
        /// </summary>
        /// <typeparam name="TReturn">The return object type.</typeparam>
        /// <typeparam name="TContentObject">The object type passed into the request.</typeparam>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <param name="contentObject">The actual object passed into th request.</param>
        /// <param name="messageFormat">The message format of both the income and return object (XML or Json).</param>
        /// <returns>The specified object for the request.</returns>
        public static TReturn HttpRestPut<TReturn, TContentObject>(string baseUrl, string url, TContentObject contentObject, WebMessageFormat messageFormat)
        {
            HttpClient httpClient = new HttpClient(baseUrl);
            HttpResponseMessage httpResponse;

            if (!string.IsNullOrEmpty(url))
            {
                httpResponse = httpClient.Put(url,
                    ConvertObjectToHttpContent<TContentObject>(contentObject, messageFormat));
            }
            else
            {
                httpResponse = httpClient.Put(String.Empty,
                    ConvertObjectToHttpContent<TContentObject>(contentObject, messageFormat));
            }

            httpResponse.EnsureStatusIsSuccessful();
            return httpResponse.ConvertHttpContentToObject<TReturn>(messageFormat);
        }

        /// <summary>
        /// Makes a DELETE request to a REST service.
        /// </summary>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <returns>A boolean indicating if the request was successful.</returns>
        public static bool HttpRestDelete(string baseUrl, string url)
        {
            return HttpRestDelete(baseUrl, url, WebMessageFormat.Xml);
        }
        /// <summary>
        /// Makes a DELETE request to a REST service.
        /// </summary>
        /// <param name="url">The full url for the request.</param>
        /// <param name="messageFormat">The message format of the income object (XML or Json).</param>
        /// <returns>A boolean indicating if the request was successful.</returns>
        public static bool HttpRestDelete(string url, WebMessageFormat messageFormat)
        {
            return HttpRestDelete(url, null, messageFormat);
        }
        /// <summary>
        /// Makes a DELETE request to a REST service.
        /// </summary>
        /// <param name="baseUrl">The base url for the request (eg: http://www.example.com/services/).</param>
        /// <param name="url">The relative url for the request.</param>
        /// <param name="messageFormat">The message format of the income object (XML or Json).</param>
        /// <returns>A boolean indicating if the request was successful.</returns>
        public static bool HttpRestDelete(string baseUrl, string url, WebMessageFormat messageFormat)
        {
            HttpClient httpClient = new HttpClient(baseUrl);
            HttpResponseMessage httpResponse = !string.IsNullOrEmpty(url) 
                ? httpClient.Delete(url) 
                : httpClient.Delete(String.Empty);
            httpResponse.EnsureStatusIsSuccessful();
            return httpResponse.ConvertHttpContentToObject<bool>(messageFormat);
        }

        private static HttpContent ConvertObjectToHttpContent<TContentObject>(TContentObject contentObject, 
            WebMessageFormat messageFormat)
        {
            HttpContent content;
            switch (messageFormat)
            {
                case WebMessageFormat.Json:
                    content = HttpContentExtensions.CreateJsonDataContract<TContentObject>(contentObject);
                    break;
                case WebMessageFormat.Xml:
                    content = HttpContentExtensions.CreateDataContract<TContentObject>(contentObject);
                    break;
                default:
                    content = HttpContentExtensions.CreateDataContract<TContentObject>(contentObject);
                    break;
            }

            return content;
        }
        private static TContentObject ConvertHttpContentToObject<TContentObject>(this HttpResponseMessage httpResponse, 
            WebMessageFormat messageFormat)
        {
            switch (messageFormat)
            {
                case WebMessageFormat.Json:
                    return httpResponse.Content.ReadAsJsonDataContract<TContentObject>();
                case WebMessageFormat.Xml:
                    return httpResponse.Content.ReadAsDataContract<TContentObject>();
                default:
                    return httpResponse.Content.ReadAsDataContract<TContentObject>();
            }
        }

        /// <summary>
        /// Gets a parameter from the request and parse it to int.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <returns>The parsed int value.</returns>
        static public int GetInt(string parmName)
        {
            return GetInt(parmName, 0, false);
        }
        /// <summary>
        /// Gets a parameter from the request and parse it to int.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <param name="defaultValue">The default value in case the parse gives an error.</param>
        /// <returns>The parsed int value.</returns>
        static public int GetInt(string parmName, int defaultValue)
        {
            return GetInt(parmName, defaultValue, false);
        }
        /// <summary>
        /// Gets a required parameter from the request and parse it to int.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <returns>The parsed int value or an exception if the parameter does not exist.</returns>
        static public int GetRequiredInt(string parmName)
        {
            return GetInt(parmName, 0, true);
        }
        /// <summary>
        /// Gets a parameter from the request and parse it to int.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <param name="defaultValue">The default value in case the parse gives an error.</param>
        /// <param name="isRequired">Defines if the parameter is required.</param>
        /// <returns>The parsed int value or an exception if the parameter is required.</returns>
        static public int GetInt(string parmName, int defaultValue, bool isRequired)
        {
            string value = GetString(parmName, defaultValue.ToString(), isRequired);
            int returnValue = defaultValue;

            if (!string.IsNullOrEmpty(value))
            {
                if (!Int32.TryParse(value, out returnValue))
                    SetBadRequest(string.Format("Invalid query parameter \"{0}\", value \"{1}\"", parmName, value));
            }

            return returnValue;
        }

        /// <summary>
        /// Gets a parameter from the request and parse it to bool.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <returns>The parsed int value.</returns>
        static public bool GetBool(string parmName)
        {
            return GetBool(parmName, false, false);
        }
        /// <summary>
        /// Gets a parameter from the request and parse it to bool.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <param name="defaultValue">The default value in case the parse gives an error.</param>
        /// <returns>The parsed int value.</returns>
        static public bool GetBool(string parmName, bool defaultValue)
        {
            return GetBool(parmName, defaultValue, false);
        }
        /// <summary>
        /// Gets a required parameter from the request and parse it to bool.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <returns>The parsed int value or an exception if the parameter does not exist.</returns>
        static public bool GetRequiredBool(string parmName)
        {
            return GetBool(parmName, false, true);
        }
        /// <summary>
        /// Gets a parameter from the request and parse it to bool.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <param name="defaultValue">The default value in case the parse gives an error.</param>
        /// <param name="isRequired">Defines if the parameter is required.</param>
        /// <returns>The parsed int value or an exception if the parameter is required.</returns>
        static public bool GetBool(string parmName, bool defaultValue, bool isRequired)
        {
            string value = GetString(parmName, defaultValue.ToString(), isRequired);
            bool returnValue = defaultValue;

            if (!string.IsNullOrEmpty(value))
            {
                if (bool.TryParse(value, out returnValue))
                    SetBadRequest(string.Format("Invalid query parameter \"{0}\", value \"{1}\"", parmName, value));
            }
            return returnValue;
        }

        /// <summary>
        /// Gets a string parameter from the request.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <returns>The parameter value.</returns>
        static public string GetString(string parmName)
        {
            return GetString(parmName, string.Empty, false);
        }
        /// <summary>
        /// Gets a string parameter from the request.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <param name="defaultValue">A default value in case the parameter does not exist or is null or empty.</param>
        /// <returns>The parameter value.</returns>
        static public string GetString(string parmName, string defaultValue)
        {
            return GetString(parmName, defaultValue, false);
        }
        /// <summary>
        /// Gets a required string parameter from the request.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <returns>The parameter value.</returns>
        static public string GetRequiredString(string parmName)
        {
            return GetString(parmName, string.Empty, true);
        }
        /// <summary>
        /// Gets a string parameter from the request.
        /// </summary>
        /// <param name="parmName">The request parameter name.</param>
        /// <param name="defaultValue">A default value in case the parameter does not exist or is null or empty.</param>
        /// <param name="isRequired">Defines if the parameter is required.</param>
        /// <returns>The parameter value.</returns>
        static public string GetString(string parmName, string defaultValue, bool isRequired)
        {
            if (WebOperationContext.Current == null)
                throw new InvalidOperationException("WebOperationContext is null");

            string returnValue = WebOperationContext.Current.IncomingRequest.UriTemplateMatch.QueryParameters[parmName];
            // if the parameter is required then this is a bad request - this will throw ArgumentException
            if (isRequired && string.IsNullOrEmpty(returnValue))
            {
                SetBadRequest(string.Format("Missing required query parameter \"{0}\"", parmName));
            }
            else if (returnValue == null)
            {
                // If null (not found) use default value
                returnValue = defaultValue;
            }

            return returnValue;
        }

        /// <summary>
        /// Throws a bad request exception.
        /// </summary>
        /// <param name="description">The exceprion description.</param>
        static private void SetBadRequest(string description)
        {
            if (WebOperationContext.Current != null)
            {
                WebOperationContext.Current.OutgoingResponse.StatusDescription = description;
                WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                WebOperationContext.Current.OutgoingResponse.SuppressEntityBody = true;
            }
            throw new ArgumentException(description);
        }
    }
}
