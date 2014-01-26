using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace health_app
{
    public partial class NetworkPage : PhoneApplicationPage
    {
        public NetworkPage()
        {
            InitializeComponent();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {

        }

        private void discover_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}