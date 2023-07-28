using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Newtonsoft.Json;
using InfomedTest.Models;
using Newtonsoft.Json.Linq;
using System.Web.Script.Serialization;
using System.Text;

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
                 .ThenByDescending(x => x.promotionLevel).Select(x => new Doctor
                 {
                     ID = x.id,
                     FullName = x.fullName,
                     Rating = x.reviews.averageRating,
                     Languages = x.languageIds,
                     isArticle = false,
                     PhoneNumber = x.phones[0].number
                 }).ToList();
                var list = FixPhones(items);
                list = CheckIfArticle(list);
                list = Languages(list);
                return View(list);
            }
            
        }

      
        public List<Doctor> Languages(List<Doctor> list)
        {
            List<string> listToReturn = new List<string>();

            var path = Server.MapPath(@"~/Models/JsonData/language.json");
            using (StreamReader r = new StreamReader(path))
            {
                string languages = r.ReadToEnd();
                var jss = new JavaScriptSerializer();
                var table = jss.Deserialize<dynamic>(languages);
                Dictionary<string, dynamic> dictio = new Dictionary<string, dynamic>();
                
                foreach (var item in table)
                {
                    dictio = item.Value;
                }
                foreach (var item in list)
                {
                    for (int i=0;i < item.Languages.Count;i++)
                    {
                        foreach (var dictioItem in dictio)
                        {
                            if (item.Languages[i] == dictioItem.Key)
                            {
                                item.Languages[i] = dictioItem.Value;
                                break;
                            }
                        }
                    }
                    
                }

            }
            return list;
    }



        private List<Doctor> FixPhones(List<Doctor> list)
        {
            foreach (var item in list.Where(w => !w.PhoneNumber.Contains("-")))
            {
                if ((item.PhoneNumber.StartsWith("07")) || (item.PhoneNumber.StartsWith("05")))
                {
                    item.PhoneNumber = item.PhoneNumber.Insert(3, "-");
                }
                else
                {
                    item.PhoneNumber = item.PhoneNumber.Insert(2, "-");
                }
            }

            return list;
        }

        private List<Doctor> CheckIfArticle(List<Doctor> list)
        {
            var path = Server.MapPath(@"~/Models/JsonData/articles.json");

            using (StreamReader r = new StreamReader(path))
            {
                string articles = r.ReadToEnd();
                dynamic articlesItems = JsonConvert.DeserializeObject<List<RootArticle>>(articles);
                foreach (var doctor in list)
                {
                    foreach (var article in articlesItems)
                    {
                        if (article.sponsorships.Count == 0)
                        {
                            continue;
                        }
                        else if(doctor.ID== article.sponsorships[0].sponsorshipId)
                        {
                            doctor.isArticle = true;
                            break;
                        }
                    }
                }
                
            }
            return list;
        }
     
      [HttpPost]
        public void SaveContact(string drid,string Name, string Tel, string Email)
        {
            var path = Server.MapPath(@"~/Models/JsonData/details.txt");

            using (var fs = new FileStream(path, FileMode.Append))
            using (var sw = new StreamWriter(fs))
            {
                sw.WriteLine(string.Format("{0} ask to contact Dr {1} and his phone is {2} and Email {3}", Name, drid, Tel, Email));
            }



        }
    }
}