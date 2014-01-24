using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Core
{
    public abstract class IRssReader
    {
        /// <summary>
        /// the root Document Node
        /// </summary>
        protected XDocument mXDoc = null;

        /// <summary>
        /// loads an xml file
        /// </summary>
        /// <param name="xml_file">xml file to load</param>
        void LoadXMLFile(string xml_file)
        {
            try
            {
                mXDoc = XDocument.Load(xml_file);
            }
            catch (Exception e){
                System.Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// Read a RssFlow.
        /// </summary>
        /// <param name="flowLink">link of rss flow.</param>
        /// <returns>articles of flow.</returns>
        public List<Article> ReadFlow(String flowLink)
        {
            List<Article> articles = null;
            try
            {
                LoadXMLFile(flowLink);

                articles = ReadFlow();
            }
            catch (SystemException e)
            {
                System.Console.WriteLine(e.ToString());

            }
            return articles;
        }

        /// <summary>
        /// Read a RssFlow.
        /// </summary>
        /// <returns>articles of flow.</returns>
        protected abstract List<Article> ReadFlow();

    }
}
