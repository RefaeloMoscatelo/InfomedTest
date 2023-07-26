using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfomedTest.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
    public class Address
    {
        public string cityName { get; set; }
        public string street { get; set; }
        public string cityId { get; set; }
    }

    public class Clinic
    {
        public int id { get; set; }
        public Address address { get; set; }
        public DateTime dateCreated { get; set; }
        public string name { get; set; }
        public List<Phone> phones { get; set; }
        public bool isActive { get; set; }
    }

    public class Phone
    {
        public string number { get; set; }
        public int phoneType { get; set; }
    }

    public class Reviews
    {
        public int professionalismRate { get; set; }
        public int averageRating { get; set; }
        public int totalRatings { get; set; }
        public int waitingTimeRate { get; set; }
        public int serviceRate { get; set; }
    }

    public class Root
    {
        public int id { get; set; }
        public int promotionLevel { get; set; }
        public List<Clinic> clinics { get; set; }
        public Reviews reviews { get; set; }
        public List<string> mainSpecialtiesIds { get; set; }
        public List<Phone> phones { get; set; }
        public List<string> subSpecialtiesIds { get; set; }
        public List<string> languageIds { get; set; }
        public string fullName { get; set; }
        public List<string> areaIds { get; set; }
        public bool isActive { get; set; }
        public List<string> mainSpecializationsIds { get; set; }
        public List<string> subSpecializationsIds { get; set; }
    }


}