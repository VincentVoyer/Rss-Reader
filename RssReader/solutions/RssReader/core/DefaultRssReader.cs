using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml;
using System.Text.RegularExpressions;

namespace Core
{
    public class DefaultRssReader : IRssReader
    {
       

        private String RemoveHTML(XElement descriptionNode)
        {
            StringBuilder sb = new StringBuilder();
            string desiredValue =
             Regex.Replace(descriptionNode.Value
                                      .Replace(Environment.NewLine, String.Empty)
                                      .Trim(),
                 @"\s+", " ");
            desiredValue = Regex.Replace(desiredValue, @"(<\/?\w+((\s+\w+(\s*=\s*(?:"".*?""|'.*?'|[^'"">\s]+))?)+\s*|\s*)\/?>)|()", "");
            
            desiredValue = Regex.Replace(desiredValue ,@"/&amp;/g", "&");
            desiredValue = Regex.Replace(desiredValue, @"/&nbsp;/g", " ");

            sb.Append(desiredValue);
            return sb.ToString();
        }

        protected override List<Article> ReadFlow()
        {
            List<Article> lesItems = new List<Article>();
            XElement root = mXDoc.Root;
            XElement channel = root.Element(XMLTags.CHANNEL);
            List<XElement> items = channel.Elements(XMLTags.ITEM).ToList();
            if (items != null)
            {
                foreach (XElement element in items)
                {
                    try
                    {
                        String title = element.Element(XMLTags.TITLE).Value;
                        String description = RemoveHTML(element.Element(XMLTags.DESCRIPTION));
                        String link = element.Element(XMLTags.LINK).Value;
                        String pubdate = element.Element(XMLTags.PUBDATE).Value.ToString();

                        lesItems.Add(new Article(title, description, link, pubdate));
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.ToString());
                    }
                }
            }
            return lesItems;
        }
    }
}
