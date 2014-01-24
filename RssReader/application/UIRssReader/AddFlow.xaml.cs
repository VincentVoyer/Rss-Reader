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
using System.Windows.Shapes;
using Core;
using System.Net;
using System.Net.Sockets;

namespace UIRssReader
{
    /// <summary>
    /// Logique d'interaction pour AddFlow.xaml
    /// </summary>
    public partial class AddFlow : Window
    {
        RssManager manager;
        public AddFlow(RssManager manager)
        {
            InitializeComponent();
            this.manager = manager;
        }

        private void ok_Click(object sender, RoutedEventArgs e)
        {
            String nom = name.Text.Trim();
            String desc = description.Text.Trim();
            String lien = link.Text.Trim();

            if (nom != null && desc != null && lien != null)
            {
               
                    manager.AddFlow(nom, desc, lien);
                    this.Close();
                
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
