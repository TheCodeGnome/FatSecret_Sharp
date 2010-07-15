using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        /// <summary>
        /// profile.create
        /// </summary>
        public static string Profile_Create = "profile.create";
        
        /// <summary>
        /// profile.get
        /// </summary>
        public static string Profile_Get = "profile.get";

        /// <summary>
        /// profile.get_auth
        /// </summary>
        public static string Profile_Get_Auth = "profile.get_auth";

        /// <summary>
        /// weight.update
        /// </summary>
        public static string Weight_Update = "weight.update";

        /// <summary>
        /// weights.get_month
        /// </summary>
        public static string Weight_GetMonth = "weights.get_month";
    }
}
