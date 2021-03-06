﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.IO;

namespace FastComments
{
    /// <summary>
    /// Interaction logic for AddCommentsWindow.xaml
    /// </summary>
    public partial class AddCommentsWindow : Window
    {
        ObservableCollection<Item> items;
        String myTitle;
        const int filenameMaxLen = 54;
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="lista">Core data</param>
        public AddCommentsWindow(ref ObservableCollection<Item> lista)
        {
            InitializeComponent();
            items = lista;
            myTitle = Title;
            // "database"
            filenameTB.Text = shortenFilePath(Properties.Settings.Default.DBFilename, filenameMaxLen); 
            filenameTB.ToolTip = Properties.Settings.Default.DBFilename; 
            if(Path.GetDirectoryName(Properties.Settings.Default.DBFilename).Length==0)
            {
                filenameTB.ToolTip = $"{Directory.GetCurrentDirectory()}\\{Properties.Settings.Default.DBFilename}";
            }
            
        }

        /// <summary>
        /// Add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Item item = new Item(codeTextBox.Text, commentTextBox.Text);
            //if (!myContains(item)) // uses Compare            
            if(!item.isContainedIn(items))
            {
                items.Add(item);
                codeTextBox.Text = "";
                codeTextBox.Focus();
                commentTextBox.Text = "";
                //Title = String.Format("{0} ({1} comments)", myTitle, items.Count);
                Title = String.Format(Properties.Resources.acw_title, myTitle, items.Count);
                //Title = myTitle + " (" + items.Count + " comments)";
            }
            else MessageBox.Show(Properties.Resources.mb_codeused);
        }

        /// <summary>
        /// Done button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //DialogResult = true;
            Close();
        }

        /// <summary>
        /// Browse button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btFilename_Click(object sender, RoutedEventArgs e)
        {
            GetDBDirectory(this);
            filenameTB.Text = shortenFilePath(Properties.Settings.Default.DBFilename, filenameMaxLen);
            filenameTB.ToolTip = Properties.Settings.Default.DBFilename;
        }

        /// <summary>
        /// Ask directory and save in properties/settings
        /// <param name="w">Window</param>
        /// </summary>
        internal static void GetDBDirectory(Window w)
        {
            // called also from MainWindow
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            if (Path.GetDirectoryName(Properties.Settings.Default.DBFilename).Length == 0)
            {
                sfd.InitialDirectory = Directory.GetCurrentDirectory();
            }
            sfd.FileName = //Properties.Settings.Default.DBFilename;
                sfd.Title = Properties.Resources.acw_selectfolder;
            //sfd.Filter = "XML Database files |*.xml";
            //sfd.DefaultExt = "xml";
            sfd.CheckPathExists = true;
            sfd.CheckFileExists = false;
            sfd.ValidateNames = false;
            sfd.Title = Properties.Resources.acw_selectfolder;

            if (sfd.ShowDialog() == true)
            {
                try
                {
                    // check file path                                              
                    Properties.Settings.Default.DBFilename = Path.GetDirectoryName(sfd.FileName) + "\\" +
                        Properties.Settings.Default.DefaultDBFileName;
                    Properties.Settings.Default.Save();
                    if (w is AddCommentsWindow) // hack to enable static func to b called from this window
                    {
                        w.DialogResult = true;
                    }
                }
                catch (Exception ex) when (ex is ArgumentException || ex is PathTooLongException)
                {
                    MessageBox.Show("Tarkista hakemisto");//TODO:
                }
              }
        }

        /* moved to Item.cs
internal bool myContains(Item item)
{
   foreach(Item i in items)
   {
       if (i.CompareTo(item) == 0) return true;
   }
   return false;
}*/

        /// <summary>
        /// Typically for file path shortened view
        /// </summary>
        /// <param name="s"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        private string shortenFilePath(string s, int maxLength)
        {                        
            if (s.Length > maxLength)
            {
                int shortenedLength = maxLength / 2 - (s.Length - maxLength)/2;
                int len = Math.Max(Path.GetFileName(s).Length, shortenedLength) + 1;
                return s.Substring(0, shortenedLength) + "..." + s.Substring(s.Length - len , len);
            }
            else
            {
                return s;
            }
        }
    }
}
