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

        /// <summary>
        /// food_entry.create
        /// </summary>
        public static string FoodEntry_Create = "food_entry.create";

        /// <summary>
        /// food_entry.delete
        /// </summary>
        public static string FoodEntry_Delete = "food_entry.delete";

        /// <summary>
        /// food_entry.edit
        /// </summary>
        public static string FoodEntry_Edit = "food_entry.edit";

        /// <summary>
        /// food_entry.copy
        /// </summary>
        public static string FoodEntry_Copy = "food_entries.copy";

        /// <summary>
        /// food_entry.copy_saved_meal
        /// </summary>
        public static string FoodEntry_CopySavedMeal = "food_entries.copy_saved_meal";

        /// <summary>
        /// food_entries.get
        /// </summary>
        public static string FoodEntry_Get = "food_entries.get";
        
        /// <summary>
        /// food_entries.get_month
        /// </summary>
        public static string FoodEntry_GetMonth = "food_entries.get_month";
    }
}
