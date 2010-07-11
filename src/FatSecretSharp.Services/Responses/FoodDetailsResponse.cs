using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FatSecretSharp.Services.Responses
{
    /// <summary>
    /// Nutritional information by serving for a food.
    /// </summary>
    public class ServingInfo
    {
        public string serving_id { get; set; }
        public string serving_description { get; set; }
        public string serving_url { get; set; }
        public double metric_serving_amount { get; set; }
        public string metric_serving_unit { get; set; }
        public double number_of_units { get; set; }
        public string measurement_description { get; set; }
        public double calories { get; set; }
        public double carbohydrate { get; set; }
        public double protein { get; set; }
        public double fat { get; set; }
        public double saturated_fat { get; set; }
        public double polyunsaturated_fat { get; set; }
        public double monounsaturated_fat { get; set; }
        public double trans_fat { get; set; }
        public double cholesterol { get; set; }
        public double sodium { get; set; }
        public double potassium { get; set; }
        public double fiber { get; set; }
        public double sugar { get; set; }
        public int vitamin_a { get; set; }
        public int vitamin_c { get; set; }
        public int calcium { get; set; }
        public int iron { get; set; }
    }

    /// <summary>
    /// A holder for serving information, because the FatSecret API is a tad bit verbose.
    /// </summary>
    public class ServingHolder
    {
        public List<ServingInfo> serving { get; set; }
    }

    /// <summary>
    /// A wrapper around servings details.
    /// </summary>
    public class FoodServingsDetails : FoodInfo
    {
        public ServingHolder servings { get; set; }        
    }

    /// <summary>
    /// The food details response details.
    /// </summary>
    public class FoodDetailsResponse
    {
        /// <summary>
        /// Gets or sets the food.
        /// </summary>
        /// <value>The found food item.</value>
        public FoodServingsDetails food { get; set; }

        public bool HasResponse
        {
            get { return food != null && food.servings != null; }
        }
    }
}
