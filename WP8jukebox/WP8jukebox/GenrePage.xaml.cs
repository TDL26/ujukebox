using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8jukebox.ViewModels;

namespace WP8jukebox
{
    public partial class GenrePage : PhoneApplicationPage
    {
        string getVenue = "";
        string getGenre = "";
        string fromChart = "";
        //string getName = "";
          // Constructor
        public GenrePage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            if (NavigationContext.QueryString.TryGetValue("fromChart", out fromChart))
            {
                getVenue = NavigationContext.QueryString["getVenue"];
                getGenre = NavigationContext.QueryString["getGenre"];

                //NavigationService.Navigate(new Uri("/ChartPage.xaml" + "&getVenue=" + getVenue + "&getGenre=" + getGenre, UriKind.Relative));
                //NavigationService.Navigate(new Uri("/ChartPage.xaml" + "&getVenue=" + getVenue + "&getGenre=" + getGenre, UriKind.Relative));
                //NavigationService.Navigate(new Uri("/ChartPage.xaml", UriKind.Relative));
                NavigationService.Navigate(new Uri("/ChartPage.xaml" + "?getVenue=" + getVenue + "&getGenre=" + getGenre, UriKind.Relative));
               // NavigationService.Navigate(new Uri("/ChartPage.xaml" + "?getVenue=" + getVenue + "&getGenre=" + getGenre, UriKind.Relative));

            
           
            
            }
            else
            {
                string selectedIndex = "";
                //navigated from playlist page
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    
                    int index = int.Parse(selectedIndex);
                    //DataContext = App.ViewModel.Items[index];
                    getVenue = App.ViewModel.Items[index].LineOne;

                }

           
            
                App.ViewModel = null;
                if (!App.ViewModel.IsDataLoaded)
                {
                    //DataContext = App.ViewModel.Items2;
                    App.ViewModel.LoadGenreData();
                }

            }
            
           
        }

       



        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

           // VenueInfo VenueInfo = new VenueInfo();
            //VenueInfo.NameInfo = getName;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/PlaylistPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID + "&getVenue=" + getVenue, UriKind.Relative));
            //NavigationService.Navigate(new Uri("/PlaylistPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));
            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
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
    }
}