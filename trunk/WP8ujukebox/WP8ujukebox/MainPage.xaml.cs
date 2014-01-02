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


namespace WP8ujukebox
{
    public class Tracks
    {
        //[Required(ErrorMessage = "Sugar level recording must not be Blank")]
        //[Range(1, 40, ErrorMessage = "Sugar level must be between 1 to 40")]
        public int Id { get; set; }

        //[JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        //[JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        //[JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        public ICollection<Tracks> tracks { get; set; }

    }
    public partial class MainPage : PhoneApplicationPage
    {

        private const String serviceURI = "http://ujukebox.azurewebsites.net/api/ujukeapi";
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri("http://ujukebox.azurewebsites.net/");                             // base URL for API Controller i.e. RESTFul service

                // add an Accept header for JSON
                client.DefaultRequestHeaders.
                    Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // GET ../api/stock
                // get all stock listings asynchronously - await the result (i.e. block and return control to caller)
                HttpResponseMessage response = await client.GetAsync("api/ujukeapi");

                // continue
                if (response.IsSuccessStatusCode)                                                   // 200.299
                {
                    // read result 
                    //String output = "";
                    var listings = await response.Content.ReadAsAsync<IEnumerable<Tracks>>();
                    foreach (var listing in listings)
                    {

                        string t = listing.Title;
                        string a = listing.Artist;
                        
                        lstReading.Items.Add(t);
                        lstReading_Copy.Items.Add(a);
                        //Text1.Text= listing.Title + " " ; 
                        //Text2.Text= listing.Artist + " " ; 
                        //output += listing.Title + " " + listing.Artist;
                         //= output.Ti;
                    }

                    // display on UI
                    
                }
                //else
                //{
                //    displayTextBlock.Text = response.StatusCode + " " + response.ReasonPhrase;
                //}
            }


            catch (Exception e1)
            {
                //displayTextBlock.Text = e1.ToString();
            }

           
      
         }

        private void lstReading_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void lstReading_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(
            new Uri("/AddRead.xaml", UriKind.Relative));

        }
        

               
    
    }
}