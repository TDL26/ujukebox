using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using WP8jukebox.Resources;
using System.Linq;
using System.Net;


namespace WP8jukebox.ViewModels
{

    public class MainViewModel : INotifyPropertyChanged
    {


        // URI for RESTful service (implemented using Web API)
        //private const String serviceURI = "ttp://ujukebox.azurewebsites.net/";

        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            this.Items2 = new ObservableCollection<ItemViewModel>();
            this.Items3 = new ObservableCollection<ItemViewModel>();
            this.Items4 = new ObservableCollection<ItemViewModel>();
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }   //venue
        public ObservableCollection<ItemViewModel> Items2 { get; private set; } //genre
        public ObservableCollection<ItemViewModel> Items3 { get; private set; }  //playlist
        public ObservableCollection<ItemViewModel> Items4 { get; private set; }  //chart
        
        private string _sampleProperty = "Sample Runtime Property Value";
        /// <summary>
        /// Sample ViewModel property; this property is used in the view to display its value using a Binding
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Sample property that returns a localized string
        /// </summary>
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

        /// <summary>
        /// Creates and adds a few ItemViewModel objects into the Items collection.
        /// </summary>
        public async void LoadVenueData()
        {
           HttpClient client = new HttpClient();

                // base URL for API Controller i.e. RESTFul service
           client.BaseAddress = new Uri("http://ujuke.azurewebsites.net/");       

                // add an Accept header for JSON
           client.DefaultRequestHeaders.
           Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

           HttpResponseMessage response = await client.GetAsync("api/venueapi");
                
                // read result     
                var lists = await response.Content.ReadAsAsync<IEnumerable<string>>();

           
           // IEnumerable<Venue> listings = lists.OrderBy(list => lists.venueName);
           //                // index id for list of items               //int newid = 0;

           //// to hold real item id from db
                 string getId = "";
                int newID = 0;
                foreach (var listing in lists)
                {
                    //get the original id and save to getid
                    //getId = listing.ID;
                    getId = listing.ToString();
                    var ID = newID;
                    var lineone = listing.ToString();
                    var linetwo = listing.ToString();

                    //Real to pass the realid 
                    this.Items.Add(new ItemViewModel() { RealID = getId, ID = newID.ToString(), LineOne = lineone, LineTwo = linetwo });

                    //newid is used to set ID to 0 - the index of the item in the displayed list
                    newID++;
                    
                }
                LoadGenreData();
            this.IsDataLoaded = true;
        }

        public async void LoadGenreData()
        {
            HttpClient client = new HttpClient();

            // base URL for API Controller i.e. RESTFul service
            client.BaseAddress = new Uri("http://ujuke.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/genreapi");

            // read result     
            var lists2 = await response.Content.ReadAsAsync<IEnumerable<string>>();


            // IEnumerable<Venue> listings = lists.OrderBy(list => lists.venueName);
            //                // index id for list of items               //int newid = 0;

            //// to hold real item id from db
            string getId = "";
            int newID2 = 0;
            foreach (var listing in lists2)
            {
                //get the original id and save to getid
                getId = listing.ToString();
                var ID = newID2;
                var lineone = listing.ToString();
                var linetwo = listing.ToString();
                var linethree = listing.ToString();
                var linefour = listing.ToString();


                //Real to pass the realid 
                this.Items2.Add(new ItemViewModel() { RealID = getId, ID = newID2.ToString(), LineOne = lineone, LineTwo = linetwo, LineThree = linethree });

                //newid is used to set ID to 0 - the index of the item in the displayed list
                newID2++;
            }
            LoadPlaylistData();
            this.IsDataLoaded = true;
        }

        public async void LoadPlaylistData()
        {
            HttpClient client = new HttpClient();

            // base URL for API Controller i.e. RESTFul service
            client.BaseAddress = new Uri("http://ujukebox.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/ujukeapi/");

                                   
            var lists = await response.Content.ReadAsAsync<IEnumerable<Track>>();


            // IEnumerable<Venue> listings = lists.OrderBy(list => lists.venueName);
            //                // index id for list of items               //int newid = 0;

            //// to hold real item id from db
            string getId = "";
            int newID = 0;
            int position = 1;

            foreach (var listing in lists)
            {
                //get the original id and save to getid
                //getId = listing.ID;
                getId = listing.ID.ToString();
                var ID = newID;
                var lineone = listing.Title.ToString();
                var linetwo = listing.Artist.ToString();
                var linethree = listing.Genre.ToString();
                int linefour = listing.Vote;
               
                
               

                //Real to pass the realid 
                this.Items3.Add(new ItemViewModel() { RealID = getId, ID = newID.ToString(), LineOne = lineone, LineTwo = linetwo, LineThree = linethree, LineFour = linefour, LineFive = position.ToString() });
                position++;
                //newid is used to set ID to 0 - the index of the item in the displayed list
                newID++;

            }
           // LoadGenreData();
            this.IsDataLoaded = true;
        }

        public async void LoadChartData()
        {
            HttpClient client = new HttpClient();

            // base URL for API Controller i.e. RESTFul service
            client.BaseAddress = new Uri("http://ujukebox.azurewebsites.net/");

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
            Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/ujukeapi/");


            var lists = await response.Content.ReadAsAsync<IEnumerable<Track>>();


            // IEnumerable<Venue> listings = lists.OrderBy(list => lists.venueName);
            //                // index id for list of items               //int newid = 0;

            //// to hold real item id from db
            string getId = "";
            int newID = 0;
            int position = 1;

            foreach (var listing in lists)
            {
                //get the original id and save to getid
                //getId = listing.ID;
                getId = listing.ToString();
                var ID = newID;
                var lineone = listing.Title.ToString();
                var linetwo = listing.Artist.ToString();
                var linethree = listing.Genre.ToString();
                int linefour = listing.Vote;

                //Real to pass the realid 
                this.Items4.Add(new ItemViewModel() { RealID = getId, ID = newID.ToString(), LineOne = lineone, LineTwo = linetwo, LineThree = linethree, LineFour = linefour, LineFive = position.ToString() });
                position++;
                //newid is used to set ID to 0 - the index of the item in the displayed list
                newID++;

            }
            // LoadGenreData();
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