using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Acr.UserDialogs;
using GPInventory.Core;
using GPInventory.Models;
using GPInventory.Repository;
using GPInventory.Service;
using GPInventory.Sync;
using GPInventory.Views;
using Refit;
using Xamarin.Forms;

namespace GPInventory.ViewModels
{
    public class ItemsPageViewmodel: BaseViewmodel
    {
        public INavigation Navigation { get; set; }
        private readonly IInventoryService _inventoryService;

        protected ItemsRepository ItemsRepository { get; } = new ItemsRepository();

        private ObservableCollection<ItemsModel> _items;
        public ObservableCollection<ItemsModel> Items
        {
            get=> _items;
            set=> SetProperty(ref _items, value);
        }

        private int _totalItems;
        public int TotalItems
        {
            get => _totalItems;
            set => SetProperty(ref _totalItems, value);
        }
        private int _totalStock = 0;
        public int TotalStock
        {
            get => _totalStock;
            set => SetProperty(ref _totalStock, value);
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        private bool _isEnable;
        public bool IsEnable
        {
            get => _isEnable;
            set => SetProperty(ref _isEnable, value);
        }

        private string _textSync = "SINCRONIZAR";
        public string TextSync
        {
            get => _textSync;
            set => SetProperty(ref _textSync, value);
        }


        public ItemsPageViewmodel(INavigation navigation, IInventoryService inventoryService)
        {
            Navigation = navigation;
            _inventoryService = inventoryService;
            AddItemCommand = new Command(async () => await AddItemAsync());
            RefreshList = new Command(async () =>  await SyncAsync());
            UpdateItemCommand = new Command<CollectionView>(async (CollectionView) => await UpdateItemAsync(CollectionView));
            DeleteItemCommand = new Command<ItemsModel>(async  (item) => await DeleteItemAsync(item));
            LoadItems();
            SubscribeMessaging();
        }

        public ICommand AddItemCommand { get; set; }
        public ICommand RefreshList { get; set; }
        public ICommand UpdateItemCommand { get; set; }
        public ICommand DeleteItemCommand { get; set; }

        private void SubscribeMessaging()
        {
            MessagingCenter.Subscribe<AddItemViewmodel,ItemsModel>(this, "Update",  (sender, Item) =>
            {
                LoadItems();
            });
        }

        private async Task SyncAsync()
        {
            IsBusy = true;
            IsEnable = false;
            TextSync = "Sincronizando...";

            var sync = new SyncInventory(new InventoryService());
            await sync.Sync(Items.ToList());


            TextSync = "SINCRONIZAR";
            IsEnable = true;
            IsBusy = false;

            RegisterForError();
        }

        private void RegisterForError()
        {
            MessagingCenter.Subscribe<Page>(this, "Sync", (sender) =>
            {
                UserDialogs.Instance.Toast("Não foi possível sincronizar, tente novamente", TimeSpan.FromSeconds(3));
            });
            MessagingCenter.Subscribe<SyncInventory>(this, "AlreadySync", (sender) =>
            {
                UserDialogs.Instance.Toast("Items sincronizados", TimeSpan.FromSeconds(3));
            });
        }

        private Task LoadItems()
        {
            Task.Run(async () =>
            {
                IsBusy = true;
                try
                {
                    List<ItemsModel> AllItems = await ItemsRepository.GetAllasync();
                    Items = new ObservableCollection<ItemsModel>(AllItems);

                    if (Items == null || Items.Count() == 0)
                    {
                        List<ItemsModel> model = new List<ItemsModel>();

                        model = await _inventoryService.GetItems();
                        Items = new ObservableCollection<ItemsModel>(model);
                        foreach (var item in Items)
                        {
                            await this.ItemsRepository.Create(item);
                        }
                    }
                    TotalStockItems();
                }
                finally
                {
                    IsBusy = false;
                }
            });
            return Task.CompletedTask;
            
        }

        private void TotalStockItems()
        {
            TotalItems = Items.Count;
            TotalStock = 0;
            foreach (var item in Items)
            {
                TotalStock += item.Quantity;
            }
        }

        private async Task AddItemAsync()
        {
            await Navigation.PushAsync(new AddItemPage());

            MessagingCenter.Unsubscribe<Page>(this, "AlreadySync");
        }

        private async Task DeleteItemAsync(ItemsModel item)
        {
            var selected = item;
            Items.Remove(selected);
            await this.ItemsRepository.Delete(selected);
            TotalStockItems();
        }

        private async Task UpdateItemAsync(CollectionView collectionView)
        {
            if (collectionView.SelectedItem != null)
            {
                var selected = collectionView.SelectedItem as ItemsModel;
                await Navigation.PushAsync(new AddItemPage(selected));

                MessagingCenter.Unsubscribe<Page>(this, "AlreadySync");

                collectionView.SelectedItem = null;
            }
        } 
    }
}
