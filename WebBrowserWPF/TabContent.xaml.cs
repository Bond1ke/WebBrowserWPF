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
/*        MenuItem historyButton = new MenuItem();

        MenuItem bookmarkButton = new MenuItem();*/
        //public static 
        public TabContent()
        {
            InitializeComponent();

/*            historyButton.Click += HistoryClicked;
            historyButton.Header = "History";
            Menu.Items.Add(historyButton);

            bookmarkButton.Click += BookmarksClicked;
            bookmarkButton.Header = "Bookmarks";
            Menu.Items.Add(bookmarkButton);*/
        }
        static TabContent()
        {
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
        private void BookmarkButtonClicked(object sender, RoutedEventArgs e)
        {
            //BookmarkButton.Content = FindResource("Warning");
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
            item.FontSize = 15;
            item.Icon = WebPages.Count;
            //item.Background = Brushes.Transparent;
            //item.Foreground = Brushes.White;
            //item.Template = FindResource("ItemTemplate") as ControlTemplate;

            //Menu.Items.Add(item);
            contextmenu.Items.Add(item);
            BurgerButton.ContextMenu = contextmenu;
            //historyButton.Items.Add(item);
            //historyButton.Items.Clear();
            /*int count = 0;
            while(count < Current)
            {
                MenuItem item1 = new MenuItem();
                item1.Click += MenuClicked;
                item1.Header = Url;
                item1.Width = 300;
                item1.FontSize = 15;
                item1.Icon = WebPages.Count;
                historyButton.Items.Add(item1);
                count++;
            }*/

            /*historyButton.Click += HistoryClicked;
            historyButton.Header = "History";*/
            //Menu.Items.Add(historyButton);
            //Menu.Items.SortDescriptions.Clear();
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
            /*Menu.PlacementTarget = BurgerButton;
            Menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            Menu.HorizontalOffset = 0;
            Menu.IsOpen = true;*/

            contextmenu.PlacementTarget = BurgerButton;
            contextmenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;

            contextmenu.HorizontalOffset = 0;
            contextmenu.VerticalOffset = 0;
            contextmenu.Height = 150;
            contextmenu.Width = 250;
            contextmenu.IsOpen = true;
        }
        private void HistoryClicked(object sender, RoutedEventArgs e)
        {
            /*if (WebPages.Count != 0)
            {

                Menu.PlacementTarget = BurgerButton;
                Menu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
                Menu.HorizontalOffset = 0;
                Menu.IsOpen = true;
            }*/
            MenuItem item = (MenuItem)sender;
            Browser.Load(item.Header.ToString());
        }
        private void BookmarksClicked(object sender, RoutedEventArgs e)
        {
            /*            if (WebPages.Count != 0)
                        {
                            Menu.PlacementTarget = BurgerButton;
                            Menu.Placement=System.Windows.Controls.Primitives.PlacementMode.Bottom;
                            Menu.HorizontalOffset = 0;
                            Menu.IsOpen = true;
                        }
            if (WebPages.Count != 0)
            {
                //contextmenu.Background = Brushes.Black;
                //ScrollViewer scroll = new ScrollViewer();

                contextmenu.PlacementTarget = BurgerButton;
                contextmenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;

                contextmenu.HorizontalOffset = 0;
                contextmenu.VerticalOffset = 0;
                contextmenu.Height = 150;
                contextmenu.Width = 250;
                contextmenu.IsOpen = true;

            }
             
             */

        }
        private void Burger_MouseRightButtonUp(object sender, RoutedEventArgs e)
        {
            e.Handled = true;
        }
    }
}
