using System;
using System.Collections.Generic;
using GPInventory.ViewModels;
using Xamarin.Forms;

namespace GPInventory.Views
{
    public partial class AddItemPage : ContentPage
    {
        public AddItemPage()
        {
            InitializeComponent();
            BindingContext = new AddItemViewmodel(Navigation);
        }
    }
}
