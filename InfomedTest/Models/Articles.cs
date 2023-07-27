using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfomedTest.Models
{
    public class RootArticle
    {
        public int id { get; set; }
        public string authorName { get; set; }
        public string description { get; set; }
        public DateTime dateModified { get; set; }
        public string title { get; set; }
        public bool isActive { get; set; }
        public int views { get; set; }
        public List<string> specializationsIds { get; set; }
        public List<Sponsorship> sponsorships { get; set; }
        public int? intId { get; set; }
    }

    public class Sponsorship
    {
        public string sponsorshipName { get; set; }
        public int sponsorshipType { get; set; }
        public int sponsorshipId { get; set; }
        public string promotionText { get; set; }
    }
}