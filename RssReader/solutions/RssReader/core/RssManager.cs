using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    public class RssManager
    {
        /// <summary>
        /// The list of flows.
        /// </summary>
        public List<Flow> Flows
        {
            get
            {
                return mFlows.Values.ToList<Flow>();
            }
        }

        /// <summary>
        /// Flows.
        /// </summary>
        private Dictionary<String, Flow> mFlows;

        /// <summary>
        /// Rss Reader.
        /// </summary>
        private IRssReader reader;

        /// <summary>
        /// Default Constructor of RssManager.
        /// </summary>
        public RssManager()
          :this(new DefaultRssReader()) 
        { 
        }

        /// <summary>
        /// Complet Constructor of RssManager.
        /// </summary>
        /// <param name="rssReader">rss Reader of Manager.</param>
        public RssManager(IRssReader rssReader)
        {
            reader = rssReader;
            mFlows = new Dictionary<string, Flow>();
            ChargedFlow();
        }

        /// <summary>
        /// Add a new flow.
        /// </summary>
        /// <param name="name">name of flow.</param>
        /// <param name="description">description of flow.</param>
        /// <param name="link">link of flow.</param>
        public void AddFlow(String name, String description, String link)
        {
            if (!mFlows.ContainsKey(name))
            {
                Flow rss = new Flow(name, description, link);
                mFlows.Add(name, rss);
                SaveFlow(rss);
            }
        }

        /// <summary>
        /// Remove a flow.
        /// </summary>
        /// <param name="flowName">name of flow.</param>
        public void RemoveFlow(String flowName)
        {
            if (mFlows.ContainsKey(flowName))
            {
                mFlows.Remove(flowName);
                DataManager.Instance.Remove(flowName);
            }
        }

        /// <summary>
        /// Read a flow.
        /// </summary>
        /// <param name="flowName">name of flow.</param>
        public void ReadFlow(String flowName)
        {
            if (mFlows.ContainsKey(flowName))
            {
                String link = mFlows[flowName].Link;
                mFlows[flowName].Clear();
                mFlows[flowName].Add(reader.ReadFlow(link));
            }
        }

        /// <summary>
        /// list of flow's article.
        /// </summary>
        /// <param name="flowName">flow's name.</param>
        /// <returns>articles from flow</returns>
        public List<Article> ArticlesAt(String flowName)
        {
            if(mFlows.ContainsKey(flowName))
                return mFlows[flowName].Articles;
            return null;
        }

        /// <summary>
        /// Get the flow with flowName.
        /// </summary>
        /// <param name="flowName">Name of flow.</param>
        /// <returns>Flow with flowName.</returns>
        public Flow GetFlow(String flowName)
        {
            if (mFlows.ContainsKey(flowName))
                return mFlows[flowName];

            return null;
        }

        private void SaveFlow(Flow rss)
        {
            DataManager.Instance.Write(rss);
        }

        private void ChargedFlow()
        {
            foreach (Flow flow in DataManager.Instance.Read())
            {
                mFlows.Add(flow.Name, flow);
            }
        }
    }
}
