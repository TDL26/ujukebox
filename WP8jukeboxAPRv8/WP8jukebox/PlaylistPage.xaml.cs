using System;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8jukebox.ViewModels;

namespace WP8jukebox
{
    public partial class PlaylistPage : PhoneApplicationPage
    {
        //variable holding
        string getVenue = "";
        string fromAdmin = "";
        string fromEdit = "";
        string fromFull = "";
        public static string venueBox { get; set; }

        public PlaylistPage()
        {
            InitializeComponent();
            App.ViewModel = null;
            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            App.ViewModel = null;
            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;
            
            
            getVenue = NavigationContext.QueryString["getVenue"];
            venueBox = getVenue;
            textBox1.Text = venueBox;

            //new
            //remove spaces from getvenue
            AVenue.TheVenue = getVenue.Replace(" ", string.Empty);

            NavigationContext.QueryString.TryGetValue("fromAdmin", out fromAdmin);
            NavigationContext.QueryString.TryGetValue("fromEdit", out fromEdit);
            NavigationContext.QueryString.TryGetValue("fromFull", out fromFull);

            
            if (!App.ViewModel.IsDataLoaded)
            {
                ProgressIndicator prog = new ProgressIndicator();
                prog.IsVisible = true;
                prog.IsIndeterminate = true;
                prog.Text = "Downloading Data from the Cloud...";
                SystemTray.SetProgressIndicator(this, prog);

                if (fromFull == "fromFull")
                {
                    App.ViewModel.LoadNonPlaylistData();
                    textBox1.Text = "Full Playlist";
                  
                }
                else
                {
                    App.ViewModel.LoadPlaylistData();
                }
               
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            if ((fromEdit == "fromEdit") && (fromFull == null))
            {
                // Navigate to the new page
                NavigationService.Navigate(new Uri("/EditTrack.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID + "&getVenue=" + getVenue + "&fromPlaylist=true" + "&fromAdmin=" + fromAdmin + "&fromEdit=" + fromEdit, UriKind.Relative));
            }

            else if ((fromEdit == "fromEdit") && (fromFull == "fromFull"))
            {

                // Navigate to the new page
                NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID + "&getVenue=" + getVenue + "&fromPlaylist=true" + "&fromAdmin=" + fromAdmin + "&fromEdit=" + fromEdit + "&fromFull=" + fromFull, UriKind.Relative));
                }   
            
            else 
            {
                // Navigate to the new page
                NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID + "&getVenue=" + getVenue + "&fromPlaylist=true" + "&fromAdmin=" + fromAdmin + "&fromEdit=" + fromEdit, UriKind.Relative));
            }
           

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }
    }
}