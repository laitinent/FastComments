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
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow(ObservableCollection<Item> comments)
        {
            InitializeComponent();
            foreach (var item in comments)
            {
                tbContents.Text += item.Key + "\t\t" + item.Fulltext + "\n";
            }
            
        }
    }
}
