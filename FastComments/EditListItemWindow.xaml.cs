using System;
using System.Collections.Generic;
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
    /// Interaction logic for EditListItemWindow.xaml
    /// </summary>
    public partial class EditListItemWindow : Window
    {

        Item editItem;
        public EditListItemWindow(ref Item item)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.Manual;
            textTB.Focus();
            editItem = item;
        }
        /// <summary>
        /// OK button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            editItem.Key = keyTB.Text;
            editItem.Fulltext = textTB.Text;
            DialogResult = true;
            Close();
        }
        /// <summary>
        /// Cancel button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
