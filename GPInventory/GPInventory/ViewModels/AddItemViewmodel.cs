﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GPInventory.Models;
using GPInventory.Repository;
using GPInventory.Service;
using GPInventory.Views;
using Xamarin.Forms;

namespace GPInventory.ViewModels
{
    public class AddItemViewmodel: BaseViewmodel
    {
        public INavigation Navigation { get; set; }
        protected ItemsRepository Itemsrepository { get; } = new ItemsRepository();
        protected IInventoryService _inventoryService;
        public ItemsModel Item = new ItemsModel();

        private string _category;
        public string Category
        {
            get => _category;
            set => SetProperty(ref _category, value);
        }

        private int _quantity;
        public int Quantity
        {
            get => _quantity;
            set => SetProperty(ref _quantity, value);
        }
        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }
        public AddItemViewmodel(INavigation navigation, IInventoryService inventoryService, ItemsModel item = null)
        {
            _inventoryService = inventoryService;
            Item = item;
            Populate();
            Navigation = navigation;
            SaveItemCommand = new Command(async () => await SaveItemAsync());
        }

        public ICommand SaveItemCommand { get; set; }

        private void Populate()
        {
            if(Item != null)
            {
                Name = Item?.Name;
                Category = Item?.Category;
                Quantity = (int)(Item?.Quantity);
            }
        }        

        private async Task SaveItemAsync()
        {
            if (Category == null || Quantity == 0 || Name == null)
                return;

            if(Item == null)
            {
                ItemsModel model = new ItemsModel()
                {
                    Category = this.Category,
                    Quantity = this.Quantity,
                    Name = this.Name
                };
                await this.Itemsrepository.Create(model);
            }
            else
            {
                Item.Name = Name;
                Item.Category = Category;
                Item.Quantity = Quantity;
                await this.Itemsrepository.Update(Item);
            }
            
            MessagingCenter.Send(this, "Update", Item);
            MessagingCenter.Unsubscribe<AddItemViewmodel>(this, "Update");
            await Navigation.PopAsync();
        }
    }
}
