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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_App_Experiment
{
    /// <summary>
    /// Interaction logic for News_Bubbles.xaml
    /// </summary>
    public partial class News_Bubbles : UserControl
    {
        public News_Bubbles()
        {
            InitializeComponent();
        }
        public string Heading_Box_Text
        {
            get { return Heading_Box.Text; }
            set { Heading_Box.Text = value; }
        }
        public string Content_Box_Text
        {
            get { return Content_Box.Text; }
            set { Content_Box.Text = value; }
        }
        public string Timestamp_Box_Text
        {
            get { return Timestamp_Box.Text; }
            set { Timestamp_Box.Text = value; }
        }
    }
}
