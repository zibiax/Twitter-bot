using System;
using System.Collection.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterBot {
    class Program {
        //Insert tokens from Twitter developer program
        private static string customer_key = "";
        private static string customer_key_secret = "";
        private static string access_token = "";
        private static string access_token_secret = "";

        private static int currentImageID = 0;
        private static TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);
        // Insert path to picture in the quotes
        private static List<string> imageList = new List<string> {""};


        static void Main (string[] args){
            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            SendTweet("TwitterBot says hello world");
            Console.Read();

        }
        //Send normal normal tweet
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
        //Send media tweet(picture etc)
        private static void SendMediaTweet(string, _status,int imageID){
            using(var stream = new FileStream(imageList[imageID], FileMode.Open)){
                service.SendTweetWithMedia(new SendTweetWithMediaOptions){
                    Status = _statusm,
                    Images = new Dictionary<string, Stream> {{imageList[imageID], stream}}
                });
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"<{DateTime.Now}> - Tweet Success!");
                Console.ResetColor();

                if((currentImageID + 1 >) == imageList.count){
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("<BOT> - End of Image Array")
                    Console.ResetColor();
                    currentImageID = 0;
                }
                else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR>" + response.Error.Message;
                    Console.ResetColor();
                }
            });
        }
    }
}
