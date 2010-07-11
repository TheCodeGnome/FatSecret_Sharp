using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FatSecretSharp.Services.Responses
{
    

    /// <summary>
    /// A wrapper for the food search result.
    /// </summary>
    public class FoodSearchResult
    {
        public int max_results { get; set; }
        public int total_results { get; set; }
        public int page_number { get; set; }

        public List<FoodInfo> food { get; set; }
    }

    /// <summary>
    /// A wrapper for food search results.
    /// </summary>
    public class FoodSearchResponse
    {
        public FoodSearchResult foods { get; set; }
        public bool HasResults 
        {
            get 
            {
                return foods != null && foods.food != null && foods.food.Count > 0;
            }
        }
    }
}
