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

namespace UIRssReader
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private RssManager manager;

        private String currentSelect;

        public MainWindow()
        {
            InitializeComponent();
            manager = new RssManager();
            GetFlows();
            
        }

        private void GetFlows()
        {
            flows.ItemsSource = manager.Flows.OrderBy(flow => flow.Name).ToList();
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            Window add = new AddFlow(manager);
            add.ShowDialog();
            GetFlows();
        }

        private void delete_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelect != null)
            {
                String remove = currentSelect;
                flows.UnselectAll();
                manager.RemoveFlow(remove);
                GetFlows();
            }
        }

        private void read_Click(object sender, RoutedEventArgs e)
        {
            if (currentSelect != null)
            {
                Flow current = null;
                manager.ReadFlow(currentSelect);
                current = manager.GetFlow(currentSelect);
                if(current != null)
                    ChangedArticles(current);
            }
        }

        private void ChangedArticles(Flow current)
        {
            if(articles.Children.Count >0)
                articles.Children.Clear();
            List<Article> lesArticles = current.Articles;
            if (lesArticles != null)
            {
                foreach (Article art in lesArticles)
                {
                    articles.Children.Add(new UCArticle(art));
                }
            }

        }

        private void flows_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (flows.SelectedItem != null)
            {
                currentSelect = flows.SelectedItem.ToString();
                descFlux.Text = manager.GetFlow(currentSelect).Description;
            }
        }
    }
}
