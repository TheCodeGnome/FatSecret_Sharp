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

        private static string searchUserKey = string.Empty;
        private static string searchUserSecret = string.Empty;

        private static FoodSearch foodSearch;
        private static FoodDetails foodDetail;
        
        private static ExerciseCatalog exerCatalog;

        private static ProfileCreate profileCreate;
        private static ProfileGet profileGet;
        private static ProfileGetAuthInfo profileAuthInfo;

        private static WeightUpdate weightUpdate;
        private static WeightGetMonth weightMonth;

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
                    case 4:
                        CreateProfileExample();
                        break;
                    case 5:
                        ProfileDetailsExample();
                        break;
                    case 6:
                        ProfileAuthExample();
                        break;
                    case 7:
                        WeightUpdateExample();
                        break;
                    case 8:
                        WeightMonthExample();
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

        private static void WeightMonthExample()
        {
            var userId = "";
            var userSecret = "";
            var choice = AskFor<int>("Enter 1 to enter custom profile info, or 0 to use the last searched for info: ");
            if (choice == 1)
            {
                userId = AskFor<string>("Enter a User Token to look for: ");
                userSecret = AskFor<string>("Enter a User Secret to look for: ");
            }
            else
            {
                userId = searchUserKey;
                userSecret = searchUserSecret;
            }

            if (weightMonth == null)
                weightMonth = new WeightGetMonth(consumerKey, consumerSecret);

            var response = weightMonth.GetResponseSynchronously(new WeightGetMonthRequest()
                {
                    UserToken = userId,
                    UserSecret = userSecret
                });

            if (response != null && response.month != null && response.month.day != null)
            {
                Console.WriteLine("Found " + response.month.day.Count + " entries this month.");
                var form = "Date: {0:D}\nWeight: {1:#.## lbs}\nComment: {2}";
                foreach (var day in response.month.day)
                {
                    Console.WriteLine(String.Format(form, day.Date, day.WeightPounds, day.weight_comment));
                }
            }
            else
                Console.WriteLine("Problem getting weights for this month.");
        }

        private static void WeightUpdateExample()
        {
            string userId = "";
            string userSecret = "";

            var choice = AskFor<int>("Enter 1 to enter custom profile info, or 0 to use the last searched for info: ");
            if (choice == 1)
            {
                userId = AskFor<string>("Enter a User Token to look for: ");
                userSecret = AskFor<string>("Enter a User Secret to look for: ");
            }
            else
            {
                userId = searchUserKey;
                userSecret = searchUserSecret;
            }

            if (weightUpdate == null)
                weightUpdate = new WeightUpdate(consumerKey, consumerSecret);

            var weight = AskFor<double>("Weight (pounds): ");
            var height = AskFor<double>("Height (feet) [5'11\" = 5.9167]: ");
            var goal = AskFor<double>("Goal Weight (pounds): ");
            var comment = AskFor<string>("Comment: ");

            var response = weightUpdate.GetResponseSynchronously(new WeightUpdateRequest()
            {
                UserToken = userId,
                UserSecret = userSecret,
                CurrentWeight_KG = weight * 0.45359237,
                CurrentHeight_CM = height * 30.48,
                GoalWeight_KG = goal * 0.45359237,
                Comment = comment,
                UpdateDate = DateTime.UtcNow.AddDays(1),
            });

            if (response != null && response.IsSuccess)
                Console.WriteLine("Successfully reported weight.");
            else
                Console.WriteLine("Problem reporting weight.");
        }

        private static void ProfileAuthExample()
        {
            var userId = AskFor<string>("Enter UserId to get AuthInfo for: ");
            if (profileAuthInfo == null)
                profileAuthInfo = new ProfileGetAuthInfo(consumerKey, consumerSecret);

            var response = profileAuthInfo.GetResponseSynchronously(new ProfileGetAuthRequest()
            {
                UserId = userId
            });

            if (response != null && response.profile != null)
            {
                Console.WriteLine("Got AuthInfo for user:");
                Console.WriteLine("Token: " + response.profile.auth_token);
                Console.WriteLine("Secret: " + response.profile.auth_secret);

                searchUserKey = response.profile.auth_token;
                searchUserSecret = response.profile.auth_secret;
            }
            else
                Console.WriteLine("Problem getting auth info.");

            
        }

        private static void ProfileDetailsExample()
        {
            string userId = "";
            string userSecret = "";

            var choice = AskFor<int>("Enter 1 to enter custom profile info, or 0 to use the last searched for info: ");
            if (choice == 1)
            {
                userId = AskFor<string>("Enter a User Token to look for: ");
                userSecret = AskFor<string>("Enter a User Secret to look for: ");
            }
            else
            {
                userId = searchUserKey;
                userSecret = searchUserSecret;
            }

            if (profileGet == null)
                profileGet = new ProfileGet(consumerKey, consumerSecret);

            var response = profileGet.GetResponseSynchronously(new ProfileGetRequest()
            {
                UserToken = userId,
                UserSecret = userSecret
            });

            if (response != null && response.profile != null)
            {
                Console.WriteLine("Got Profile Details: ");
                ShowPropertyValues(response.profile);
            }
            else
                Console.WriteLine("Problem getting profile.");
        }

        private static void CreateProfileExample()
        {
            var userId = AskFor<string>("Enter a User Id for the new profile: ");
            
            if (profileCreate == null)
                profileCreate = new ProfileCreate(consumerKey, consumerSecret);

            var response = profileCreate.GetResponseSynchronously(new ProfileCreateRequest()
            {
                UserId = userId
            });

            if (response != null && response.profile != null)
            {
                Console.WriteLine("Profile Created Successfully: ");
                Console.WriteLine("Token: " + response.profile.auth_token);
                Console.WriteLine("Secret: " + response.profile.auth_secret);
            }
            else
                Console.WriteLine("Problem creating profile.");

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
            Console.WriteLine("4. Profile Create");
            Console.WriteLine("5. Profile Details");
            Console.WriteLine("6. Profile AuthInfo");
            Console.WriteLine("7. Weight Update");
            Console.WriteLine("8. Weight Updates this month");
            Console.WriteLine(string.Empty);

            int choice = AskFor<int>("Choice: ");

            return choice;
        }

        private static void FoodDetailsExample()
        {
            // Get the food id, call the service, show the response.

            var foodId = AskFor<int>("Enter a food id: ");            

            if (foodDetail == null)
                foodDetail = new FoodDetails(consumerKey, consumerSecret);

            var response = foodDetail
                            .GetResponseSynchronously(
                                new FoodDetailsRequest()
                                {
                                    FoodId = foodId
                                });

            if (response.HasResponse)
            {
                Console.WriteLine("Found " + response.food.servings.serving.Count + " Results: ");
                
                foreach (var serv in response.food.servings.serving)
                {                  
                    // Use a little reflection to help show the results.
                    ShowPropertyValues(serv);
                }
                
            }
            else
                Console.WriteLine("No response for food id: " + foodId);
        }

        private static void ShowPropertyValues(object serv)
        {
            if (serv == null)
                return;

            var props = serv.GetType().GetProperties();
            foreach (var prop in props)
            {
                if (prop == null || !prop.CanRead)
                    continue;

                Console.WriteLine("{0} : {1}", prop.Name, prop.GetValue(serv, null));
            }
        }        

        private static void FoodSearchExample()
        {
            var searchTerm = AskFor<string>("Enter a search term: ");

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

        static Dictionary<Type, Func<string, object>> converters = new Dictionary<Type, Func<string, object>>()
        {
            { typeof(int), new Func<string, object>((s) => Convert.ToInt32(s)) },
            { typeof(double), new Func<string, object>((s) => Convert.ToDouble(s)) },
            { typeof(string), new Func<string, object>((s) => s) },
        };

        private static TResponse AskFor<TResponse>(string prompt)
        {
            Console.Write(prompt);

            if (!converters.ContainsKey(typeof(TResponse)))
                throw new ArgumentException("TResponse", "Argument must be int, string, or double");

            var cvtr = converters[typeof(TResponse)];

            // Get line from input, try to parse it.
            var line = Console.ReadLine();
            TResponse choice = default(TResponse);

            bool convertSuccess = true;
            try
            {
                choice = (TResponse)cvtr(line);
            }
            catch
            {
                convertSuccess = false;
            }

            while (!convertSuccess)
            {
                Console.Write("Invalid choice, try again: ");
                line = Console.ReadLine();

                try
                {
                    choice = (TResponse)cvtr(line);
                    convertSuccess = true;
                }
                catch
                {
                    convertSuccess = false;
                }
            }

            return choice;
        }
    }
}

