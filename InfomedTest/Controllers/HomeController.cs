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
                 .ThenByDescending(x => x.promotionLevel).Select(x => new Doctor {
                     ID = x.id,
                     FullName = x.fullName,
                     Rating = x.reviews.averageRating,
                     Languages = null,
                     isArticle = false,
                     PhoneNumber = x.phones[0].number
                 }).ToList();
                var list = FixPhones(items);
            }
            
          


            
            return View();
        }
       public List<Doctor> FixPhones(List<Doctor> list)
        {
            foreach (var item in list.Where(w => !w.PhoneNumber.Contains("-")))
            {
                if ((item.PhoneNumber.StartsWith("07"))|| (item.PhoneNumber.StartsWith("05")))
                {
                    item.PhoneNumber= item.PhoneNumber.Insert(3, "-");
                }
                else
                {
                    item.PhoneNumber = item.PhoneNumber.Insert(2, "-");
                }
            }

            return list;
        }
    }
}