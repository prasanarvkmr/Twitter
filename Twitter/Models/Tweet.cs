using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Twitter.Models
{
    public class Tweet
    {
        public int TweetId { get; set; }

        public User Person { get; set; }

        [DisplayName(displayName:"Message")]
        public string Message { get; set; }

        public DateTime? CreatedDate { get; set; }

        public string UserId { get; set; }
    }
}