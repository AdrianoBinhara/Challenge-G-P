using System;
using System.Collections.Generic;
using GPInventory.Models;
using GPInventory.ViewModels;
using Xamarin.Forms;

namespace GPInventory.Views
{
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage(ItemsModel item = null)
        {
            InitializeComponent();
            BindingContext = new AddItemViewmodel(Navigation, item);
        }
    }
}
