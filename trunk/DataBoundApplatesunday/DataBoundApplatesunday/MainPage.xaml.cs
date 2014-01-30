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
using DataBoundApplatesunday.ViewModels;
using Microsoft.WindowsAzure.MobileServices;
using System.Net.Http;
using System.Net.Http.Headers;

namespace DataBoundApplatesunday
{
    // a listing for a stock on the stock market - model class in RESTful service
    public class Track
    {
        public int ID { get; set; }
        public string Title
        {
            get;
            set;
        }

        public string Artist
        {
            get;
            set;
        }

        public string Genre
        {
            get;
            set;
        }
        public int Vote { get; set; }
    }

    public partial class MainPage : PhoneApplicationPage
    {
        // URI for RESTful service (implemented using Web API)
        private const String serviceURI = "http://ujukebox.azurewebsites.net/api/ujukeapi";

        
        private ItemViewModel lastSelectedItem;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
             
            // Set the data context of the LongListSelector control to the sample data
            //DataContext = null;
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //App.ViewModel.LoadData();
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int selectedIndexOfSchedule = scheduleListBox.SelectedIndex;
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            ItemViewModel selectedItem = MainLongListSelector.SelectedItem as ItemViewModel;
            
            
            lastSelectedItem = selectedItem;
            
            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));
           

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;

        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
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
                
               
                // display on UI
                //displayTextBlock.Text = output;
            }
            else
            {
                //displayTextBlock.Text = response.StatusCode + " " + response.ReasonPhrase;
            }
            NavigationService.Navigate(new Uri("/ChartsPage.xaml", UriKind.Relative));
            
        }

        
    }
}