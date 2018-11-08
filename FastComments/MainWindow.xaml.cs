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
        HelpWindow helpWindow;

        public MainWindow()
        {
            // to test localized version
            //System.Threading.Thread.CurrentThread.CurrentUICulture =  new System.Globalization.CultureInfo("fi-FI");
            InitializeComponent();
            tbVersion.Text = "v. "+System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            codeTextBox.ToolTip = $"{Properties.Resources.mw_code_tt} {Properties.Resources.mw_help} & {Properties.Resources.mw_report}"; 
        }

        /// <summary>
        /// "Code/Key" Text changed - not used, used Enter button instead
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (codeTextBox.Text.Length > 0)
            {
                btEnter.IsEnabled = true;
            }
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

        /// <summary>
        /// Help button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (helpWindow == null)
            {
                // use existing window, if already created
                helpWindow = new HelpWindow(ref Comments);
                helpWindow.listView1.ItemsSource = Comments;
            }

            // button toggles Show/Hide
            if (helpWindow.Visibility != Visibility.Visible)
            {
                helpWindow.Show();
            }
            else
            {
                helpWindow.Hide();
            }
        }


        //---Save & Restore on Window close/load----//
        private void Save(String _filename)
        {            
            // Insert code to set properties and fields of the object.  
            XmlSerializer mySerializer = new XmlSerializer(typeof(ObservableCollection<Item>));

            StreamWriter myWriter;
            try
            {
                // To write to a file, create a StreamWriter object.  
                myWriter = new StreamWriter(_filename);
                mySerializer.Serialize(myWriter, Comments);
                myWriter.Close();
            }
            catch (Exception ex) { MessageBox.Show($"Tietojen tallennuksessa ongelmia ({ex.Message})"); }
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
            catch (FileNotFoundException)
            {
                Button_Click_1(this, null);
            }
            catch (Exception ex) { MessageBox.Show($"Tietojen lukemisessa ongelmia ({ex.Message})"); }
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


            /// <summary>
            /// Settings -button
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddCommentsWindow addWindow = new AddCommentsWindow(ref Comments);            
            addWindow.ShowDialog();// may modify filename
            filename = Properties.Settings.Default.DBFilename;
        }

        /// <summary>
        /// Undo button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (listFulltext.Count > 0)
            {
                try
                {
                    listFulltext.RemoveAt(listFulltext.Count - 1);
                    UpdateTBFromList();
                    if (listFulltext.Count == 0) btClear.IsEnabled = false;
                }
                catch (ArgumentOutOfRangeException ) { MessageBox.Show("Poisto ei onnistunut"); }
            }
        }

        /// <summary>
        /// "Enter" click - add comment to tb
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            string fullText;
            // check if Code can be found
            foreach (var item in Comments)
            {
                if (item.Key.CompareTo(codeTextBox.Text) == 0)
                {
                    fullText = item.Fulltext;
                    listFulltext.Add(fullText);

                    UpdateTBFromList();
                    btUndo.IsEnabled = true;
                    btClear.IsEnabled = true;

                    codeTextBox.Text = "";
                    btEnter.IsEnabled = false;

                    if (copyCheckbox.IsChecked == true) {
                        try
                        {
                            Clipboard.SetText(fullTextBox.Text);
                        }
                        catch (ArgumentNullException ) { /* ei kopioitavaa (argumentnullexception) */}
                    }
                    break;
                }
            }
            codeTextBox.Focus();
        }

        // List all codes -button
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            ReportWindow rw = new ReportWindow(Comments);
            rw.ShowDialog();
        }

        // Clear button
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            listFulltext.Clear();
            UpdateTBFromList();
        }

        private void fullTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (fullTextBox.Text.Length == 0) btClear.IsEnabled = false;
        }
    }
}
