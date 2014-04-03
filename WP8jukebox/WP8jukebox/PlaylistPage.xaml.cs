﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8jukebox.ViewModels;

namespace WP8jukebox
{
    public partial class PlaylistPage : PhoneApplicationPage
    {
        string getVenue = "";
        string getGenre = "";

        public PlaylistPage()
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
           

            if (NavigationContext.QueryString.ContainsKey("getVenue"))
            {
                //string getName = NavigationContext.QueryString["getName"];
                // etc ...
            }

            string selectedIndex = "";

            //navigated from playlist page
            if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
            {
                int index = int.Parse(selectedIndex);
                //DataContext = App.ViewModel.Items[index];
                getGenre = App.ViewModel.Items2[index].LineOne;
                getVenue = NavigationContext.QueryString["getVenue"];

            }

           
            App.ViewModel = null;
            if (!App.ViewModel.IsDataLoaded)
            {
                //DataContext = App.ViewModel.Items2;
                App.ViewModel.LoadGenreData();
            }



        }





        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            VenueInfo VenueInfo = new VenueInfo();
            VenueInfo.NameInfo = getVenue;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID+"&getVenue="+getVenue+"&getGenre="+getGenre, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GenrePage.xaml" + "?fromChart=true" + "&getVenue=" + getVenue + "&getGenre=" + getGenre, UriKind.Relative));

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