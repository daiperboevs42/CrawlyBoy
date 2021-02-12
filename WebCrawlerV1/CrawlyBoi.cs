using System;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;

namespace WebCrawlerV1
{
    public class CrawlyBoi
    {
        private Queue<Uri> frontier;
        private Dictionary<string, bool> visitedUrls;

        //public Dictionary<Uri, bool> allURLs;
        public List<string> allURLs;
        public void DoThingy()
        {
            Console.WriteLine("Please Write URL You Wish To Crawl");
            UriBuilder ub = new UriBuilder(Console.ReadLine());
            WebClient wc = new WebClient();
            string webPage = wc.DownloadString(ub.Uri.ToString());
            var hrefPattern = new Regex("href\\s*=\\s*(?:\"(?<1>[^\"]*)\"|(?<1>\\S+))", RegexOptions.IgnoreCase);

            var urls = hrefPattern.Matches(webPage);
            foreach (Match url in urls)
            {
                string newUrl = hrefPattern.Match(url.Value).Groups[1].Value;
                Console.WriteLine(newUrl);
                Uri myUri = new Uri(newUrl);

                if(BasedAfURL(myUri, newUrl) != null)
                {
                    frontier.Enqueue(myUri);
                    visitedUrls.Add(newUrl, true);
                }
                else
                {
                    visitedUrls.Add(newUrl, false);
                }
                
            }
        } 
        
        private Uri BasedAfURL(Uri baseUrl, string newUrl)
        {
            newUrl = newUrl.ToLower();
            if (Uri.TryCreate(newUrl, UriKind.RelativeOrAbsolute, out var url))
            {
                return (Uri.TryCreate(baseUrl, url, out Uri absoluteUrl) ?
               absoluteUrl : null);
            }
            return null;
        }
        
        private void DoWhatITellYouToDo(List<string> allURLs)
        {
            foreach (var url in allURLs)
            {
                try
                {
                    WebRequest request = WebRequest.Create(url);
                    request.Method = "GET";
                    var meme = request.GetResponse();
                    Console.WriteLine(meme); 
                }
                catch
                {

                }
            }
        }
    }
}
