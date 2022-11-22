using CefSharp;
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

namespace WebBrowserWPF
{
    /// <summary>
    /// Interaction logic for TabContent.xaml
    /// </summary>
    public partial class TabContent : UserControl
    {
        public TabContent()
        {
            InitializeComponent();
        }
        public string Url
        {
            get { return (string)GetValue(UrlProperty); }
            set { SetValue(UrlProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Url.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UrlProperty =
            DependencyProperty.Register("Url", typeof(string), typeof(TabContent), new PropertyMetadata("https://www.google.com"));
        private void Browser_Initialized(object sender, EventArgs e)
        {
            Browser.Address = Url;
            /*SearchTextBox.Text = Browser.Address;*/
        }

        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            Url = "https://www.google.com";
            Browser.Load(Url);
            /*Browser.Address = Url;*/ 
        }

        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            Url = SearchTextBox.Text;
            //check whether a string is a valid HTTP URL
            //if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            //Browser.Load(Url);
            /*if (!string.IsNullOrWhiteSpace(Url))
            {
                Browser.Address = Url;
                SearchTextBox.Text = Browser.Address;
            }*/
            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                Browser.Address = Url;
                SearchTextBox.Text = Browser.Address;
                Browser.Load(Url);
            }
        }

        private void GoBackButtonClick(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoBack == true)
                Browser.Back();
        }
        private void GoForwardButtonClick(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoForward == true)
                Browser.Forward();
        }

        private void ReloadButtonClick(object sender, RoutedEventArgs e)
        {
            Browser.Reload();
        }

        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Url = SearchTextBox.Text;
                //check whether a string is a valid HTTP URL
                if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
                {
                    Browser.Address = Url;
                    SearchTextBox.Text = Browser.Address;
                    Browser.Load(Url);
                }
                /*else
                {
                    
                }*/
            }
        }

        /*private void Browser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Browser.Address = Url;
            SearchTextBox.Text = Browser.Address;
        }*/

        private void ChromiumWebBrowser_OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Url = e.Url;
            //check whether a string is a valid HTTP URL
            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute) && Url != "about:blank")
                //if (!string.IsNullOrWhiteSpace(Url))
                {
                    SearchTextBox.Text = Url;
                }
                //GoBackButton.IsEnabled = Browser.CanGoBack;
                //SearchButton.IsEnabled = !string.IsNullOrWhiteSpace(SearchTextBox.Text);
                //GoForwardButton.IsEnabled = Browser.CanGoForward;
            }));
        }
    }
}
