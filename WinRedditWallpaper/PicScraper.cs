using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Diagnostics;
using System.Net;
using System.Text.RegularExpressions;

namespace WinRedditWallpaper
{
    public class PicScraper
    {
        public string[] subreddits;
        public string path;
        public PicScraper(string[] subreddits, string path)
        {
            this.subreddits = subreddits;
            this.path = path;
        }
        public PicScraper()
        {
            this.subreddits = null;
            this.path = null; 
        }
        public void scrape()
        {
            // Scrape links from wikipedia.org

            // 1.
            //
            if (subreddits == null || path == null)
            {
                return;
            }
            Debug.WriteLine(subreddits.Length);
            List<string> urls = new List<string>();
            WebClient webClient = new WebClient();
            for (int i = 0; i < subreddits.Length; i++)
            {
                string page = null;
                try
                {
                    page = webClient.DownloadString("http://reddit.com/r/" + subreddits[i]);
                    //positive lookbehind (?<=text)
                    //positive lookahead q(?=u)
                }
                catch (System.Net.WebException e)
                {
                    Debug.WriteLine(e.Message);
                }

                if (page != null)
                {
                    string pat = @"(?<=<a class=""title may-blank "" href="")(.{0,200}\.[a-z]{3}?)(?="" tabindex=""1"" >)";

                    //this will help http://msdn.microsoft.com/en-us/library/ez801hhe%28v=vs.110%29.aspx

                    MatchCollection matches = Regex.Matches(page, pat);
                    foreach (Match match in matches)
                    {
                        //Debug.WriteLine(match.Value);
                        urls.Add(match.Value);
                    }
                    //Debug.WriteLine(matches.Count);
                }
            }
            //now download all of the pictures
            Debug.WriteLine(path);
            for (int i = 0; i < urls.Count; i++)
            {
                try
                {
                    string fileName = path + "\\" + (i.ToString()) + ".png";
                    webClient.DownloadFile(new Uri(urls.ElementAt<string>(i)), fileName);
                }
                catch (System.Net.WebException e)
                {
                    Debug.WriteLine(e.Message);
                }
            }
        }
    }
}
