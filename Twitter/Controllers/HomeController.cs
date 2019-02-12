using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntityCore;
using Twitter.DataAccess;
using Twitter.Models;
using Tweet = EntityCore.Tweet;

namespace Twitter.Controllers
{
    public class HomeController : Controller
    {
        public DataRepository DataAccess { get; set; }

        public HomeController()
        {
            if (DataAccess == null)
            {
                DataAccess = new DataRepository();
            }
        }

        public ActionResult Index()
        {
            var user = this.Session["User"] as Person;
            if (user?.fullName != null)
            {
                var person = DataAccess.Get(user.user_id);
                var tweets = MapTweets(person.Tweets.ToList());
                ViewBag.Following = person.Followings.Count;
                ViewBag.Followers = person.Followings1.Count;
                return View(tweets);
            }

            return RedirectToAction("Login", "Login");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Tweet()
        {
            return View("Index");
        }

        public ActionResult ViewProfile()
        {
            var person = (Person)Session["User"];
            return View("Profile", person);
        }

        public ActionResult EditProfile(string user_id)
        {
            var person = (Person)Session["User"];
            return View("EditProfile", person);
        }

        [HttpPost]
        public ActionResult EditProfile(Person data)
        {
            var update = DataAccess.UpdateProfile(data);
            Session["User"] = update;
            return View("Profile", update);
        }

        [HttpPost]
        public ActionResult Tweet(string data)
        {
            var user = (Person) Session["User"];
            var tweet = new Tweet
            {
                message = data,
                user_id = user.user_id,
                created = DateTime.Now
            };
            var tweets = DataAccess.Tweet(tweet);
            
            return View("_tweets", MapTweets(tweets));
        }

        private List<Models.Tweet> MapTweets(List<Tweet> tweets)
        {
            var tweetModel = new List<Models.Tweet>();
            if (tweets != null)
            {
                tweets.ForEach(x =>
                {
                    tweetModel.Add(new Models.Tweet
                    {
                        UserId = x.user_id,
                        CreatedDate = x.created,
                        Message = x.message
                    });
                });
            }

            return tweetModel;
        }

        [HttpPost]
        public ActionResult LoadUsers(string data)
        {
            var person = DataAccess.Get(data);
            return View("Follow", person);
        }

        [HttpPost]
        public ActionResult Follow(string userId)
        {
            var person = (Person)Session["User"];
            var isFollowed = DataAccess.Follow(person.user_id, userId);
            return RedirectToAction("Index");
        }
    }
}