using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PrototypeTwitter
{
    class Program
    {
        static string userTxtLocation = "user.txt";
        static string tweetTxtLocation = "tweet.txt";

        static void Main(string[] args)
        {
            try
            {
                var twitterLikeClass = new TwitterLikeClass(userTxtLocation, tweetTxtLocation);

                var sortedUsers = twitterLikeClass.allUsers.OrderBy(x => x.UserName);

                Console.SetWindowSize(140, 20);

                foreach (var user in sortedUsers)
                {
                    Console.WriteLine(user.UserName);
                    foreach (var tweet in twitterLikeClass.allTweets)
                    {
                        if (user.Following.Contains(tweet.UserName))
                        {
                            Console.WriteLine("\t@{0}: {1}", tweet.UserName, tweet.Message);
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Exception caught: {0}", ex.Message);
            }           

            Console.ReadLine();            
        }        
    }
}
