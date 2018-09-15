using PracticaSales.Common.Models;
using PracticaSales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace PracticaSales.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { base.SetValue(ref products, value); }
        }

        public ProductsViewModel()
        {
            apiService = new ApiService();
        }

        private async void LoadProducts()
        {
            var response = await apiService.GetList<Product>("https://practicasalesapivla.azurewebsites.net", "/api", "/Products");
            if(!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var list = (List<Product>)response.Result;
            Products = new ObservableCollection<Product>(list);
        }
    }
}
