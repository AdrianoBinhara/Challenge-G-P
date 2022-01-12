using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GPInventory.Models;
using GPInventory.Repository;
using GPInventory.Views;
using Xamarin.Forms;

namespace GPInventory.ViewModels
{
    public class ItemsPageViewmodel: BaseViewmodel
    {
        public INavigation Navigation { get; set; }

        protected ItemsRepository ItemsRepository { get; } = new ItemsRepository();

        private ObservableCollection<ItemsModel> _items;
        public ObservableCollection<ItemsModel> Items
        {
            get=> _items;
            set=> SetProperty(ref _items, value);
        }
        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }


        public ItemsPageViewmodel(INavigation navigation)
        {
            Navigation = navigation;
            AddItemCommand = new Command(async () => await AddItemAsync());
            RefreshList = new Command(() =>  LoadItems());
            LoadItems();
            SubscribeMessaging();
        }

        private void SubscribeMessaging()
        {
            MessagingCenter.Subscribe<AddItemViewmodel>(this, "Update", (sender) =>
            {
                LoadItems();
            });
        }

        public ICommand AddItemCommand { get; set; }
        public ICommand RefreshList { get; set; }

        private Task LoadItems()
        {
            Task.Run(async () =>
            {
                IsBusy = true;
                try
                {
                    List<ItemsModel> data = await ItemsRepository.GetAllasync();
                    Items = new ObservableCollection<ItemsModel>(data);
                }
                finally
                {
                    IsBusy = false;
                }
            });
            return Task.CompletedTask;
            
        }

        private async Task AddItemAsync()
        {
            await Navigation.PushAsync(new AddItemPage());
        }
    }
}
