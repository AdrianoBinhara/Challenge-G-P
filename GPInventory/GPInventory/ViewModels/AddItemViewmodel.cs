using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GPInventory.Models;
using GPInventory.Repository;
using Xamarin.Forms;

namespace GPInventory.ViewModels
{
    public class AddItemViewmodel: BaseViewmodel
    {
        public INavigation Navigation { get; set; }
        protected ItemsRepository Itemsrepository { get; } = new ItemsRepository();

        private string _quantity;
        public string Quantity
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
        public AddItemViewmodel(INavigation navigation)
        {
            Navigation = navigation;
            SaveItemCommand = new Command(async () => await SaveItemAsync());
        }
        public ICommand SaveItemCommand { get; set; }

        private async Task SaveItemAsync()
        {
            ItemsModel model = new ItemsModel()
            {
                Quantity = this.Quantity,
                Name = this.Name
            };
            await this.Itemsrepository.Create(model);
            await Navigation.PopAsync();
            MessagingCenter.Send<AddItemViewmodel>(this, "Update");
            MessagingCenter.Unsubscribe<AddItemViewmodel>(this, "Update");
        }

    }
}
