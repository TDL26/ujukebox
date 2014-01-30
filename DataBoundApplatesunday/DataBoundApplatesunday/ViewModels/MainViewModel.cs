using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using DataBoundApplatesunday.Resources;
using Microsoft.WindowsAzure.MobileServices;

namespace DataBoundApplatesunday.ViewModels
{
    public class Track
    {
        //[Required(ErrorMessage = "Sugar level recording must not be Blank")]
        //[Range(1, 40, ErrorMessage = "Sugar level must be between 1 to 40")]
        //as a string the id works for the list

        public string Real { get; set; }
        public string Id { get; set; }

        //[JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        //[JsonProperty(PropertyName = "artist")]
        public string Artist { get; set; }

        //[JsonProperty(PropertyName = "genre")]
        public string Genre { get; set; }

        public int Vote { get; set; }

        //public ICollection<Tracks> tracks { get; set; }
    }
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
                client.BaseAddress = new Uri("http://ujukebox.azurewebsites.net/");                             // base URL for API Controller i.e. RESTFul service

                // add an Accept header for JSON
                client.DefaultRequestHeaders.
                    Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync("api/ujukeapi");
                // read result 
                //String output = "";
                
                var lists = await response.Content.ReadAsAsync<IEnumerable<Track>>();
                IEnumerable<Track> listings = lists.OrderBy(list => list.Title);

                int newid = 0;
                string getId = "";
            foreach (var listing in listings)
            {
                getId = listing.Id;

                if (listing.Title == null)
                {
                    listing.Title = "Empty";
                }
                if (listing.Artist == null)
                {
                    listing.Artist = "Empty";
                }
                if (listing.Genre == null)
                {
                    listing.Genre = "Empty";
                }
                
                this.Items.Add(new ItemViewModel() { Real = getId, ID = newid.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Vote });
               
                newid++;

            }

            IEnumerable<Track> listings2 = lists.OrderByDescending(list => list.Vote);
            int position = 1;
            int newid2 = 0;
            string getId2 = "";
            foreach (var listing in listings2)
            {
                getId2 = listing.Id;

                this.Items2.Add(new ItemViewModel() { Real = getId2, ID = newid2.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Vote, LineFive = position });
                position++;
                newid2++;
                

            }
                   
            this.IsDataLoaded = true;
        }


        public async void LoadDataVotes()
        {

            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
           "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            //get all the tracks
            List<Track> allTracks = await client.GetTable<Track>().OrderBy(x => x.Vote).ToListAsync();

            
            

         

            int newId = 1;
            foreach (var listing in allTracks)
            {

                if (listing.Id == null)
                {
                    listing.Id = "Empty";
                }
                if (listing.Title == null)
                {
                    listing.Title = "Empty";
                }
                if (listing.Artist == null)
                {
                    listing.Artist = "Empty";
                }
                if (listing.Genre == null)
                {
                    listing.Genre = "Empty";
                }
                //if (listing.Vote == null)
                //{
                //    listing.Vote = 0;
                //}
               
                
                //this.Items.Add(new ItemViewModel() { ID = newId.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Votes });
                
                newId++;
            }
            //List<all>all = allTracks.Sort.

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
