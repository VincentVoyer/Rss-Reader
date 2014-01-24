using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RssManager manager = new RssManager();

            manager.AddFlow("manga news", "rss mangaNews", "http://www.manga-news.com/index.php/feed/actujapon");
            manager.ReadFlow("manga news");
            Console.Write(manager.GetFlow("manga news"));
            Console.ReadLine();
        }
    }
}
