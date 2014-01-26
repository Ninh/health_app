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
    public partial class ProfilePage : PhoneApplicationPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            //if (this.NavigationService.CanGoBack)
            //{
                string sex = string.Empty;
                //if (M.IsChecked == null)
                //    sex = "F";
                //else if (F.IsChecked == null)
                    sex = "M";
                NavigationService.Navigate(new Uri(
                    "/MainPage.xaml?nm=" + 
                    name.Text + "&amp;age=" + 
                    age.Text + "&amp;sex=" +
                    sex + "&amp;h=" +
                    height.Text + "&amp;w=" +
                    weight.Text + "&amp;alg=" +
                    allergies.Text + "&amp;eth=" +
                    ethnicity.Text + "&amp;bld=" +
                    blood.Text, UriKind.Relative));
            //}
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void ethnicity_TextChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Image_ImageFailed(object sender, ExceptionRoutedEventArgs e)
        {

        }
    }
}