using System;
using System.Collection.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterBot {
    class Program {
        private static string customer_key = "";
        private static string customer_key_secret = "";
        private static string access_token = "";
        private static string access_token_secret = "";

        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);

        static void Main (string[] args){
            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            SendTweet("TwitterBot says hello world");
            Console.Read();

        }
        private static void SendTweet(string _status){
            service.SendTweet(new SendTweetOptions { Status = _status}, (tweet, response)) =>{
                if (response.StatusCode== HttpStatusCode.OK){
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Success!");
                    Console.ResetColor(); 
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR>" + response.Error.Message;
                    Console.ResetColor(); 
                }
            }
        }
    }
}