using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Syndication;
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
using System.Xaml;
using System.Xml;

namespace WPF_App_Experiment
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void On_Load(object sender, RoutedEventArgs e)
        {
            string url = "https://www.thehindu.com/business/markets/feeder/default.rss";
            XmlDocument XDoc = new XmlDocument();
            XDoc.Load(url);
            XmlNodeList XMLNodes;
            XMLNodes = XDoc.ChildNodes[1].ChildNodes[0].ChildNodes;
            foreach(XmlNode Node in XMLNodes)
            {
                if(Node.Name=="item")
                {
                    string test_1 = Node.ChildNodes[0].InnerText;
                    string test_2 = Node.ChildNodes[4].InnerText;
                    string test_3 = Node.ChildNodes[5].ChildNodes[0].Value;
                    News_Bubbles uc1 = new News_Bubbles();
                    uc1.Heading_Box_Text = test_1.Trim();
                    uc1.Content_Box_Text = test_2.Trim();
                    uc1.Timestamp_Box_Text = test_3.Trim();
                    List_Format.Items.Add(uc1);
                    //Feed_Box.Items.Add(test);
                }
            }
            
        }

        private void UserControl1_Loaded(object sender, RoutedEventArgs e)
        {
            //List_Format.Items.Add();
        }

        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Exit_Button_Action(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void MouseDown_Event(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
    }
 }
