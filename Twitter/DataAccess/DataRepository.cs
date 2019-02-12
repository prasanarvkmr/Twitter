using EntityCore;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using Microsoft.Ajax.Utilities;
using Twitter.Models;
using Tweet = EntityCore.Tweet;

namespace Twitter.DataAccess
{
    public class DataRepository
    {
        public Twitter_CloneEntities DataObj { get; set; }

        public DataRepository()
        {
            DataObj = new Twitter_CloneEntities();
        }

        public Person IsUserValid(Login login)
        {
            return DataObj.People.FirstOrDefault(x => x.user_id == login.Username || x.email == login.Username && x.password == login.Password);
        }

        public Person RegisterPerson(Regsiter registerDetails)
        {
            var newPerson = new Person
            {
                user_id = registerDetails.Username,
                fullName = registerDetails.FullName,
                email = registerDetails.Email,
                password = registerDetails.Password,
                joined = DateTime.Now
            };
            DataObj.People.Add(newPerson);
            DataObj.SaveChanges();
            return newPerson;
        }

        public Person UpdateProfile(Person data)
        {
            var person = Get(data.user_id);
            person.fullName = data.fullName;
            DataObj.People.AddOrUpdate(person);
            DataObj.SaveChanges();
            return person;
        }

        public Person Get(string id)
        {
            return DataObj.People.FirstOrDefault(x => x.user_id == id);
        }

        public List<Tweet> Tweet(Tweet data)
        {
            DataObj.Tweets.AddOrUpdate(data);
            DataObj.SaveChanges();
            return GetTweets(data.user_id);
        }

        public List<Tweet> GetTweets(string user_id)
        {
            return DataObj.Tweets.Where(x => x.user_id == user_id).ToList();
        }

        public bool Follow(string user, string userToFollow)
        {
            var follow = new Following
            {
                user_id = user,
                following_id = userToFollow
            };
            DataObj.Followings.AddOrUpdate(follow);
            var result = DataObj.SaveChanges();
            return result == 1;
        }
    }
}