using System;
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
        public static string venueBox { get; set; }
                               
        public PlaylistPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
            {
                getVenue = NavigationContext.QueryString["getVenue"];
                venueBox = getVenue;
                textBox1.Text = venueBox;
            }
            
            if (NavigationContext.QueryString.ContainsKey("getVenue"))
            {
                getVenue = NavigationContext.QueryString["getVenue"];
                venueBox = getVenue;
                textBox1.Text = venueBox;
            }
      
            //App.ViewModel = null;
            if (!App.ViewModel.IsDataLoaded)
            {
               App.ViewModel.LoadPlaylistData();
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID+"&getVenue="+getVenue+ "&fromPlaylist=true", UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }
    }
}