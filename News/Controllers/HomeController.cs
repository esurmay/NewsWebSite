using News.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace News.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            List<ItemRSS> results = null;
            IEnumerable<Post> feeds = null;
            //List<ItemRSS> items = new List<ItemRSS>(); 
            //List<SyndicationFeed> itemslist = new List<SyndicationFeed>();
            XNamespace media = XNamespace.Get("http://search.yahoo.com/mrss/");



             string[] listRss = new string[] { "http://www.avisen.dk/Handlers/GetFeed.ashx?type=MestLaeste" };
            for (int i = 0; i < listRss.Length; i++)
            {


                XNamespace ns = "http://purl.org/rss/1.0/modules/content/";
                XNamespace nsAtom = "http://www.w3.org/2005/Atom/";
                XDocument xdoc = XDocument.Load(listRss[i]);


                feeds = from post in xdoc.Root.Descendants("item")
                        select new Post(post);

 
            }
            return View(feeds.ToList());
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
    }
}