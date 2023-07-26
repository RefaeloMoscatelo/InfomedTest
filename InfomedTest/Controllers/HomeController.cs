using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using InfomedTest.Models;
using Newtonsoft.Json.Linq;

namespace InfomedTest.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
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

        public ActionResult Doctors()
        {

            var path = Server.MapPath(@"~/Models/JsonData/doctors.json");

            using (StreamReader r = new StreamReader(path))
            {
                string Doctorsjson = r.ReadToEnd();
                dynamic items = JsonConvert.DeserializeObject<List<Root>>(Doctorsjson)
                  .Where(x => x.isActive == true)
                  .Where(x => x.promotionLevel <= 5)
                  .OrderBy(x => x.reviews.averageRating)
                 .ThenByDescending(x => x.reviews.totalRatings)
                 .ThenByDescending(x => x.promotionLevel).ToList();
                items = FixPhones(items);
                return View(items);
            }

           
        }
       public List<Root> FixPhones(List<Root> list)
        {
            List<Root> FixedList = new List<Root>();
            for (int i = 0; i < list.Count; i++)
            {
                if (!list[i].phones[0].number.Contains("-"))
                {
                    if ((list[i].phones[0].number.ToString().StartsWith("07"))|| (list[i].phones[0].number.ToString().StartsWith("05")))
                    {
                        list[i].phones[0].number = list[i].phones[0].number.Insert(3, "-");
                    }
                }
            }
            return FixedList;
        }
    }
}