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
    /// Interaction logic for AddCommentsWindow.xaml
    /// </summary>
    public partial class AddCommentsWindow : Window
    {
        ObservableCollection<Item> items;
        String myTitle;
        public AddCommentsWindow(ref ObservableCollection<Item> lista)
        {
            InitializeComponent();
            items = lista;
            myTitle = Title;
            filenameTB.Text = Properties.Settings.Default.DBFilename;
        }

        /// <summary>
        /// Add button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            items.Add(new Item(codeTextBox.Text, commentTextBox.Text));
            codeTextBox.Text = "";
            codeTextBox.Focus();
            commentTextBox.Text = "";
            Title = myTitle + " ("+items.Count+" comments)";
        }

        /// <summary>
        /// Done button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }

        private void btFilename_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog sfd = new Microsoft.Win32.SaveFileDialog();
            sfd.FileName = Properties.Settings.Default.DBFilename;
            if(sfd.ShowDialog()==true)
            {
                Properties.Settings.Default.DBFilename= sfd.FileName;
                Properties.Settings.Default.Save();
                filenameTB.Text = Properties.Settings.Default.DBFilename;
            }
        }
    }
}
