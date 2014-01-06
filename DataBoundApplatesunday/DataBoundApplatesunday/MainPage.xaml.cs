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

namespace DataBoundApplatesunday
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ItemViewModel lastSelectedItem;
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Set the data context of the LongListSelector control to the sample data
            DataContext = App.ViewModel;

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        // Load data for the ViewModel Items
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!App.ViewModel.IsDataLoaded)
            {
                App.ViewModel.LoadData();
            }
        }

        // Handle selection changed on LongListSelector
        private void MainLongListSelector_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // If selected item is null (no selection) do nothing
            if (MainLongListSelector.SelectedItem == null)
                return;

            ItemViewModel selectedItem = MainLongListSelector.SelectedItem as ItemViewModel;
            lastSelectedItem = selectedItem;
            // Navigate to the new page
            NavigationService.Navigate(new Uri("/DetailsPage.xaml?selectedItem=" + selectedItem.ID, UriKind.Relative));

            // Reset selected item to null (no selection)
            MainLongListSelector.SelectedItem = null;
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            MobileServiceClient client = new MobileServiceClient("https://ujukebox.azure-mobile.net/",
            "WzaesYtewHSUagMdcYPiBnPwhCromc10");

            lastSelectedItem.LineFour++;

            //await client.GetTable<Tracks>().Where(x => x.Votes == ).ToListAsync();
            //await client.GetTable<Tracks>().ToListAsync();
             NavigationService.Navigate(new Uri("/ChartsPage.xaml", UriKind.Relative));
        }

        // Sample code for building a localized ApplicationBar
        //private void BuildLocalizedApplicationBar()
        //{
        //    // Set the page's ApplicationBar to a new instance of ApplicationBar.
        //    ApplicationBar = new ApplicationBar();

        //    // Create a new button and set the text value to the localized string from AppResources.
        //    ApplicationBarIconButton appBarButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/appbar.add.rest.png", UriKind.Relative));
        //    appBarButton.Text = AppResources.AppBarButtonText;
        //    ApplicationBar.Buttons.Add(appBarButton);

        //    // Create a new menu item with the localized string from AppResources.
        //    ApplicationBarMenuItem appBarMenuItem = new ApplicationBarMenuItem(AppResources.AppBarMenuItemText);
        //    ApplicationBar.MenuItems.Add(appBarMenuItem);
        //}
    }
}