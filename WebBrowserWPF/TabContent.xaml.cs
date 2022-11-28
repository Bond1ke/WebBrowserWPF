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
        public static List<string> WebPages = new List<string>();
        public static int Current = 0;
        public static ContextMenu contextmenu = new ContextMenu();
        //public static 
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
            DependencyProperty.Register("Url", typeof(string), typeof(TabContent), new PropertyMetadata("https://www.google.com/"));
        private void Browser_Initialized(object sender, EventArgs e)
        {
            Browser.Address = Url;
        }
        private void HomeButtonClick(object sender, RoutedEventArgs e)
        {
            
            Url = "https://www.google.com/";
            Browser.Load(Url);
        }
        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            Url = SearchTextBox.Text;
            //check whether a string is a valid HTTP URL
            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
            {
                Browser.Load(Url);
            }
        }
        private void SearchTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Url = SearchTextBox.Text;
                //check whether a string is a valid HTTP URL
                if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
                {
                    Browser.Load(Url);
                }
            }
        }
        private void GoBackButtonClick(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoBack == true)
            {
                Browser.Back();
            }
        }
        private void GoForwardButtonClick(object sender, RoutedEventArgs e)
        {
            if (Browser.CanGoForward == true)
            {
                Browser.Forward();
            }
        }
        private void ReloadButtonClick(object sender, RoutedEventArgs e)
        {
            Browser.Reload();
        }    
/*        private void ChromiumWebBrowser_OnFrameLoadEnd(object sender, FrameLoadEndEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                Url = e.Url;
            //check whether a string is a valid HTTP URL
            if (Uri.IsWellFormedUriString(Url, UriKind.Absolute))
                {
                    SearchTextBox.Text = Browser.Address;
                    //WebPages.Add(Browser.Address); Current++;
                   if (Browser.IsLoaded)
                    AddWebPage(Browser.Address);
                }
            }));
        }*/
        private void Browser_AddressChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            Url = e.NewValue.ToString();
            SearchTextBox.Text = Url;
            WebPages.Add(Url);
            Current++;

            MenuItem item = new MenuItem();
            item.Click += MenuClicked;
            item.Header = Url;
            item.Width = 300;
            item.Background = Brushes.Transparent;
            item.FontSize = 15;
            item.Foreground = Brushes.White;
            
            item.Icon = WebPages.Count;

            //item.Template = FindResource("ItemTemplate") as ControlTemplate;

            //Menu.Items.Add(item);
            BurgerButton.ContextMenu = contextmenu;
            BurgerButton.ContextMenu = contextmenu;
            contextmenu.Items.Add(item);
        }
        private void MenuClicked(object sender, RoutedEventArgs e)
        {
            //load page from browsing history
            MenuItem item = (MenuItem)sender;
            //Browser.Load(item.Header.ToString());
            Clipboard.SetText(item.Header.ToString());
        }
        private void BurgerClicked(object sender, RoutedEventArgs e)
        {
            if (WebPages.Count != 0)
            {
                contextmenu.Background = Brushes.Black;
                //ScrollViewer scroll = new ScrollViewer();
                
                contextmenu.PlacementTarget = BurgerButton;
                contextmenu.Placement=System.Windows.Controls.Primitives.PlacementMode.Bottom;
               
                contextmenu.HorizontalOffset = 0;
                contextmenu.VerticalOffset = 0;
                contextmenu.Height = 150;
                contextmenu.Width = 250;
                contextmenu.IsOpen = true;
                
            }
        }
        private void Burger_MouseRightButtonUp(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
