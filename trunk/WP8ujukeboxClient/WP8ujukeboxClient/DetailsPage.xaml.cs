﻿using System;
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
using WP8ujukeboxClient.Resources;
using WP8ujukeboxClient.ViewModels;

namespace WP8ujukeboxClient
{
    public partial class DetailsPage : PhoneApplicationPage
    {
        //new viewmodel property to display page by vote order
        public ItemViewModel tr;

        //get real index of model
        string getreal = "";

        //will be set to true if navigated to from chart page
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
            //hides vote acknowledgement popup 
            Text1.Visibility = Visibility.Collapsed;

            string selectedIndex = "";

            if (NavigationContext.QueryString.TryGetValue("fromChart", out fromChart))
            {
                //check to see if navigated from chart page
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items2[index];
                    getreal = App.ViewModel.Items2[index].RealID;
                }
            }
            else
            {
                //navigated from playlist page
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items[index];
                    getreal = App.ViewModel.Items[index].RealID;
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
            int id = Convert.ToInt32(getreal);
            string title = tr.LineOne;
            string artist = tr.LineTwo;
            string genre = tr.LineThree;
            int vote = Convert.ToInt32(tr.LineFour);

            //increment the vote number
            vote++;
            //increment the displayed vote number
            tr.LineFour++;

            // base URL for API Controller i.e. RESTFul service
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://ujukebox.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/ujukeapi");

            //getreal sets the db id row to the correct value
            Track newListing = new Track { ID = getreal, Title = title, Artist = artist, Genre = genre, Vote = vote };

            // update by Put to /api/ujukeapi a listing serialised in request body
            //the +id is added to the url to address the correct row in the db
            response = await client.PutAsJsonAsync("api/ujukeapi/" + id, newListing);

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
                NavigationService.Navigate(new Uri("/ChartsPage.xaml", UriKind.Relative));
            }
            //else back to playlist page            
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}