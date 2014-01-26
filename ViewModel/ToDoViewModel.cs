/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

// Directive for the data model.
using LocalDatabaseSample.Model;


namespace LocalDatabaseSample.ViewModel
{
    public class CondViewModel : INotifyPropertyChanged
    {
        // LINQ to SQL data context for the local database.
        private CondDataContext CondDB;

        // Class constructor, create the data context object.
        public CondViewModel(string CondDBConnectionString)
        {
            CondDB = new CondDataContext(CondDBConnectionString);
        }

        // All to-do items.
        private ObservableCollection<CondItem> _allCondItems;
        public ObservableCollection<CondItem> AllCondItems
        {
            get { return _allCondItems; }
            set
            {
                _allCondItems = value;
                NotifyPropertyChanged("AllCondItems");
            }
        }

        // To-do items associated with the Infectious category.
        private ObservableCollection<CondItem> _InfectiousCondItems;
        public ObservableCollection<CondItem> InfectiousCondItems
        {
            get { return _InfectiousCondItems; }
            set
            {
                _InfectiousCondItems = value;
                NotifyPropertyChanged("InfectiousCondItems");
            }
        }

        // To-do items associated with the Cancer category.
        private ObservableCollection<CondItem> _CancerCondItems;
        public ObservableCollection<CondItem> CancerCondItems
        {
            get { return _CancerCondItems; }
            set
            {
                _CancerCondItems = value;
                NotifyPropertyChanged("CancerCondItems");
            }
        }

        // To-do items associated with the Seasonal category.
        private ObservableCollection<CondItem> _SeasonalCondItems;
        public ObservableCollection<CondItem> SeasonalCondItems
        {
            get { return _SeasonalCondItems; }
            set
            {
                _SeasonalCondItems = value;
                NotifyPropertyChanged("SeasonalCondItems");
            }
        }

        // A list of all categories, used by the add task page.
        private List<CondCategory> _categoriesList;
        public List<CondCategory> CategoriesList
        {
            get { return _categoriesList; }
            set
            {
                _categoriesList = value;
                NotifyPropertyChanged("CategoriesList");
            }
        }

        // Write changes in the data context to the database.
        public void SaveChangesToDB()
        {
            CondDB.SubmitChanges();
        }

        // Query database and load the collections and list used by the pivot pages.
        public void LoadCollectionsFromDatabase()
        {

            // Specify the query for all to-do items in the database.
            var CondItemsInDB = from CondItem Cond in CondDB.Items
                                select Cond;

            // Query the database and load all to-do items.
            AllCondItems = new ObservableCollection<CondItem>(CondItemsInDB);

            // Specify the query for all categories in the database.
            var CondCategoriesInDB = from CondCategory category in CondDB.Categories
                                     select category;


            // Query the database and load all associated items to their respective collections.
            foreach (CondCategory category in CondCategoriesInDB)
            {
                switch (category.Name)
                {
                    case "Infectious":
                        InfectiousCondItems = new ObservableCollection<CondItem>(category.Conds);
                        break;
                    case "Cancer":
                        CancerCondItems = new ObservableCollection<CondItem>(category.Conds);
                        break;
                    case "Seasonal":
                        SeasonalCondItems = new ObservableCollection<CondItem>(category.Conds);
                        break;
                    default:
                        break;
                }
            }

            // Load a list of all categories.
            CategoriesList = CondDB.Categories.ToList();

        }

        // Add a to-do item to the database and collections.
        public void AddCondItem(CondItem newCondItem)
        {
            // Add a to-do item to the data context.
            CondDB.Items.InsertOnSubmit(newCondItem);

            // Save changes to the database.
            CondDB.SubmitChanges();

            // Add a to-do item to the "all" observable collection.
            AllCondItems.Add(newCondItem);

            // Add a to-do item to the appropriate filtered collection.
            switch (newCondItem.Category.Name)
            {
                case "Infectious":
                    InfectiousCondItems.Add(newCondItem);
                    break;
                case "Cancer":
                    CancerCondItems.Add(newCondItem);
                    break;
                case "Seasonal":
                    SeasonalCondItems.Add(newCondItem);
                    break;
                default:
                    break;
            }
        }

        // Remove a to-do task item from the database and collections.
        public void DeleteCondItem(CondItem CondForDelete)
        {

            // Remove the to-do item from the "all" observable collection.
            AllCondItems.Remove(CondForDelete);

            // Remove the to-do item from the data context.
            CondDB.Items.DeleteOnSubmit(CondForDelete);

            // Remove the to-do item from the appropriate category.   
            switch (CondForDelete.Category.Name)
            {
                case "Infectious":
                    InfectiousCondItems.Remove(CondForDelete);
                    break;
                case "Cancer":
                    CancerCondItems.Remove(CondForDelete);
                    break;
                case "Seasonal":
                    SeasonalCondItems.Remove(CondForDelete);
                    break;
                default:
                    break;
            }

            // Save changes to the database.
            CondDB.SubmitChanges();
        }

        // Valid priorities for new tasks.
        // Added in Version 2 of the application.
        private List<int> _prioritiesList = new List<int> { 1, 2, 3 };
        public List<int> PrioritiesList
        {
            get { return _prioritiesList; }
        }
        

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify Silverlight that a property has changed.
        private void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion
    }
}
