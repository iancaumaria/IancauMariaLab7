using IancauMariaLab7.Models;
using Microsoft.Maui.Devices.Sensors;
using Plugin.LocalNotification;
namespace IancauMariaLab7;

public partial class ShopPage : ContentPage
{
    private Location location;

    public ShopPage()
	{
		InitializeComponent();
	}

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }

    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        await App.Database.SaveShopAsync(shop);
        await Navigation.PopAsync();
    }
    async void OnShowMapButtonClicked(object sender, EventArgs e)
    {
        var shop = (Shop)BindingContext;
        var address = shop.Adress;
        var locations = await Geocoding.GetLocationsAsync(address);

        var options = new MapLaunchOptions
        {
            Name = "Magazinul meu preferat" };
        var shoplocation = locations?.FirstOrDefault();
        /* var shoplocation= new Location(46.7492379, 23.5745597);//pentru
        Windows Machine */
        var myLocation = await Geolocation.GetLocationAsync();
        /* var myLocation = new Location(46.7731796289, 23.6213886738);
       //pentru Windows Machine */
        var distance = myLocation.CalculateDistance(location, DistanceUnits.Kilometers);
        if (distance < 5)
        {
            var request = new NotificationRequest
            {
                Title = "Ai de facut cumparaturi in apropiere!",
                Description = address,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Now.AddSeconds(1)
                }
            };
            LocalNotificationCenter.Current.Show(request);
        }

        await Map.OpenAsync(shoplocation, options);
        }
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        // Obținem magazinul din BindingContext
        var shop = (ShopList)BindingContext;

        // Confirmare înainte de ștergere
        bool confirmDelete = await DisplayAlert("Confirm Delete",
                                                 $"Are you sure you want to delete {shop.Name}?",
                                                 "Yes", "No");

        if (confirmDelete)
        {
            // Ștergem magazinul din baza de date
            await App.Database.DeleteShopListAsync(shop);

            // Navigăm înapoi la pagina anterioară
            await Navigation.PopAsync();
        }
    }
}