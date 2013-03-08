using System;
using System.ServiceModel.Web;

namespace Global.Http
{
    /// <summary>
    /// A collection of methods that allows performing tasks on REST services such as GET, POST, PUT and DELETE operations.
    /// </summary>
    public static class Rest
    {
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
                if (!bool.TryParse(value, out returnValue))
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
