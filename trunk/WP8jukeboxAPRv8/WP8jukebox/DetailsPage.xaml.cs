using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using WP8jukebox.ViewModels;

namespace WP8jukebox
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        public string getVenue = "";
        public static string venueBox { get; set; }
        public string venue = "";

        //new viewmodel property to display page by vote order
        public ItemViewModel tr;

        //get real index of model
        string getreal = "";
        string fromChart = "";
        string fromAdmin = "";
        string fromEdit = "";
        
        // Constructor
        public DetailsPage()
        {
            InitializeComponent();
        }

        // When page is navigated to set data context to selected item in list
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //hides vote acknowledgement popup 
            Text1.Visibility = Visibility.Collapsed;
            Text3.Visibility = Visibility.Collapsed;
            Text4.Visibility = Visibility.Collapsed;
         
            string selectedIndex = "";
            getVenue = NavigationContext.QueryString["getVenue"];
            venueBox = getVenue;
            textBox1.Text = venueBox;

            NavigationContext.QueryString.TryGetValue("fromChart", out fromChart);
            NavigationContext.QueryString.TryGetValue("fromAdmin", out fromAdmin);
            NavigationContext.QueryString.TryGetValue("fromEdit", out fromEdit);

            if (fromAdmin == "fromAdmin")
            {
                Text1.Visibility = Visibility.Collapsed;
                Text2.Visibility = Visibility.Collapsed;
                Text3.Visibility = Visibility.Visible;
                Text4.Visibility = Visibility.Collapsed;
            }

            if (fromEdit == "fromEdit")
            {
                Text1.Visibility = Visibility.Collapsed;
                Text2.Visibility = Visibility.Collapsed;
                Text4.Visibility = Visibility.Collapsed;
            }  

            //check if navigated to from chart, load Items4
            if (fromChart == "true")
            {
                if (DataContext == null)
                {
                    if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                    {
                        int index = int.Parse(selectedIndex);
                        DataContext = App.ViewModel.Items4[index];
                        getreal = App.ViewModel.Items4[index].RealID;
                    }
                }
            }

            //not from chart load Items3
            if (DataContext == null)
            {
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items3[index];
                    getreal = App.ViewModel.Items3[index].RealID;
                }
            }
        }
        //make the vote
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //msg to acknowledge vote
            Text2.Visibility = Visibility.Collapsed;

            //msg to acknowledge vote
            Text1.Visibility = Visibility.Visible;

            App.ViewModel = null;

            ItemViewModel tr = (ItemViewModel)this.DataContext;

            //get the values to be sent to db to facilitate update PUT to db
            int id = Convert.ToInt32(getreal);
            string title = tr.LineOne;
            string artist = tr.LineTwo;
            string genre = tr.LineThree;
            int venuevote = Convert.ToInt32(tr.LineFour);
            int popbar = Convert.ToInt32(tr.LineSix);
            int partyclub = Convert.ToInt32(tr.LineSeven);
            int rockbar = Convert.ToInt32(tr.LineEight);
            int danceclub = Convert.ToInt32(tr.LineNine);
                       
            venue = getVenue.Replace(" ", string.Empty);

            if (venue == "PopBar")
            {
                popbar++;
            }
            if (venue == "PartyClub")
            {
                partyclub++;
            }
            if (venue == "RockBar")
            {
                rockbar++;
            }
            if (venue == "DanceClub")
            {
                danceclub++;
            }
                                 
            //increment the displayed vote number
            tr.LineFour++;

            // base URL for API Controller i.e. RESTFul service
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://jukebox.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/jukeapi");

            //getreal sets the db id row to the correct value
            Track newListing = new Track { TrackID = getreal, Title = title, Artist = artist, Genre = genre, PopBar = popbar.ToString(), PartyClub = partyclub.ToString(), RockBar = rockbar.ToString(), DanceClub = danceclub.ToString()};

            // update by Put to /api/ujukeapi a listing serialised in request body
            //the +id is added to the url to address the correct row in the db
            response = await client.PutAsJsonAsync("api/jukeapi/" + id, newListing);

            //if PUT fails
            if (!response.IsSuccessStatusCode)
            {
                //TODO
            }

            //navigated to from chart page , then navigate back to that page
            if (fromChart == "true")
            {
                NavigationService.Navigate(new Uri("/ChartPage.xaml" + "?getVenue=" + getVenue + "&fromChart=true" + "&fromDetails=true" + "&fromPlaylist=true" + "&venue" + venue, UriKind.Relative));

                //delay the page navigation so user can see vote acknowledgement                      
                Thread.Sleep(1000);
            }
            else
            {
                //else back to playlist page            
                NavigationService.Navigate(new Uri("/PlaylistPage.xaml" + "?getVenue=" + getVenue + "&fromDetails=true" + "&fromPlaylist=true" + "&venue" + venue, UriKind.Relative));

                //delay the page navigation so user can see vote acknowledgement                      
                Thread.Sleep(1000);
             }
        }

        //Delete a Track
        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Text3.Visibility = Visibility.Collapsed;
            Text4.Visibility = Visibility.Visible;

            App.ViewModel = null;

            ItemViewModel tr = (ItemViewModel)this.DataContext;

            //get the values to be sent to db to facilitate update PUT to db
            int id = Convert.ToInt32(getreal);
            
            // base URL for API Controller i.e. RESTFul service
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://jukebox.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/jukeapi");

            //delete works
            response = await client.DeleteAsync("api/jukeapi/" + id);
            
            //if PUT fails
            if (!response.IsSuccessStatusCode)
            {
                //TODO
            }
                          
            //back to playlist page            
            NavigationService.Navigate(new Uri("/AdminPage.xaml" + "?getVenue=" + getVenue + "&fromDetails=true" + "&fromPlaylist=true" + "&venue" + venue, UriKind.Relative));

            //delay the page navigation so user can see vote acknowledgement                      
            Thread.Sleep(1000);
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}