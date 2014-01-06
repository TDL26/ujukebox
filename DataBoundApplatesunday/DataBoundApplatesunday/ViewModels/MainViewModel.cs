using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using DataBoundApplatesunday.Resources;
using Microsoft.WindowsAzure.MobileServices;

namespace DataBoundApplatesunday.ViewModels
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
    public class MainViewModel : INotifyPropertyChanged
    {
       
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

            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
           "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            //get all the tracks

            
            //List<Tracks> allTracks = await client.GetTable<Tracks>().ToListAsync();
            List<Tracks> allTracks = await client.GetTable<Tracks>().OrderBy(x => x.Title).ToListAsync();
            List<Tracks> allTracks2 = await client.GetTable<Tracks>().OrderByDescending(x => x.Votes).ToListAsync();

            ////get all the tracks which artist is DC Comics
            //List<Tracks> filteredComics = await client.GetTable<Tracks>().Where(x => x.Genre == "DC Comics").ToListAsync();

            ////get all the tracks ordered by title
            //List<Tracks> orderedComics = await client.GetTable<Tracks>().OrderBy(x => x.Title).ToListAsync();

            //TracksList.ItemsSource = allTracks;


            int newId = 0;
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

                

                this.Items.Add(new ItemViewModel() { ID = newId.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Votes });
                
                newId++;
            }

           foreach (var listing in allTracks2)
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



                this.Items2.Add(new ItemViewModel() { ID = newId.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Votes });

                newId++;
            }
          
            this.IsDataLoaded = true;
        }

        //private async void RefreshTracks()
        //{

        //    Tracks = SampleProperty.;

        //    //    //await client.GetTable<Tracks>().InsertAsync(tracks);
        ////    // This query filters out completed TodoItems. 
        //    //items = await todoTable
        //    //   .Where(todoItem => todoItem.Complete == false)
        //    //   .ToCollectionAsync();


        //   // ListItems.ItemsSource = items;


        //}

        public async void LoadDataVotes()
        {

            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
           "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            //get all the tracks
            List<Tracks> allTracks = await client.GetTable<Tracks>().OrderBy(x => x.Votes).ToListAsync();

            
            

            ////get all the tracks which artist is DC Comics
            //List<Tracks> filteredComics = await client.GetTable<Tracks>().Where(x => x.Genre == "DC Comics").ToListAsync();

            ////get all the tracks ordered by title
            //List<Tracks> orderedComics = await client.GetTable<Tracks>().OrderBy(x => x.Title).ToListAsync();

            //TracksList.ItemsSource = allTracks;


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
               
                
                this.Items.Add(new ItemViewModel() { ID = newId.ToString(), LineOne = listing.Title, LineTwo = listing.Artist, LineThree = listing.Genre, LineFour = listing.Votes });
                
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