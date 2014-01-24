using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Core;
using System.Diagnostics;

namespace UIRssReader
{
    /// <summary>
    /// Logique d'interaction pour UCArticle.xaml
    /// </summary>
    public partial class UCArticle : UserControl
    {
        private String link;
        public UCArticle(Article art)
        {
            InitializeComponent();
            article.Header = art.Title;
            pubdate.Text = art.PubDate;
            description.Text = art.Description;
            link = art.Link;
        }

        private void goto_Click(object sender, RoutedEventArgs e)
        {
            Process p = new Process();
            p.StartInfo.FileName = link;
            p.Start();
        }
    }
}
