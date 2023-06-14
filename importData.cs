using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace matrix
{
    public class importData
    {

        private string baseUrl = "https://jsonplaceholder.typicode.com/";

        ReadOnlyDictionary<string, string> resources = new ReadOnlyDictionary<string, string>(
      new Dictionary<string, string>
      {
                { "todos","todos"},
                { "comments", "comments" },
                { "albums", "albums" },
                { "photos", "photos" },
                { "users", "users" }
      });

        public async Task<string> GetJsonFromUrl(string res, string id)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                string result = await httpClient.GetStringAsync($"{baseUrl}{res}/{id}");
                if (result == null)
                {
                    throw new Exception("Failed to deserialize JSON to the specified type.");
                }
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
                throw;
            }
        }
        public async Task<List<string>> GetListNamesOfUsers()
        {
            string jsonString = await GetJsonFromUrl("users", "");
            List<string> usernames = new List<string>();

            try
            {
                JArray userArray = JArray.Parse(jsonString);

                foreach (JObject userObject in userArray)
                {
                    string name = userObject.Value<string>("name");
                    Console.WriteLine($"{name}");
                    usernames.Add(name);
                }
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"שגיאה בקריאת JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
            }

            return usernames;
        }

        public async Task<List<string>> GetListTodosByUserId(string userId)
        {
            string jsonString = await GetJsonFromUrl("todos", $"?userId={userId}");
            List<string> tasks = new List<string>();

            try
            {
                JArray userArray = JArray.Parse(jsonString);

                foreach (JObject userObject in userArray)
                {
                    string task = userObject.Value<string>("title");
                    Console.WriteLine($"{task}");
                    tasks.Add(task);
                }
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"שגיאה בקריאת JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
            }

            return tasks;
        }

        public async Task <Dictionary<string, object>> getAllDetailsByUserId(string userId)
        {

            string jsonString = await GetJsonFromUrl("users", userId);
            JObject userObject = JObject.Parse(jsonString);

            // יצירת מילון מהפרטים
            var userDictionary = userObject.ToObject<Dictionary<string, object>>();

            // הדפסת המילון
            foreach (var kvp in userDictionary)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            return userDictionary;

        }

        public async Task<Dictionary<string, object>> getAllPostsByUserId(string userId)
        {

            string jsonString = await GetJsonFromUrl("posts", userId);
            JObject userObject = JObject.Parse(jsonString);

            // יצירת מילון מהפרטים
            var userDictionary = userObject.ToObject<Dictionary<string, object>>();

            // הדפסת המילון
            foreach (var kvp in userDictionary)
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value}");
            }
            return userDictionary;

        }

        public async Task<List<string>> GetListPostsByUserId(string postId)
        {
            string jsonString = await GetJsonFromUrl("comments", $"?postId={postId}");
            List<string> tasks = new List<string>();

            try
            {
                JArray postArry = JArray.Parse(jsonString);

                foreach (JObject userObject in postArry)
                {
                    string name = userObject.Value<string>("name");
                    string body = userObject.Value<string>("body");

                    Console.WriteLine($"{name} : {body}");
                }
            }
            catch (JsonReaderException ex)
            {
                Console.WriteLine($"שגיאה בקריאת JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"שגיאה: {ex.Message}");
            }

            return tasks;
        }
    }

    
}
