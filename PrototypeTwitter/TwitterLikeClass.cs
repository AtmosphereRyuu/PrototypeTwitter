using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace PrototypeTwitter
{
    class TwitterLikeClass
    {
        const string FOLLOWSTEXT = " follows ";
        public List<User> allUsers = new List<User>();
        public List<Tweet> allTweets = new List<Tweet>();

        public TwitterLikeClass(string userTextLocation, string tweetTextLocation)
        {
            CreateUsers(userTextLocation);
            CreateTweets(tweetTextLocation);
        }

        //Add all tweets from tweet.txt to allTweets
        private void CreateTweets(string tweetTextLocation)
        {
            foreach (string line in File.ReadLines(tweetTextLocation))
            {
                var indexOfGTSpace = line.IndexOf("> ");
                var userName = line.Substring(0, indexOfGTSpace);
                var message = line.Substring(indexOfGTSpace + 2, line.Length - (indexOfGTSpace + 2));
                var messageLength = message.Length;
                if (!(messageLength > 140))
                {
                    var tweet = new Tweet();
                    tweet.UserName = userName;
                    tweet.Message = message;
                    allTweets.Add(tweet);
                }                
            }
        }

        //Add all users from user.txt to allUsers
        private void CreateUsers(string userTextLocation)
        {
            foreach (string line in File.ReadLines(userTextLocation))
            {
                try
                {
                    //Find username of follower
                    var indexOfFirstSpace = line.IndexOf(" ");
                    var userNameOfFollower = line.Substring(0, indexOfFirstSpace);

                    //Check line contains FOLLOWSTEXT straight after username of follower
                    var followsToEndOfLineText = line.Substring(indexOfFirstSpace, line.Length - indexOfFirstSpace);
                    var followsText = followsToEndOfLineText.Substring(0, 9);
                    bool followsCorrectFormat = false;
                    if (followsText == FOLLOWSTEXT)
                    {
                        followsCorrectFormat = true;
                    }

                    if (followsCorrectFormat)
                    {
                        //Create string of who follower follows as a comma separated string
                        var usersFollowedText = followsToEndOfLineText.Substring(FOLLOWSTEXT.Length);
                        var usersFollowedTextNoSpaces = usersFollowedText.Replace(" ", "");

                        //List of people follower follows
                        List<string> usersFollowed = usersFollowedTextNoSpaces.Split(',').ToList();

                        //Create User or select User if already exists
                        var followersWithSameUserName = allUsers.Where(p => p.UserName == userNameOfFollower);
                        User user;
                        if (followersWithSameUserName.Any())
                        {
                            //Select user with same username
                            user = followersWithSameUserName.FirstOrDefault();
                        }
                        else
                        {
                            //Create new user with username
                            user = new User();
                            user.UserName = userNameOfFollower;
                            user.Following.Add(userNameOfFollower);
                            allUsers.Add(user);
                        }

                        //Create User that follower follows or select User follower follows and add to follower's list of Following
                        foreach (var username in usersFollowed)
                        {
                            var usersWIthSameUserName = allUsers.Where(p => p.UserName == username);
                            if (!usersWIthSameUserName.Any())
                            {
                                var newUser = new User();
                                newUser.UserName = username;
                                newUser.Following.Add(username);
                                allUsers.Add(newUser);
                            }

                            if (!user.Following.Contains(username))
                                user.Following.Add(username);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Exception caught: {0}", ex.Message);
                }

            }
        }

    }
}
