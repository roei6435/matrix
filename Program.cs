using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace matrix
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            importData impda = new importData();
            Console.WriteLine("application:");
            Console.WriteLine("task board:");
            Console.WriteLine("List all users:");
            var listNamesInSystem = await impda.GetListNamesOfUsers();
            Console.WriteLine("----------------------------------\n ");
            Console.WriteLine("Enter your USER ID to receive the tasks for you: ");
            string userId = Console.ReadLine();
            var tasksForThisUser = await impda.GetListTodosByUserId(userId);
            Console.WriteLine("you want read more about this user? enter yes/no");
            string input = Console.ReadLine();
            bool readMore = input == "yes" ? true : false;
            if (readMore)
            {
                var details = await impda.getAllDetailsByUserId(userId);
            }
            Console.WriteLine("---------------------------------------------\nBlog screen:");
            Console.WriteLine("all posts:");
            Console.WriteLine("Enter post id ");
            string postId = Console.ReadLine();
            var listOfPosts = await impda.GetListPostsByUserId(postId);
            Console.ReadLine();
        }
    }
}
