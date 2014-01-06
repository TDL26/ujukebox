using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DataBoundApplatesunday.Resources;
using Microsoft.WindowsAzure.MobileServices;
using DataBoundApplatesunday.ViewModels;

namespace DataBoundApplatesunday
{
    public class Tracks
    {
        //[Required(ErrorMessage = "Sugar level recording must not be Blank")]
        //[Range(1, 40, ErrorMessage = "Sugar level must be between 1 to 40")]
        //as a string the id works for the list
        public string Id { get; set; }

        //[JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        //[JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        //[JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        public int Votes { get; set; }

        //public ICollection<Tracks> tracks { get; set; }
    }
    public partial class DetailsPage : PhoneApplicationPage
    {
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items[index];

                }

             }
            
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
        "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            

            //ItemViewModel newTrack = new Tracks();
            Tracks tracks = new Tracks();

            //int votes = tracks.Votes;
            //votes++;


                await client.GetTable<Tracks>().UpdateAsync(tracks);       
  
            //await client.GetTable<Tracks>().Where(x => x.Votes == selectedIndex).ToListAsync();

            //await client.GetTable<Tracks>().OrderBy(x => x.Votes).ToListAsync();
            //await client.GetTable<Tracks>().InsertAsync(tracks);
            //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            //OnNavigatedTo(NavigationEventArgs e);
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}

        public int selectedIndex { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}