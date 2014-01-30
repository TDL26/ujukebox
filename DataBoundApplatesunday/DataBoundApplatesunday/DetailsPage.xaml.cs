using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using DataBoundApplatesunday.Resources;
using Microsoft.WindowsAzure.MobileServices;
using DataBoundApplatesunday.ViewModels;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Collections.ObjectModel;

namespace DataBoundApplatesunday
{
    
    public partial class DetailsPage : PhoneApplicationPage
    {
        public static ItemViewModel tr;
        
        string getreal = "";
        string shouldDownload = ""; //May not be needed if you'll only ever go to page 2 from page 1 to download...

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

            //if (e.NavigationMode == NavigationMode.Back)

            Text1.Visibility = Visibility.Collapsed;
        
            if (DataContext == null)
            {
                
                if (NavigationContext.QueryString.TryGetValue("shouldDownload", out shouldDownload))
                {
                    //Convert.ToBoolean(shouldDownload);
                    string selectedIndex = "";
                    if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                    {

                        int index = int.Parse(selectedIndex);
                        //int realid = int.Parse(selectedIndex.);
                        DataContext = App.ViewModel.Items2[index];
                        getreal = App.ViewModel.Items2[index].Real;

                    }
                }
                else
                {
                    string selectedIndex = "";
                    if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                    {

                        int index = int.Parse(selectedIndex);
                        //int realid = int.Parse(selectedIndex.);
                        DataContext = App.ViewModel.Items[index];
                        getreal = App.ViewModel.Items[index].Real;

                    }
                }

               

             }
            
        }

        private async void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Text1.Visibility = Visibility.Visible;

            ItemViewModel tr = (ItemViewModel)this.DataContext;
            int id = Convert.ToInt32(getreal);
            //id++;
            string title = tr.LineOne;
            string artist = tr.LineTwo;
            string genre = tr.LineThree;
            int vote = tr.LineFour;
            vote++;
            tr.LineFour++;

            
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://ujukebox.azurewebsites.net/");                             // base URL for API Controller i.e. RESTFul service

            // add an Accept header for JSON
            client.DefaultRequestHeaders.
                Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

             HttpResponseMessage response = await client.GetAsync("api/ujukeapi");

            // continue
             if (response.IsSuccessStatusCode)                                                   // 200.299
             {
                 // read result 
                 //String output = "";
                 //var lists = await response.Content.ReadAsAsync<IEnumerable<Track>>();
              
             }

            // 2
            // update by Put to /api/stock a listing serialised in request body
            //response = await client.GetAsync("api/ujukeapi/?ID=65");

            Track newListing = new Track { ID = int.Parse(getreal), Title = title, Artist = artist, Genre = genre,  Vote = vote };

            // price has dropped for FB
            response = await client.PutAsJsonAsync("api/ujukeapi/"+id, newListing);
            if (!response.IsSuccessStatusCode)
            {
                Uri newStockUri = response.Headers.Location;
                Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
            }

                                   
            Thread.Sleep(1200);

            App.ViewModel = null; 
                       

            if (shouldDownload =="true")
            {
                
                //public ObservableCollection<ItemViewModel> Items { get; private set; }
                App.ViewModel = null;

                NavigationService.Navigate(new Uri("/ChartsPage.xaml", UriKind.Relative));

            }

            
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
          
     }

        

        public int selectedIndex { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //ItemViewModel change = App.ViewModel.UpdateNote();
            //NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}