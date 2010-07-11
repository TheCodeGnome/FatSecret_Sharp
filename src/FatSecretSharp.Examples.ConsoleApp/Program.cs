using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FatSecretSharp.Services;
using FatSecretSharp.Services.Requests;

namespace FatSecretSharp.Examples.ConsoleApp
{
    class Program
    {
        // Get you're api key from http://platform.fatsecret.com/api/Default.aspx?screen=r.
        
        // TODO: Replace with your key / secret so you don't have to enter it every time.
        private static string consumerKey = string.Empty;
        private static string consumerSecret = string.Empty;

        private static FoodSearch foodSearch;
        private static FoodDetails foodDetail;
        private static ExerciseCatalog exerCatalog;

        static void Main(string[] args)
        {
            if (String.IsNullOrEmpty(consumerKey))
            {
                Console.WriteLine("Enter your API Key:");
                consumerKey = Console.ReadLine();
            }
            if (String.IsNullOrEmpty(consumerSecret))
            {
                Console.WriteLine("Enter your API Secret:");
                consumerSecret = Console.ReadLine();
            }

            Console.WriteLine(string.Empty);

            int choice = 1;
            while(choice != 0)
            {
                choice = PromptForService();
                switch (choice)
	            {
                    case 0:
                        // Do nothing, exit.
                        break;
                    case 1:
                        FoodSearchExample();
                        break;
                    case 2:
                        FoodDetailsExample();
                        break;
                    case 3:
                        ExerciseCatalogExample();
                        break;
		            default:
                        Console.WriteLine("Unrecognized choice: " + choice);
                        break;
	            }
            }

            Console.WriteLine("");
            Console.WriteLine("Any key to quit...");
            Console.ReadKey();
        }

        private static void ExerciseCatalogExample()
        {
            if (exerCatalog == null)
                exerCatalog = new ExerciseCatalog(consumerKey, consumerSecret);

            var response = exerCatalog.GetResponseSynchronously(new ExerciseCatalogRequest());

            if (response.HasResult)
            {
                Console.WriteLine("Found " + response.exercises.exercise.Count + " exercises: ");
                var form = "id:{0} - {1}";
                foreach (var ex in response.exercises.exercise)
                {
                    Console.WriteLine(String.Format(form, ex.exercise_id, ex.exercise_name));
                }
            }
            else
                Console.WriteLine("Failed to get the exercise catalog.");
        }

        private static int PromptForService()
        {
            // Prompt text.
            Console.WriteLine(string.Empty);
            Console.WriteLine("Please Select a Service to Run (by number):");
            Console.WriteLine("===============================");
            Console.WriteLine("0. None / Quit");
            Console.WriteLine("1. Food Search");
            Console.WriteLine("2. Food Details");
            Console.WriteLine("3. Exercise Catalog");
            Console.WriteLine(string.Empty);
            Console.Write("Choice: ");
            
            // Get choice from input, try to parse it.
            var line = Console.ReadLine();
            int choice = 0;
            while (!Int32.TryParse(line, out choice))
            {
                Console.Write("Invalid choice, try again: ");
                line = Console.ReadLine();
            }

            return choice;
        }

        private static void FoodDetailsExample()
        {
            // Get the food id, call the service, show the response.

            Console.Write("Enter a food id: ");
            var foodId = Console.ReadLine();

            long parsedId = 0;
            while(!long.TryParse(foodId, out parsedId))
            {
                Console.WriteLine("Invalid food id, try again:");
                foodId = Console.ReadLine();
            }

            if (foodDetail == null)
                foodDetail = new FoodDetails(consumerKey, consumerSecret);

            var response = foodDetail
                            .GetResponseSynchronously(
                                new FoodDetailsRequest()
                                {
                                    FoodId = parsedId
                                });

            if (response.HasResponse)
            {
                Console.WriteLine("Found " + response.food.servings.serving.Count + " Results: ");
                
                foreach (var serv in response.food.servings.serving)
                {                  
                    // Use a little reflection to help show the results.
                    var props = serv.GetType().GetProperties();
                    foreach (var prop in props)
                    {
                        if (prop == null || !prop.CanRead)
                            continue;

                        Console.WriteLine("{0} : {1}", prop.Name, prop.GetValue(serv, null));
                    }
                }
                
            }
            else
                Console.WriteLine("No response for food id: " + foodId);
        }        

        private static void FoodSearchExample()
        {
            Console.Write("Enter a search term: ");
            var searchTerm = Console.ReadLine();

            if ( foodSearch == null )
                foodSearch = new FoodSearch(consumerKey, consumerSecret);

            var response = foodSearch.GetResponseSynchronously(new FoodSearchRequest()
            {
                SearchExpression = searchTerm
            });

            if (response.HasResults)
            {
                Console.WriteLine("Got " + response.foods.food.Count + " Results: \n\n");
                var form = "id: {0}, \n - type: {1}, \n - name: {2}, \n - description: {3}";
                foreach (var food in response.foods.food)
                {
                    Console.WriteLine(String.Format(form, food.food_id, food.food_type, food.food_name, food.food_description));
                }
            }
            else
                Console.WriteLine("No results from term: " + searchTerm);

            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}

