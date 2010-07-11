using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace FatSecretSharp.Services.Common
{
    /// <summary>
    /// A centralized static list of FatSecret methods.
    /// </summary>
    public static class FatSecretAPIMethods
    {
        /// <summary>
        /// foods.search
        /// </summary>
        public static string Food_Search = "foods.search";
        /// <summary>
        /// food.get
        /// </summary>
        public static string Food_Get = "food.get";

        /// <summary>
        /// exercises.get
        /// </summary>
        public static string Exercise_Catalog = "exercises.get";
    }

    /// <summary>
    /// Strongly typed way of dealing with Http Methods.
    /// </summary>
    public enum HttpMethod
    {
        /// <summary>
        /// GET
        /// </summary>
        GET = 0,
        /// <summary>
        /// POST
        /// </summary>
        POST = 1
    }

    /// <summary>
    /// A helper class for building FatSecret Rest Url's.
    /// </summary>
    public class FatSecretUrlBuilder
    {
        private OAuthBase oAuth = new OAuthBase();
        private string apiUrl = "http://platform.fatsecret.com/rest/server.api";

        private string consumerKey = string.Empty;
        private string consumerSecret = string.Empty;

        public FatSecretUrlBuilder(string key, string secret)
        {
            consumerKey = key;
            consumerSecret = secret;
        }

        /// <summary>
        /// Creates the rest AP GET URL.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="secret">The secret.</param>
        /// <param name="optionalParameters">The optional parameters.</param>
        /// <returns>A fully fleshed FatSecret rest url ready to call.</returns>
        public string CreateRestAPIGETUrl(Dictionary<string, string> optionalParameters)
        {
            var parmString = string.Empty;
            if (optionalParameters != null && optionalParameters.Count > 0)
            {
                var tmpParm = optionalParameters.OrderBy(x => x.Key)
                                                .ThenBy(y => y.Value);

                var form = "{0}={1}";
                var nameValues = tmpParm.Select(s => string.Format(form, s.Key, s.Value)).ToArray();
                parmString = "?" + string.Join("&", nameValues);
            }

            string outUrl;
            string outParms;
            var baseUri = new Uri(string.Concat(apiUrl, parmString));

            var sig = oAuth.GenerateSignature(baseUri, consumerKey, consumerSecret, "GET", null, null, out outUrl, out outParms);

            var fullUrl = String.Format("{0}?{1}&oauth_signature={2}", outUrl, outParms, HttpUtility.UrlEncode(sig));

            return fullUrl;
        }
    }

    
}
