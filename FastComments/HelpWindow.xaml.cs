using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FastComments
{
    /// <summary>
    /// Interaction logic for HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {

        String myTitle;
        ObservableCollection<Item> comms;
        

        public HelpWindow(ref ObservableCollection<Item> comments)
        {
            InitializeComponent();
            myTitle = Title;
            comms = comments;
        }

        /// <summary>
        /// Click item on listview to edit
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listView1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Title = myTitle + " "+(listView1.SelectedItem as Item).Key + ": " + (listView1.SelectedItem as Item).Fulltext;
            int index = listView1.SelectedIndex;
            if (index >= 0)
            {
                Title = myTitle + " " + comms[index].Key + " " + comms[index].Fulltext;
                Item item = comms[index];
                EditListItemWindow ew = new EditListItemWindow(ref item);
                ew.keyTB.Text = item.Key;
                ew.textTB.Text = item.Fulltext;
                if (ew.ShowDialog() == true)
                {
                    if (item.Key.Length == 0 && item.Fulltext.Length == 0)
                    {
                        comms.RemoveAt(index);
                    }
                    else
                    {
                        if (!item.isContainedIn(comms))
                        {
                            comms[index] = item;
                        }
                        else
                        {
                            MessageBox.Show(Properties.Resources.mb_codeused);
                        }
                    }
                    listView1.Items.Refresh();
                }
                else listView1.SelectedIndex = -1;
            }
            
        }
    }
}
