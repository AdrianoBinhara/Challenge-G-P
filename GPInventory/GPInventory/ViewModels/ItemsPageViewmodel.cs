﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GPInventory.Core;
using GPInventory.Models;
using GPInventory.Repository;
using GPInventory.Service;
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


        public ItemsPageViewmodel(INavigation navigation, IInventoryService inventoryService)
        {
            Navigation = navigation;
            _inventoryService = inventoryService;
            AddItemCommand = new Command(async () => await AddItemAsync());
            RefreshList = new Command(() =>  LoadItems());
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
                collectionView.SelectedItem = null;
            }
        } 
    }
}
