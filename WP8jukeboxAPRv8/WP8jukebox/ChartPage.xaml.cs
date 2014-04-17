﻿using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WP8jukebox.ViewModels;

namespace WP8jukebox
{
    public partial class ChartPage : PhoneApplicationPage
    {
         string getVenue = "";
         public static string venueBox { get; set; }
        
        public ChartPage()
        {
            InitializeComponent();
                       
            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            getVenue = NavigationContext.QueryString["getVenue"];
           
            venueBox = getVenue;
            textBox1.Text = venueBox;
            
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadChartData();
            }
        }
        
        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID+"&getVenue="+getVenue+"&fromChart=true", UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }
    }
}