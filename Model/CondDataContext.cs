/* 
    Copyright (c) 2011 Microsoft Corporation.  All rights reserved.
    Use of this sample source code is subject to the terms of the Microsoft license 
    agreement under which you licensed this sample source code and is provided AS-IS.
    If you did not accept the terms of the license agreement, you are not authorized 
    to use this sample source code.  For the terms of the license, please see the 
    license agreement between you and Microsoft.
  
    To see all Code Samples for Windows Phone, visit http://go.microsoft.com/fwlink/?LinkID=219604 
  
*/
using System;
using System.ComponentModel;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace LocalDatabaseSample.Model
{

    public class CondDataContext : DataContext
    {
        // Pass the connection string to the base class.
        public CondDataContext(string connectionString)
            : base(connectionString)
        { }

        // Specify a table for the to-do items.
        public Table<CondItem> Items;

        // Specify a table for the categories.
        public Table<CondCategory> Categories;
    }

    [Table]
    public class CondItem : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _CondItemId;

        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL Identity", CanBeNull = false, AutoSync = AutoSync.OnInsert)]
        public int CondItemId
        {
            get { return _CondItemId; }
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

        // Define item name: private field, public property, and database column.
        private string _itemName;

        [Column]
        public string ItemName
        {
            get { return _itemName; }
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

        // Define completion value: private field, public property, and database column.
        private bool _isComplete;

        [Column]
        public bool IsComplete
        {
            get { return _isComplete; }
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


        // Internal column for the associated CondCategory ID value
        [Column]
        internal int _categoryId;

        // Entity reference, to identify the CondCategory "storage" table
        private EntityRef<CondCategory> _category;

        // Association, to describe the relationship between this key and that "storage" table
        [Association(Storage = "_category", ThisKey = "_categoryId", OtherKey = "Id", IsForeignKey = true)]
        public CondCategory Category
        {
            get { return _category.Entity; }
            set
            {
                NotifyPropertyChanging("Category");
                _category.Entity = value;

                if (value != null)
                {
                    _categoryId = value.Id;
                }

                NotifyPropertyChanging("Category");
            }
        }

        // Define Priority: field, public property, and database column.
        // Added in Version 2 of the application.
        private int? _priority;

        [Column(CanBeNull = true)]
        public int? Priority
        {
            get { return _priority; }
            set
            {
                if (_priority != value)
                {
                    NotifyPropertyChanging("Priority");
                    _priority = value;
                    NotifyPropertyChanged("Priority");
                }
            }
        }


        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
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

        // Used to notify that a property is about to change
        private void NotifyPropertyChanging(string propertyName)
        {
            if (PropertyChanging != null)
            {
                PropertyChanging(this, new PropertyChangingEventArgs(propertyName));
            }
        }

        #endregion
    }


    [Table]
    public class CondCategory : INotifyPropertyChanged, INotifyPropertyChanging
    {

        // Define ID: private field, public property, and database column.
        private int _id;

        [Column(DbType = "INT NOT NULL IDENTITY", IsDbGenerated = true, IsPrimaryKey = true)]
        public int Id
        {
            get { return _id; }
            set
            {
                NotifyPropertyChanging("Id");
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        // Define category name: private field, public property, and database column.
        private string _name;

        [Column]
        public string Name
        {
            get { return _name; }
            set
            {
                NotifyPropertyChanging("Name");
                _name = value;
                NotifyPropertyChanged("Name");
            }
        }

        // Version column aids update performance.
        [Column(IsVersion = true)]
        private Binary _version;

        // Define the entity set for the collection side of the relationship.
        private EntitySet<CondItem> _Conds;

        [Association(Storage = "_Conds", OtherKey = "_categoryId", ThisKey = "Id")]
        public EntitySet<CondItem> Conds
        {
            get { return this._Conds; }
            set { this._Conds.Assign(value); }
        }


        // Assign handlers for the add and remove operations, respectively.
        public CondCategory()
        {
            _Conds = new EntitySet<CondItem>(
                new Action<CondItem>(this.attach_Cond),
                new Action<CondItem>(this.detach_Cond)
                );
        }

        // Called during an add operation
        private void attach_Cond(CondItem Cond)
        {
            NotifyPropertyChanging("CondItem");
            Cond.Category = this;
        }

        // Called during a remove operation
        private void detach_Cond(CondItem Cond)
        {
            NotifyPropertyChanging("CondItem");
            Cond.Category = null;
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        // Used to notify that a property changed
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

        // Used to notify that a property is about to change
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
