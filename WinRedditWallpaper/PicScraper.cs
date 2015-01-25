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
    public static class PicScraper
    {
        public static void scrape(string[] subreddits)
        {
            // Scrape links from wikipedia.org

            // 1.
            //
            Debug.WriteLine(subreddits.Length);
            for (int i = 0; i < subreddits.Length; i++)
            {
                WebClient w = new WebClient();
                string page = null;
                try
                {
                    page = w.DownloadString("http://reddit.com/r/" + subreddits[i]);
                    //Debug.WriteLine(page);
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
                        Debug.WriteLine(match.Value);
                    }
                    Debug.WriteLine(matches.Count);
                }
            }
        }
    }
}
