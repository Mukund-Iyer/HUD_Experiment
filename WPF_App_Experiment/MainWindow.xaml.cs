using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private string MarketURL = "https://www.thehindu.com/business/markets/feeder/default.rss";
        private string BusinesslURL = "https://www.thehindu.com/business/feeder/default.rss";
        private string AgriBusinessURL = "https://www.thehindu.com/business/agri-business/feeder/default.rss";
        private string EconomyURL = "https://www.thehindu.com/business/Economy/feeder/default.rss";
        private string IndustryURL = "https://www.thehindu.com/business/Industry/feeder/default.rss";
        private string BudgetURL = "https://www.thehindu.com/business/budget/feeder/default.rss";
        private string DefaultURL = "https://www.thehindu.com/business/feeder/default.rss";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void On_Load(object sender, RoutedEventArgs e)
        {
        }

        private async Task LoadData(string Option)
        {
            List_Format.Items.Clear();
            string url = GetUrlForOption(Option);

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string xmlData = await client.GetStringAsync(url);
                    XmlDocument XDoc = new XmlDocument();
                    XDoc.LoadXml(xmlData);

                    XmlNodeList XMLNodes = XDoc.SelectNodes("/rss/channel/item");

                    if (XMLNodes != null)
                    {
                        List<Task> downloadTasks = new List<Task>();

                        foreach (XmlNode Node in XMLNodes)
                        {
                            News_Bubbles uc1 = new News_Bubbles();

                            foreach (XmlNode Itex in Node.ChildNodes)
                            {
                                switch (Itex.Name)
                                {
                                    case "title":
                                        uc1.Heading_Box_Text = Itex.InnerText;
                                        break;
                                    case "description":
                                        uc1.Content_Box_Text = Itex.InnerText;
                                        break;
                                    case "pubDate":
                                        uc1.Timestamp_Box_Text = Itex.InnerText;
                                        break;
                                    case "media:content":
                                        downloadTasks.Add(DownloadImageAsync(Itex, uc1));
                                        break;
                                }
                            }

                            if (!string.IsNullOrEmpty(uc1.Heading_Box_Text) && !string.IsNullOrEmpty(uc1.Content_Box_Text) && !string.IsNullOrEmpty(uc1.Timestamp_Box_Text))
                            {
                                List_Format.Items.Add(uc1);
                            }
                        }

                        await Task.WhenAll(downloadTasks);
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions
            }
        }

        private async Task DownloadImageAsync(XmlNode mediaContentNode, News_Bubbles uc1)
        {
            string ImageLink = mediaContentNode.Attributes["url"].Value;

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] data = await client.GetByteArrayAsync(ImageLink);

                    using (MemoryStream stream = new MemoryStream(data))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = stream;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();

                        uc1.Content_Image.Source = bitmapImage;
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle image download exceptions
            }
        }

        private string GetUrlForOption(string option)
        {
            switch (option)
            {
                case "Markets":
                    return MarketURL;
                case "Business":
                    return BusinesslURL;
                case "Economy":
                    return EconomyURL;
                case "Industry":
                    return IndustryURL;
                case "Agri-Business":
                    return AgriBusinessURL;
                default:
                    return DefaultURL;
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

        private void TransparencySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            MainGrid.Opacity = TransparencySlider.Value;
        }

        private void RefreshButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LoadData(Top_Label.SelectedValue.ToString());
        }

        private void Top_Label_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            System.Windows.Controls.ComboBox x = (System.Windows.Controls.ComboBox)sender;
            LoadData(Top_Label.SelectedValue.ToString());
        }
    }
 }
