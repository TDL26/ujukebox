using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Newtonsoft.Json;
using WP8ujukebox.Resources;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.WindowsAzure.MobileServices;


namespace WP8ujukebox
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
    public partial class MainPage : PhoneApplicationPage
    {
        MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
        "WzaesYtewHSUagMdcYPiBnPwhCromc10");

        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
            "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            //get all the tracks
           List<Tracks> allTracks = await client.GetTable<Tracks>().ToListAsync();

            //get all the tracks which artist is DC Comics
           List<Tracks> filteredComics = await client.GetTable<Tracks>().Where(x => x.Genre == "DC Comics").ToListAsync();

           //get all the tracks ordered by title
           List<Tracks> orderedComics = await client.GetTable<Tracks>().OrderBy(x => x.Title).ToListAsync();

           //TracksList.ItemsSource = allTracks;
           
            foreach (var listing in allTracks)
            {

                string t = listing.Title;
                string a = listing.Artist;

                lstReading.Items.Add(t);

                if (a == null)
                {
                    a = "Empty";
                    lstReading_Copy.Items.Add(a);
                }
                else
	            {
                     lstReading_Copy.Items.Add(a);
	            }
               
            }
        }
                     

            
        private void lstReading_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstReading_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {

            //NavigationService.Navigate(
            //new Uri("/AddRead.xaml", UriKind.Relative));

            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
           "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            Tracks tracks = new Tracks
                {
                    //Id = "",
                    Title = "New",
                    Artist = "Luke",
                    Genre = "PC",
                    Votes = 1
                };

            await client.GetTable<Tracks>().InsertAsync(tracks);
            //OnNavigatedTo(NavigationEventArgs e);
            

        }
    
} 
}