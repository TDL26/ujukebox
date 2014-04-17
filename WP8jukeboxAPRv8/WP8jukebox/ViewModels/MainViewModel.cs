﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using WP8jukebox.Resources;


namespace WP8jukebox.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            this.Items3 = new ObservableCollection<ItemViewModel>();
            this.Items4 = new ObservableCollection<ItemViewModel>();
            this.Items4 = new ObservableCollection<ItemViewModel>();
        }

        /// A collection for ItemViewModel objects.
        public ObservableCollection<ItemViewModel> Items { get; private set; }   //venue
        public ObservableCollection<ItemViewModel> Items3 { get; private set; }  //playlist
        public ObservableCollection<ItemViewModel> Items4 { get; private set; }  //chart
        public ObservableCollection<ItemViewModel> Items5 { get; private set; }  //choice

        private string _sampleProperty = "Sample Runtime Property Value";
   
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        public string SampleProperty
        {
            get
            {
                return _sampleProperty;
            }
            set
            {
                if (value != _sampleProperty)
                {
                    _sampleProperty = value;
                    NotifyPropertyChanged("SampleProperty");
                }
            }
        }

        /// Sample property that returns a localized string
        public string LocalizedSampleProperty
        {
            get
            {
                return AppResources.SampleProperty;
            }
        }

        public bool IsDataLoaded
        {
            get;
            private set;
        }

        /// Creates and adds a few ItemViewModel objects into the Items collection.
        public async void LoadVenueData()
        {
           HttpClient client = new HttpClient();

           // base URL for API Controller i.e. RESTFul service
           client.BaseAddress = new Uri("http://jukebox.azurewebsites.net/");       

           // add an Accept header for JSON
           client.DefaultRequestHeaders.
           Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           HttpResponseMessage response = await client.GetAsync("api/venueapi");
                
           // read result     
           var lists = await response.Content.ReadAsAsync<IEnumerable<string>>();

           //get the incoming id - ie the db id
           string getId = "";
           
           //set the new id for the list view to 0 
           int newID = 0;
           
            foreach (var listing in lists)
            {
              getId = listing.ToString();
              var ID = newID;
              var lineone = listing.ToString();
              var linetwo = listing.ToString();

              //Real to pass the realid 
              this.Items.Add(new ItemViewModel() { RealID = getId, ID = newID.ToString(), LineOne = lineone, LineTwo = linetwo });

              //newid is used to set ID to 0 - the index of the item in the displayed list
              newID++;
                 
            }

            this.IsDataLoaded = true;
        }

        public async void LoadPlaylistData()
        {
            HttpClient client = new HttpClient();

            // base URL for API Controller i.e. RESTFul service
            client.BaseAddress = new Uri("http://jukebox.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/playlistapi/");
                                  
            var lists3 = await response.Content.ReadAsAsync<IEnumerable<Track>>();

            //////////////////var li = lists3.OrderBy(c => c.Title);
 
            //// to hold real item id from db
            string getId = "";
            int newID3 = 0;
            int position = 1;

            foreach (var listing in lists3)
            {
                               
                //get the original id and save to getid
                //getId = listing.ID;
                getId = listing.TrackID.ToString();
                var ID = newID3;
                var lineone = listing.Title.ToString();
                var linetwo = listing.Artist.ToString();
                var linethree = listing.Genre.ToString();
                int linefour = 0;
                int linesix = Int32.Parse(listing.PopBar);
                int lineseven = Int32.Parse(listing.PartyClub);
                int lineeight = Int32.Parse(listing.RockBar);
                int linenine = Int32.Parse(listing.DanceClub);
                int lineten = Int32.Parse(listing.AlternativeBar);
                int lineeleven = Int32.Parse(listing.PopClub);
                int linetwelve = Int32.Parse(listing.RnbClub);
               // int venuevote = 0;

                string thevenue = AVenue.TheVenue;

                if (thevenue == "PopBar")
                {
                    linefour = linesix;
                }
                if (thevenue == "PartyClub")
                {
                    linefour = lineseven;
                }
                if (thevenue == "RockBar")
                {
                    linefour = lineeight;
                }

                if (thevenue == "DanceClub")
                {
                    linefour = linenine;
                }
                if (thevenue == "AlternativeBar")
                {
                    linefour = lineten;
                }
                if (thevenue == "PopClub")
                {
                    linefour = lineeleven;
                }
                if (thevenue == "RnbClub")
                {
                    linefour = linetwelve;
                }



                if (linefour > 0)
                {
                    //Real to pass the realid 
                    this.Items3.Add(new ItemViewModel() { RealID = getId, ID = newID3.ToString(), LineOne = lineone, LineTwo = linetwo, LineThree = linethree, LineFour = linefour, LineFive = position.ToString(), LineSix = linesix, LineSeven = lineseven, LineEight = lineeight, LineNine = linenine, LineTen = lineten, LineEleven = lineeleven, LineTwelve = linetwelve });
                    position++;
                    //newid is used to set ID to 0 - the index of the item in the displayed list
                    newID3++;
                   
                }
                
            }
          
            this.IsDataLoaded = true;
        }

        public async void LoadChartData()
        {
            HttpClient client = new HttpClient();

            // base URL for API Controller i.e. RESTFul service
            client.BaseAddress = new Uri("http://jukebox.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/chartapi/");

            var lists4 = await response.Content.ReadAsAsync<IEnumerable<Track>>();
            var li = lists4;

            string thevenue = AVenue.TheVenue;

            if (thevenue == "PopBar")
            {
                li = lists4.OrderByDescending(c => c.PopBar);
            }
            else if (thevenue == "PartyClub")
            {
                li = lists4.OrderByDescending(c => c.PartyClub);
            }
            else if (thevenue == "RockBar")
            {
                li = lists4.OrderByDescending(c => c.RockBar);
            }
            else 
            {
                li = lists4.OrderByDescending(c => c.DanceClub);
            }

                      
            //// to hold real item id from db
            string getId = "";
            int newID4 = 0;
            int position = 1;

            foreach (var listing in li)
            {
                getId = listing.TrackID.ToString();
                var ID = newID4;
                var lineone = listing.Title.ToString();
                var linetwo = listing.Artist.ToString();
                var linethree = listing.Genre.ToString();
                int linefour = listing.Vote;
                int linesix = Int32.Parse(listing.PopBar);
                int lineseven = Int32.Parse(listing.PartyClub);
                int lineeight = Int32.Parse(listing.RockBar);
                int linenine = Int32.Parse(listing.DanceClub);
                int lineten = Int32.Parse(listing.AlternativeBar);
                int lineeleven = Int32.Parse(listing.PopClub);
                int linetwelve = Int32.Parse(listing.RnbClub);

                thevenue = AVenue.TheVenue;

                if (thevenue == "PopBar")
                {
                    linefour = linesix;
                }
                if (thevenue == "PartyClub")
                {
                    linefour = lineseven;
                }
                if (thevenue == "RockBar")
                {
                    linefour = lineeight;
                }

                if (thevenue == "DanceClub")
                {
                    linefour = linenine;
                }
                if (thevenue == "AlternativeBar")
                {
                    linefour = lineten;
                }
                if (thevenue == "PopClub")
                {
                    linefour = lineeleven;
                }
                if (thevenue == "RnbClub")
                {
                    linefour = linetwelve;
                }

                if (linefour > 0)
                {
                    //Real to pass the realid 
                    this.Items4.Add(new ItemViewModel() { RealID = getId, ID = newID4.ToString(), LineOne = lineone, LineTwo = linetwo, LineThree = linethree, LineFour = linefour, LineFive = position.ToString(), LineSix = linesix, LineSeven = lineseven, LineEight = lineeight, LineNine = linenine, LineTen = lineten, LineEleven = lineeleven, LineTwelve = linetwelve });
                    position++;

                    //newid is used to set ID to 0 - the index of the item in the displayed list
                    newID4++;
                }
            }
         
            this.IsDataLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}