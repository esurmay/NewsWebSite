using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace News.Models
{
    public class ItemRSS
    {
        public string Id;
        public string Title;
        public string Description;
        public string Summary;
        public string LastUpdatedTime;
        public string pubDate;
        public string Copyright;
        public string Link;
        public string Authors;
        public string UrlImage;
        public string Categories;
        public string Content;

        [XmlElement("encoded", Namespace = "http://purl.org/rss/1.0/modules/content/")]
        public string encoded { get; set; }

    }
}