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
using System.ComponentModel;

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
            //TODO: hide unused buttons - no other actions
            //Title = myTitle + " "+(listView1.SelectedItem as Item).Key + ": " + (listView1.SelectedItem as Item).Fulltext;
            //EditCurrentItem();

        }

        /// <summary>
        /// Get list item and call editing method
        /// </summary>
        private void EditCurrentItem()
        {
            int index = listView1.SelectedIndex;
            if (index >= 0)
            {
                EditItemAt(index);
            }
        }

        /// <summary>
        /// Edit ListView item
        /// </summary>
        /// <param name="index">Index in Items Source</param>
        /// <param name="bUseWarning">Warn if Key is already in list</param>
        private void EditItemAt(int index, bool bUseWarning=true)
        {
            Item item = comms[index];
            //Title = myTitle + " " + comms[index].Key + " " + comms[index].Fulltext;
            Title = $"{myTitle} {item.Key} {item.Fulltext}";
            
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
                    // Entering a key that is already used is ok, if editing current item
                    if (!item.isContainedIn(comms) || ew.keyTB.Text.CompareTo(item.Key)==0)
                    {
                        comms[index] = item;
                    }
                    else
                    {
                        if(bUseWarning) MessageBox.Show(Properties.Resources.mb_codeused);
                    }
                }
                listView1.Items.Refresh();
            }
            else listView1.SelectedIndex = -1;
        }

        /// <summary>
        /// Insert button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                Item i = (button.DataContext as Item);
                comms.Insert(comms.IndexOf(i), new Item());
                EditItemAt(comms.IndexOf(i) - 1, false);
                //MessageBox.Show("Index= " + comms.IndexOf(i));
            }
        }
        /// <summary>
        /// Edit button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                Item i = (button.DataContext as Item);

                EditItemAt(comms.IndexOf(i));
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //string sortby = (listView1.View as GridView).Columns[0].Header.ToString();
            // It is possible to autosort
            //listView1.Items.SortDescriptions.Add(new SortDescription("Key", ListSortDirection.Ascending));
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            foreach(Item item in comms)
            {
                if(item.Fulltext.Length==0 && item.Key.Length>0)
                {
                    if(MessageBox.Show(Properties.Resources.hw_cancel,"Info",MessageBoxButton.YesNo)== MessageBoxResult.Yes)
                    {
                        comms.Remove(item);
                    }
                }
            }
            // cancel close and use Hide/Show, see MainWindow/Button_Click
            e.Cancel = true;
            Hide();
            // also in App.xaml Shutdown mode is changed from default to close this window on mainwindow close
        }

        // not used
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Move up on the list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUp_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                Item i = (button.DataContext as Item);
                int index = comms.IndexOf(i);
                if (index > 0) { comms.Move(index, index - 1); }
            }            
        }
        /// <summary>
        /// Move later on the list and collection
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDown_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button)
            {
                Button button = sender as Button;
                Item i = (button.DataContext as Item);
                int index = comms.IndexOf(i);
                if (index < comms.Count-1) { comms.Move(index, index + 1); }
            }
        }
    }
}
