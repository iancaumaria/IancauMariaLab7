using IancauMariaLab7.Models;
namespace IancauMariaLab7;

public partial class ListPage : ContentPage
{
    public ListPage()
    {
        InitializeComponent();
    }

    // Save the shopping list
    async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        slist.Date = DateTime.UtcNow;
        await App.Database.SaveShopListAsync(slist);
        await Navigation.PopAsync();
    }

    // Delete the shopping list
    async void OnDeleteButtonClicked(object sender, EventArgs e)
    {
        var slist = (ShopList)BindingContext;
        await App.Database.DeleteShopListAsync(slist);
        await Navigation.PopAsync();
    }

    // Navigate to the Product page
    async void OnChooseButtonClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ProductPage((ShopList)this.BindingContext)
        {
            BindingContext = new Product()
        });
    }

    // Load the products when the page appears
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        var shopl = (ShopList)BindingContext;
        if (shopl == null) return;  // Ensure BindingContext is set properly

        // Fetch the products from the database and set the ItemsSource for the ListView
        var products = await App.Database.GetListProductsAsync(shopl.ID);
        listView.ItemsSource = products;
    }

    // Handle the delete action for an individual product
    async void OnDeleteItemClicked(object sender, EventArgs e)
    {
        var selectedProduct = listView.SelectedItem as Product;

        if (selectedProduct != null)
        {
            bool confirmDelete = await DisplayAlert("Confirm Delete",
                                                     $"Are you sure you want to delete {selectedProduct.Description}?",
                                                     "Yes", "No");

            if (confirmDelete)
            {
                var slist = (ShopList)BindingContext;

                // Ștergere produs din baza de date
                await App.Database.DeleteProductAsync(selectedProduct);

                // Actualizare lista după ștergere
                listView.ItemsSource = await App.Database.GetListProductsAsync(slist.ID);
            }
        }
        else
        {
            // Alertă pentru cazul în care nu este selectat niciun produs
            await DisplayAlert("No Product Selected", "Please select a product to delete.", "OK");
        }
    }


    // Handle item tap event to mark product for deletion
    void OnItemTapped(object sender, ItemTappedEventArgs e)
    {
        // The item that was tapped can be accessed through e.Item
        var selectedProduct = e.Item as Product;

        if (selectedProduct != null)
        {
            // Automatically select the tapped item for deletion
            listView.SelectedItem = selectedProduct;
        }
    }
}
