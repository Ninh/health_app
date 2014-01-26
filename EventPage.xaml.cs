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
    public partial class EventPage : PhoneApplicationPage, INotifyPropertyChanged
    {

        // Data context for the local database
        private CondDataContext CondDB;

        // Define an observable collection property that controls can bind to.
        private ObservableCollection<CondItem> _CondItems;
        public ObservableCollection<CondItem> CondItems
        {
            get
            {
                return _CondItems;
            }
            set
            {
                if (_CondItems != value)
                {
                    _CondItems = value;
                    NotifyPropertyChanged("CondItems");
                }
            }
        }
        
        // Constructor
        public EventPage()
        {
            InitializeComponent();

            // Connect to the database and instantiate data context.
            CondDB = new CondDataContext(CondDataContext.DBConnectionString);

            // Data context and observable collection are children of the Edit page.
            this.DataContext = this;
        }

        private void discuss_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/DiscussPage.xaml", UriKind.Relative));
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            if (this.NavigationService.CanGoBack)
            {
                this.NavigationService.GoBack();
            }
        }

        private void deleteTaskButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast parameter as a button.
            var button = sender as Button;

            if (button != null)
            {
                // Get a handle for the to-do item bound to the button.
                CondItem CondForDelete = button.DataContext as CondItem;

                // Remove the to-do item from the observable collection.
                CondItems.Remove(CondForDelete);

                // Remove the to-do item from the local database.
                CondDB.CondItems.DeleteOnSubmit(CondForDelete);

                // Save changes to the database.
                CondDB.SubmitChanges();

                // Put the focus back to the Edit page.
                this.Focus();
            }
        }

        private void newCondTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            // Clear the text box when it gets focus.
            newCondTextBox.Text = String.Empty;
        }

        private void newCondAddButton_Click(object sender, RoutedEventArgs e)
        {
            //sets maximum characters at 30
            if (newCondTextBox.Text.Length >= 5 && newCondTextBox.Text != "Add medical condition")
            {

                // Create a new to-do item based on the text box.
                CondItem newCond = new CondItem { ItemName = newCondTextBox.Text };

                // Add a to-do item to the observable collection.
                CondItems.Add(newCond);

                // Add a to-do item to the local database.
                CondDB.CondItems.InsertOnSubmit(newCond);

            }
            else
            {
                //create error message
                //for Minimum length
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Call the base method.
            base.OnNavigatedFrom(e);

            // Save changes to the database.
            CondDB.SubmitChanges();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Define the query to gather all of the to-do items.
            var CondItemsInDB = from CondItem Cond in CondDB.CondItems
                                select Cond;

            // Execute the query and place the results into a collection.
            CondItems = new ObservableCollection<CondItem>(CondItemsInDB);

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

    public class CondDataContext : DataContext
    {
        // Specify the connection string as a static, used in Edit page and app.xaml.
        public static string DBConnectionString = "Data Source=isostore:/Cond.sdf";

        // Pass the connection string to the base class.
        public CondDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a single table for the to-do items.
        public Table<CondItem> CondItems;
    }

    [Table]
    public class CondItem : INotifyPropertyChanged, INotifyPropertyChanging
    {
        // Define ID: private field, public property and database column.
        private int _CondItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int CondItemId
        {
            get
            {
                return _CondItemId;
            }
            set
            {
                if (_CondItemId != value)
                {
                    NotifyPropertyChanging("CondItemId");
                    _CondItemId = value;
                    NotifyPropertyChanged("CondItemId");
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