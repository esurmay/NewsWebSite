using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace News.Models
{
    public class Post
    {
        public string Title { get; private set; }
        public DateTime? pubDate { get; private set; }
        public string Link { get; private set; }
        public string Description { get; private set; }
        public string Creator { get; private set; }
        public string Content { get; private set; }
        public string imageUrl { get; private set; }
        public string Source { get; private set; }

        private static string getvalue(XContainer element, string name)
        {
            if ((element == null) || (element.Element(name) == null))
                return String.Empty;
            return element.Element(name).Value;
        }

        public Post(XContainer post)
        {
            // Get the string properties from the post's element values
            Title = getvalue(post, "title");
            Link = getvalue(post, "guid");
            Description = getvalue(post, "description");
            Creator = getvalue(post, "{http://purl.org/dc/elements/1.1/}creator");

            if (post.Elements("{http://purl.org/rss/1.0/modules/content/}encoded").Any())
                Content = post.Elements("{http://purl.org/rss/1.0/modules/content/}encoded").First().Value;
            else if (post.Elements("enclosure").Any())
                if (post.Elements("enclosure").First().Attribute("url") != null)
                    imageUrl = post.Elements("enclosure").First().Attribute("url").Value;

            if (!String.IsNullOrEmpty(Content))
            {
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Content);
                var imgLinks = doc.DocumentNode
                             .Descendants("img")
                             .Select(n => n.Attributes["src"].Value)
                             .ToArray();
                if (imgLinks.Length > 0)
                {
                    imageUrl = imgLinks[0].ToString();
                }
            }
            /*//Content = getvalue(post, "{http://purl.org/rss/1.0/modules/content/}encoded");


            if (post.Elements("{http://purl.org/rss/1.0/modules/content/}encoded").Any())
            {

                Content = post.Elements("{http://purl.org/rss/1.0/modules/content/}encoded").First().Value;
                HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
                doc.LoadHtml(Content);
                var imgLinks = doc.DocumentNode
                             .Descendants("img")
                             .Select(n => n.Attributes["src"].Value)
                             .ToArray();

                if (imgLinks.Length > 0)
                {
                    imageUrl = imgLinks[0].ToString();
                }
            }*/

            DateTime result;
            if (DateTime.TryParse(getvalue(post, "pubDate"), out result))
                pubDate = (DateTime?)result;
        }

        public override string ToString()
        {
            return String.Format("{0} by {1}", Title ?? "no title", Creator ?? "Unknown");
        }
    }
}
