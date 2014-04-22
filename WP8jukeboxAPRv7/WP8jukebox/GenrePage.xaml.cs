﻿using System;
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
        //store the venue name
        string getVenue = "";
       
        // setter for text box
        public static string venueBox { get; set; }

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
            //check for back button press to by pass reload of model
            base.OnNavigatedTo(e);
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
            { 
            }
            else
            {
               //get the id from the passed selected item in the list
               string selectedIndex = "";
               //navigated from playlist page
                  if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                  {
                      //get the passed in venue choice from index
                      int index = int.Parse(selectedIndex);
                      getVenue = App.ViewModel.Items[index].LineOne;
                      venueBox = getVenue;
                      textBox1.Text = venueBox;
                  }
             }
                  
            //force a reload of model with correct votes behind
            App.ViewModel = null;
            if (!App.ViewModel.IsDataLoaded)
            {
                //App.ViewModel.LoadGenreData();
            }          
        }

        private void setDataContext()
        {
            ContentPanel.DataContext = getVenue;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // button click navigates to playlist page and forwards getVenue
            NavigationService.Navigate(new Uri("/PlaylistPage.xaml?getVenue=" + getVenue, UriKind.Relative));
       
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
           // button click navigates to chart page and forwards getVenue
            NavigationService.Navigate(new Uri("/ChartPage.xaml?getVenue=" + getVenue, UriKind.Relative));
        }
     }
}