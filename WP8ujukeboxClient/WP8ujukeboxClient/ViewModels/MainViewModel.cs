using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using WP8ujukeboxClient.Resources;
using System.Linq;

namespace WP8ujukeboxClient.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        // URI for RESTful service (implemented using Web API)
        private const String serviceURI = "http://ujukebox.azurewebsites.net/api/ujukeapi";

        public MainViewModel()
        {
            this.Items = new ObservableCollection<ItemViewModel>();
            this.Items2 = new ObservableCollection<ItemViewModel>();
            
        }

        /// <summary>
        /// A collection for ItemViewModel objects.
        /// </summary>
        public ObservableCollection<ItemViewModel> Items { get; private set; }
        public ObservableCollection<ItemViewModel> Items2 { get; private set; }

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
        public async void LoadData()
        {
                HttpClient client = new HttpClient();

                // base URL for API Controller i.e. RESTFul service
                client.BaseAddress = new Uri("http://ujukebox.azurewebsites.net/");       

                // add an Accept header for JSON
                client.DefaultRequestHeaders.
                Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/ujukeapi");
                
                // read result     
                var lists = await response.Content.ReadAsAsync<IEnumerable<Track>>();
                IEnumerable<Track> listings = lists.OrderBy(list => list.Title);

            //index id for list of items    
            int newid = 0;

            //to hold real item id from db
            string getId = "";

            foreach (var listing in listings)
            {
                //get the original id and save to getid
                getId = listing.ID;
                              
                //Real to pass the realid 
                this.Items.Add(new ItemViewModel() { RealID = getId, ID = newid.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Vote });
                
                //newid is used to set ID to 0 - the index of the item in the displayed list
                newid++;
            }

            //to set a model sorted by vote
            IEnumerable<Track> listings2 = lists.OrderByDescending(list => list.Vote);
            
            //set chart position to start at 1
            int position = 1;
            int newid2 = 0;
            string getId2 = "";
            
            foreach (var listing in listings2)
            {
                getId2 = listing.ID;

                this.Items2.Add(new ItemViewModel() { RealID = getId2, ID = newid2.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Vote, LineFive = position.ToString() });
                position++;
                newid2++;
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