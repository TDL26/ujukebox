﻿using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using WP8jukebox.ViewModels;

namespace WP8jukebox
{
    public partial class MainPage : PhoneApplicationPage
    {
       // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the data
            DataContext = App.ViewModel;
        }

         // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {               
            base.OnNavigatedTo(e);

            //check is navigation is from a back button press - is so set the viewmodel to null  
            if (e.NavigationMode == System.Windows.Navigation.NavigationMode.Back)
            {
                //force a model reload
                App.ViewModel = null;
            }
                
            //load the model
            if (!App.ViewModel.IsDataLoaded)
            {
                ProgressIndicator prog = new ProgressIndicator();
                prog.IsVisible = true;
                prog.IsIndeterminate = true;
                prog.Text = "Downloading Data from the Cloud...";
                SystemTray.SetProgressIndicator(this, prog); 
                             
                App.ViewModel.LoadVenueData();                                      
            }
              
       }
        
        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            // Navigate to the new page - Genre=Choice
            NavigationService.Navigate(new Uri("/GenrePage.xaml?selectedItem=" + (MainLongListSelector.SelectedItem as ItemViewModel).ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

        private void About_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.Relative));
        }
    }
}