using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;


namespace FastComments
{
    

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String filename = "FastCommentsDB.xml";
        ObservableCollection<Item> Comments = new ObservableCollection<Item>(); // code+text from file
        List<string> listFulltext = new List<string>(); // in sync with full text box

        public MainWindow()
        {
            InitializeComponent();
            /* to test it
            Item ab = new Item("ab", "Korjaa abstraktia.");
            if (!Comments.Contains(ab))
            {
                Comments.Add(ab);
                Comments.Add(new Item("ti", "Korjaa tiivistelmää."));
                Comments.Add(new Item("kn", "Kuvien numerointi kuntoon."));
            }*/
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            

        }
        /// <summary>
        /// update full text from list (for Undo)
        /// </summary>
        private void UpdateTBFromList()
        {
            fullTextBox.Text = "";
            foreach (var textItem in listFulltext)
            {
                fullTextBox.Text += textItem + Environment.NewLine;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow(ref Comments);
            helpWindow.listView1.ItemsSource = Comments;
            helpWindow.Show();
        }


        //---Save & Restore on Window close/load----//
        private void Save(String _filename)
        {            
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new XmlSerializer(typeof(ObservableCollection<Item>));
            // To write to a file, create a StreamWriter object.  
            StreamWriter myWriter = new StreamWriter(_filename);
            mySerializer.Serialize(myWriter, Comments);
            myWriter.Close();
        }

        private void Restore(String _filename)
        {
            
            // Construct an instance of the XmlSerializer with the type  
            // of object that is being deserialized.  
            XmlSerializer mySerializer =  new XmlSerializer(typeof(ObservableCollection<Item>));
            // To read the file, create a FileStream.  
            try
            {
                FileStream myFileStream = new FileStream(_filename, FileMode.Open);

                // Call the Deserialize method and cast to the object type.  
                Comments = (ObservableCollection<Item>)mySerializer.Deserialize(myFileStream);
            }
            catch (FileNotFoundException )
            {
                Button_Click_1(this, null);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Save(filename);
            Properties.Settings.Default.DBFilename = filename;
            Properties.Settings.Default.Save();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            filename = Properties.Settings.Default.DBFilename;
            Restore(filename);
        }
        // --------//


        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddCommentsWindow addWindow = new AddCommentsWindow(ref Comments);            
            addWindow.ShowDialog();// may modify filename
            filename = Properties.Settings.Default.DBFilename;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listFulltext.Count > 0)
            {
                listFulltext.RemoveAt(listFulltext.Count - 1);
                UpdateTBFromList();
            }
        }

        /// <summary>
        /// "Enter" click - add comment to tb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            String fullText;
            // check if Code can be found
            foreach (var item in Comments)
            {
                if (item.Key.CompareTo(codeTextBox.Text) == 0)
                {
                    fullText = item.Fulltext;
                    listFulltext.Add(fullText);

                    UpdateTBFromList();

                    codeTextBox.Text = "";
                    if (copyCheckbox.IsChecked == true) { Clipboard.SetText(fullTextBox.Text); }
                    break;
                }
            }
            codeTextBox.Focus();
        }
    }
}
