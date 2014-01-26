using System;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel;
using System.Collections.ObjectModel;
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
    public partial class ProfilePage : PhoneApplicationPage, INotifyPropertyChanged
    {
        // Data context for the local database
        private UserDataContext UserDB;

        // Define an observable collection property that controls can bind to.
        private ObservableCollection<UserItem> _UserItems;
        public ObservableCollection<UserItem> UserItems
        {
            get
            {
                return _UserItems;
            }
            set
            {
                if (_UserItems != value)
                {
                    _UserItems = value;
                    NotifyPropertyChanged("UserItems");
                }
            }
        }
        
        public ProfilePage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            UserDB = new UserDataContext(UserDataContext.DBConnectionString);

            // Data context and observable collection are children of the Edit page.
            this.DataContext = this;
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                string sex = string.Empty;
                if (F.IsChecked.Value)
                    sex = "F";
                else if (M.IsChecked.Value)
                    sex = "M";
                NavigationService.Navigate(new Uri(
                    "/MainPage.xaml?qry=" + 
                    name.Text + "|" + 
                    age.Text + "|" +
                    sex + "|" +
                    height.Text + "|" +
                    weight.Text + "|" +
                    allergies.Text + "|" +
                    ethnicity.Text + "|" + blood.Text,
                    UriKind.Relative));   
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            UserDB.SubmitChanges();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var UserItemsInDB = from UserItem User in UserDB.UserItems
                                select User;

            // Execute the query and place the results into a collection.
            UserItems = new ObservableCollection<UserItem>(UserItemsInDB);

            // Call the base method.
            base.OnNavigatedTo(e);
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the app that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }

    public class UserDataContext : DataContext
    {
        // Specify the connection string as a static, used in Edit page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/User.sdf";

        // Pass the connection string to the base class.
        public UserDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the to-do items.
        public Table<UserItem> UserItems;
    }

    [Table]
    public class UserItem : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _UserItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int UserItemId
        {
            get
            {
                return _UserItemId;
            }
            set
            {
                if (_UserItemId != value)
                {
                    NotifyPropertyChanging("UserItemId");
                    _UserItemId = value;
                    NotifyPropertyChanged("UserItemId");
                }
            }
        }

        // Define item name: private field, public property and database column.
        private string _itemName;

        [Column]
        public string ItemName
        {
            get
            {
                return _itemName;
            }
            set
            {
                if (_itemName != value)
                {
                    NotifyPropertyChanging("ItemName");
                    _itemName = value;
                    NotifyPropertyChanged("ItemName");
                }
            }
        }

        // Define completion value: private field, public property and database column.
        private bool _isComplete;

        [Column]
        public bool IsComplete
        {
            get
            {
                return _isComplete;
            }
            set
            {
                if (_isComplete != value)
                {
                    NotifyPropertyChanging("IsComplete");
                    _isComplete = value;
                    NotifyPropertyChanged("IsComplete");
                }
            }
        }
        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify the page that a data context property changed
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region INotifyPropertyChanging Members

        public event PropertyChangingEventHandler PropertyChanging;

        // Used to notify the data context that a data context property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }
}