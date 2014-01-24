using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core
{
    /// <summary>
    /// An article from Rss Flow.
    /// </summary>
    public class Article : IComparable<Article>, IEquatable<Article>
    {
        /// <summary>
        /// Title of article.
        /// </summary>
        public String Title
        {
            get{
                return mTitle;
            }
        }
        private readonly String mTitle;

        /// <summary>
        /// Description of article.
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
        /// Link of article.
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
        /// Publish Date of article.
        /// </summary>
        public String PubDate
        {
            get
            {
                return mPubDate;
            }
        }
        private readonly String mPubDate;

        /// <summary>
        /// Constructor of Article.
        /// </summary>
        /// <param name="title">title of article.</param>
        /// <param name="description">description of article.</param>
        /// <param name="link">link of article.</param>
        /// <param name="pubDate">publish date of article.</param>
        public Article(String title, String description, String link, String pubDate)
        {
            mTitle = title;
            mDescription = description;
            mLink = link;
            mPubDate = pubDate;
        }

        public int CompareTo(Article other)
        {
            return other.Link.CompareTo(Link);
        }

        public bool Equals(Article other)
        {
            return other.Link.Equals(Link);
        }

        public override int GetHashCode()
        {
            return Link.GetHashCode();
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("/****************************************************/")
                      .Append(Title).AppendLine(" :")
                      .AppendLine(PubDate)
                      .AppendLine(Link)
                      .AppendLine(Description);
            return result.ToString();
        }
    }
}
