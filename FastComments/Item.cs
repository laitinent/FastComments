using System;

namespace FastComments
{
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
    }
}