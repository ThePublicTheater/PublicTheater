using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using LinqToTwitter;

namespace PublicTheater.Web.Services
{
    /// <summary>
    /// Summary description for Twitter
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Twitter : System.Web.Services.WebService
    {

        string consumerKey = "L2LsjObOpa39miA25XTA";
        string consumerSecret = "TUnPKnBJJ7QsZMuLtu2a3tljtliSMJkx9jj0cmYFg";
        string defaultUserAccounts = "PublicTheaterNY,JoesPub";

        public class TweetResponse
        {
            public List<JsonStatus> statuses = new List<JsonStatus>();
        }

        public struct JsonStatus
        {
            public string Text;
            public string UserName;
            public string Handle;
            public DateTime CreatedAt;
            public Status RetweettedStatus;
        }

        [WebMethod(CacheDuration = 200)]
        public TweetResponse GetTweets()
        {
            return GetTweetsByUserNames(defaultUserAccounts);
        }

        [WebMethod(CacheDuration = 200)]
        public TweetResponse GetTweetsByUserNames(string usernames)
        {
            if (string.IsNullOrEmpty(usernames))
            {
                usernames = defaultUserAccounts;
            }

            var auth = new ApplicationOnlyAuthorizer
            {
                Credentials = new InMemoryCredentials
                {
                    ConsumerKey = consumerKey,
                    ConsumerSecret = consumerSecret
                }
            };

            auth.Authorize();

            var twitterCtx = new TwitterContext(auth);
            var response = new TweetResponse();
            var twUsers =
                (from twUser in twitterCtx.User
                 where twUser.Type == UserType.Lookup &&
                       twUser.ScreenName == usernames
                 select twUser)
                 .ToList();

            foreach (var user in twUsers)
            {
                try
                {
                    foreach (var status in (from twStatus in twitterCtx.Status
                                            where twStatus.Type == StatusType.User &&
                                            twStatus.ScreenName == user.Identifier.ScreenName &&
                                            twStatus.Count == 5 &&
                                            twStatus.ExcludeReplies == true
                                            select twStatus))
                    {
                        var jsonStatus = new JsonStatus();
                        
                        jsonStatus.Text = (status.Text);
                        jsonStatus.RetweettedStatus = status.RetweetedStatus;
                        jsonStatus.UserName = status.User.Name;
                        jsonStatus.Handle = status.User.Identifier.ScreenName;
                        jsonStatus.CreatedAt = status.CreatedAt;
                        response.statuses.Add(jsonStatus);
                    }
                    response.statuses.Sort(new Comparison<JsonStatus>((a, b) => { return b.CreatedAt.CompareTo(a.CreatedAt); }));

                }
                catch (Exception)
                {
                }


            }
            return response;
        }
    }
}
