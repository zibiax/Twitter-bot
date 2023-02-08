using System;
using System.Collection.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading.Tasks;
using TweetSharp;

namespace TwitterBot {
    class Program {
        private readonly static string customer_key = "";
        private readonly static string customer_key_secret = "";
        private readonly static string access_token = "";
        private readonly static string access_token_secret = "";

        private static int currentImageID = 0;
        private static readonly TwitterService service = new TwitterService(customer_key, customer_key_secret, access_token, access_token_secret);
        private static readonly List<string> imageList = new List<string> {""};

        static void Main(string[] args) {
            Console.WriteLine($"<{DateTime.Now}> - Bot Started");
            SendTweet("TwitterBot says hello world");
            Console.Read();
        }

        private static void SendTweet(string status) {
            service.SendTweet(new SendTweetOptions { Status = status }, (tweet, response) => {
                if (response.StatusCode == HttpStatusCode.OK) {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"<{DateTime.Now}> - Tweet Success!");
                    Console.ResetColor();
                } else {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"<ERROR> {response.Error.Message}");
                    Console.ResetColor();
                }
            });
        }

        private static void SendMediaTweet(string status, int imageID) {
            using (var stream = new FileStream(imageList[imageID], FileMode.Open)) {
                service.SendTweetWithMedia(new SendTweetWithMediaOptions {
                    Status = status,
                    Images = new Dictionary<string, Stream> {{imageList[imageID], stream}}
                }, (tweet, response) => {
                    if (response.StatusCode == HttpStatusCode.OK) {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"<{DateTime.Now}> - Tweet Success!");
                        Console.ResetColor();

                        if (currentImageID + 1 == imageList.Count) {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("<BOT> - End of Image Array");
                            Console.ResetColor();
                            currentImageID = 0;
                        } else {
                            currentImageID++;
                        }
                    } else {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"<ERROR> {response.Error.Message}");
                        Console.ResetColor();
                    }
                });
            }
        }
    }
}