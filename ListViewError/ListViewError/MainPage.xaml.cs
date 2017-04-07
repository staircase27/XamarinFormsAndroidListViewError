// © Copyright Meyertech Ltd. 2007-2017
// 
// All rights are reserved. Reproduction or transmission in whole or in part, in
// any form or by any means, electronic, mechanical or otherwise, is prohibited
// without the prior written consent of the copyright owner.
// 
// Solution: ListViewError
// Project: ListViewError
// 
// Last edited by Simon Armstrong:
// 2017 - 04 - 06  @ 16:52


using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Xamarin.Forms;

namespace ListViewError
{
    public partial class MainPage
    {
        public MainPage()
        {
            _items = new ObservableCollection<Item>(Enumerable.Range(0, 10).Select(i => new Item(i)));
            InitializeComponent();
            ListView.ItemsSource = Items;
        }


        public IEnumerable<Item> Items => _items;


        public sealed class Item: INotifyPropertyChanged
        {
            private bool _expand;


            public Item(int id)
            {
                Id = id;
                Expand = false;
            }


            public int Id { get; }

            public bool Expand
            {
                get { return _expand; }
                set
                {
                    if (value == _expand) return;
                    _expand = value;
                    OnPropertyChanged();
                }
            }

            public event PropertyChangedEventHandler PropertyChanged;


            [NotifyPropertyChangedInvocator]
            private void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (_selectedItem != null)
                _selectedItem.Expand = false;
            _selectedItem = e.SelectedItem as Item;
            if (_selectedItem != null)
                _selectedItem.Expand = true;
        }


        private void MenuItem_OnClicked(object sender, EventArgs e)
        {
            _items.RemoveAt(0);
            ListView.ItemsSource = null;
            ListView.ItemsSource = Items;
        }


        private Item _selectedItem;
        private readonly ObservableCollection<Item> _items;
    }
}