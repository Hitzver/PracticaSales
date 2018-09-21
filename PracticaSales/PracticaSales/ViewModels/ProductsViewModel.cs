using GalaSoft.MvvmLight.Command;
using PracticaSales.Common.Models;
using PracticaSales.Helpers;
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

            var connection = await apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                IsRefresing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, connection.Message, Languages.Accept);
                return;
            }

            var url = Application.Current.Resources["UrlApi"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlProductsController"].ToString();

            var response = await apiService.GetList<Product>(url, prefix, controller);
            if(!response.IsSuccess)
            {
                IsRefresing = false;
                await Application.Current.MainPage.DisplayAlert(Languages.Error, response.Message, Languages.Accept);
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
