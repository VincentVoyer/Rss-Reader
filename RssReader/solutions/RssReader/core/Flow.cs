using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    /// <summary>
    /// Rss Flow
    /// </summary>
    public class Flow : IComparable<Flow>, IEquatable<Flow>
    {
        /// <summary>
        /// Name of flow.
        /// </summary>
        public String Name
        {
            get
            {
                return mName;
            }
        }
        private readonly String mName;

        /// <summary>
        /// Description of flow.
        /// </summary>
        public String Description
        {
            get
            {
                return mDescription;
            }
        }
        private readonly String mDescription;

        /// <summary>
        /// Link of flow.
        /// </summary>
        public String Link
        {
            get
            {
                return mLink;
            }
        }
        private readonly String mLink;

        /// <summary>
        /// Get an article of flow.
        /// </summary>
        /// <param name="index">index of article</param>
        /// <returns>article at index.</returns>
        public Article this[int index]
        {
            get
            {
                if (index > -1 && index < mArticles.Count)
                    return mArticles.ElementAt(index);
                return null;
            }
        }
        public int NArticles
        {
            get
            {
                return mArticles.Count;
            }
        }
        public List<Article> Articles
        {
            get
            {
                return new List<Article>(mArticles);
            }
        }
        private List<Article> mArticles;

        /// <summary>
        /// Constructor of Flow.
        /// </summary>
        /// <param name="name">name of flow.</param>
        /// <param name="description">description of flow.</param>
        /// <param name="link">link of flow.</param>
        public Flow(String name, String description, String link)
        {
            mName = name;
            mDescription = description;
            mLink = link;
            mArticles = new List<Article>();
        }

        /// <summary>
        /// Add an article in the flow.
        /// </summary>
        /// <param name="article">an article.</param>
        public void Add(Article article)
        {
            mArticles.Add(article);
        }

        /// <summary>
        /// Add a collection of article.
        /// </summary>
        /// <param name="articles">collection of article.</param>
        public void Add(List<Article> articles)
        {
            if(articles != null)
                mArticles.AddRange(articles);
        }

        /// <summary>
        /// Remove all articles in the flow.
        /// </summary>
        public void Clear()
        {
            mArticles.Clear();
        }

        public int CompareTo(Flow obj)
        {
            return obj.Name.CompareTo(Name);
        }

        public bool Equals(Flow other)
        {
           return other.Name.Equals(Name);
        }

        /// <summary>
        /// returns a hash code in order to use this class in hash table
        /// </summary>
        /// <returns>hash code</returns>
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.Append(Name);
            //      .AppendLine(Link);
            //foreach (Article art in mArticles)
            //{
            //    result.AppendLine(art.ToString());
            //}

            return result.ToString();
        }
    }
}
