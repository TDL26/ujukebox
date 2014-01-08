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

namespace DataBoundApplatesunday
{
    
    public partial class DetailsPage : PhoneApplicationPage
    {
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
            if (DataContext == null)
            {
                string selectedIndex = "";
                if (NavigationContext.QueryString.TryGetValue("selectedItem", out selectedIndex))
                {
                    int index = int.Parse(selectedIndex);
                    DataContext = App.ViewModel.Items[index];

                }

             }
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {


            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
           "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            

        //    //ItemViewModel newTrack = new Tracks();
            Tracks tracks = new Tracks();

        //    //int votes = tracks.Votes;
        //    //votes++;


                await client.GetTable<Tracks>().UpdateAsync(tracks);       
  
            //await client.GetTable<Tracks>().Where(x => x.Votes == selectedIndex).ToListAsync();

            
        }

        

        public int selectedIndex { get; set; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //ItemViewModel change = App.ViewModel.UpdateNote();
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}