using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FastComments
{
    /// <summary>
    /// Core class in application - user enters Key to generate "fulltext" contents to be copied to email etc. application
    /// </summary>
    public class Item : IComparable
    {
        public string Key { get; set; }
        public string Fulltext { get => fulltext; set => fulltext = value; }

        String fulltext;

        public Item()
        {

        }

        public Item(String k, String text)
        {
            Key = k;
            Fulltext = text;
        }

        public int CompareTo(object obj)
        {
            return Key.CompareTo((obj as Item).Key);
        }

        /// <summary>
        /// Is this item member of given list
        /// </summary>
        /// <param name="itemsList">List containing items</param>
        /// <returns>true is is member of the list, false if not</returns>
        public bool isContainedIn(ObservableCollection<Item> itemsList)
        {
            foreach (Item i in itemsList)
            {
                if (i.CompareTo(this) == 0) return true;
            }
            return false;
        }
    }
}