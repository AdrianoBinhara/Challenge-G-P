using System;
using System.Collections.Generic;
using GPInventory.Repository;
using GPInventory.Service;
using GPInventory.ViewModels;
using Xamarin.Forms;

namespace GPInventory.Views
{
    public partial class ItemsPage : ContentPage
    {
        private readonly IInventoryClient _inventoryClient;
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = new ItemsPageViewmodel(Navigation,  new InventoryService());
        }
    }
}
