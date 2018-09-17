using GalaSoft.MvvmLight.Command;
using PracticaSales.Common.Models;
using PracticaSales.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace PracticaSales.ViewModels
{
    public class ProductsViewModel : BaseViewModel
    {
        private ApiService apiService;
        private ObservableCollection<Product> products;

        private bool isRefresing;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set { base.SetValue(ref products, value); }
        }

        public bool IsRefresing
        {
            get { return isRefresing; }
            set { base.SetValue(ref isRefresing, value); }
        }

        public ProductsViewModel()
        {
            apiService = new ApiService();
            LoadProducts();
        }

        private async void LoadProducts()
        {
            IsRefresing = true;
            //var url = Application.Current.Resources["UrlApi"].ToString();
            var response = await apiService.GetList<Product>("https://practicasalesapivla.azurewebsites.net", "/api", "/Products");
            if(!response.IsSuccess)
            {
                IsRefresing = false;
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Accept");
                return;
            }

            var list = (List<Product>)response.Result;
            Products = new ObservableCollection<Product>(list);
            IsRefresing = false;
        }

        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadProducts);
            }
        }
    }
}
