using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8jukebox.Resources;
using WP8jukebox.ViewModels;

namespace WP8jukebox
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        string fromChart = "";
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
            string getVenue = "";
            string getGenre = "";
            string getTrack = "";

            //hides vote acknowledgement popup 
            Text1.Visibility = Visibility.Collapsed;
        


            getGenre = NavigationContext.QueryString["getGenre"];
            getVenue = NavigationContext.QueryString["getVenue"];
            //getVenue = NavigationContext.QueryString["fromChart"];



            if (NavigationContext.QueryString.TryGetValue("fromChart", out fromChart))
            {
            }




            
            if (DataContext == null)
                {
                    string selectedIndex = "";
                    if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                    {
                        int index = int.Parse(selectedIndex);
                        DataContext = App.ViewModel.Items3[index];
                        getTrack = App.ViewModel.Items3[index].LineOne;

                        
                       
                    }
                }
            
        }
        //make the vote
        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //msg to acknowledge vote
            Text1.Visibility = Visibility.Visible;

            ItemViewModel tr = (ItemViewModel)this.DataContext;

            //get the values to be sent to db to facilitate update PUT to db
            //int id = Convert.ToInt32(getreal);
            string title = tr.LineOne;
            string artist = tr.LineTwo;
            string genre = tr.LineThree;
            
           // int vote = Convert.ToInt32(tr.LineFour);

            //increment the vote number
            //vote++;
            //increment the displayed vote number
            //tr.LineFour++;

            // base URL for API Controller i.e. RESTFul service
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://ujuke.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/ujukeapi");

            //getreal sets the db id row to the correct value
            //Track newListing = new Track { ID = getreal, Title = title, Artist = artist, Genre = genre, Vote = vote };

            // update by Put to /api/ujukeapi a listing serialised in request body
            //the +id is added to the url to address the correct row in the db
           // response = await client.PutAsJsonAsync("api/ujukeapi/" + id, newListing);

            //if PUT fails
            if (!response.IsSuccessStatusCode)
            {
                //TODO
                //Uri newStockUri = response.Headers.Location;
                //Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
            }

            //delay the page navigation so user can see vote acknowledgement                      
            Thread.Sleep(1000);

            //force a reload of the model so all pages have correct data
            App.ViewModel = null;

            //navigated to from chart page , then navigate back to that page
            if (fromChart == "true")
            {
                NavigationService.Navigate(new Uri("/ChartPage.xaml", UriKind.Relative));
            }
            else
            {
                //else back to playlist page            
                NavigationService.Navigate(new Uri("/PlaylistPage.xaml", UriKind.Relative));
            }
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}