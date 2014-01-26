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

            string alg, _age, _sex, h, w, nm, eth, bld;
            alg = _age = _sex = h = w = nm = eth = bld = string.Empty;

            if (NavigationContext.QueryString.TryGetValue("alg", out alg) && alg != string.Empty)
                allergies.Text = alg;
            if (NavigationContext.QueryString.TryGetValue("nm", out nm) && nm != string.Empty)
                name.Text = nm;
            if (NavigationContext.QueryString.TryGetValue("age", out _age) && _age != string.Empty)
                age.Text = _age;
            if (NavigationContext.QueryString.TryGetValue("sex", out _sex) && _sex != string.Empty)
                sex.Text = _sex;
            if (NavigationContext.QueryString.TryGetValue("h", out h) && h != string.Empty)
                height.Text = h;
            if (NavigationContext.QueryString.TryGetValue("w", out w) && w != string.Empty)
                weight.Text = w;
            if (NavigationContext.QueryString.TryGetValue("eth", out eth) && alg != string.Empty)
                ethnicity.Text = eth;
            if (NavigationContext.QueryString.TryGetValue("bld", out bld) && bld != string.Empty)
                blood.Text = bld;
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