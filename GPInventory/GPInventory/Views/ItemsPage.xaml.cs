using System;
using System.Collections.Generic;
using GPInventory.Repository;
using GPInventory.ViewModels;
using Xamarin.Forms;

namespace GPInventory.Views
{
    public partial class ItemsPage : ContentPage
    {
        public ItemsPage()
        {
            InitializeComponent();
            BindingContext = new ItemsPageViewmodel();
        }
    }
}
