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
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            string qry = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("qry", out qry))
            {
                string[] fields = { "name.Text", "age.Text", "sex.Text", "height.Text", "weight.Text", "allergies.Text", "ethnicity.Text", "blood.Text" };
                string[] parameters = qry.Split(new char[] { '|' });
                //name.Text = parameters[0];
                //for(int i = 0; i < 8; i++)
                //{
                //    if (parameters[i] == null)
                //        continue;
                //    else
                //        fields[i] = parameters[i];
                //}
                if (String.Compare(parameters[0].Trim(),"") != 0 && parameters[0] != null)
                    name.Text = parameters[0];
                if (String.Compare(parameters[1].Trim(), "") != 0 && parameters[1] != null)
                    age.Text = parameters[1];
                if (String.Compare(parameters[2].Trim(), "") != 0 && parameters[2] != null)
                    sex.Text = parameters[2];
                if (String.Compare(parameters[3].Trim(), "") != 0 && parameters[3] != null)
                    height.Text = parameters[3];
                if (String.Compare(parameters[4].Trim(), "") != 0 && parameters[4] != null)
                    weight.Text = parameters[4];
                if (String.Compare(parameters[5].Trim(), "") != 0 && parameters[5] != null)
                    allergies.Text = parameters[5];
                if (String.Compare(parameters[6].Trim(), "") != 0 && parameters[6] != null)
                    ethnicity.Text = parameters[6];
                if (String.Compare(parameters[7].Trim(), "") != 0 && parameters[7] != null)
                    blood.Text = parameters[7];
            }
        }

        private void toEdit_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/ProfilePage.xaml", UriKind.Relative));
        }

        private void toNetwork_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NetworkPage.xaml", UriKind.Relative));
        }

        private void toEvent_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/EventPage.xaml", UriKind.Relative));
        }
    }
}