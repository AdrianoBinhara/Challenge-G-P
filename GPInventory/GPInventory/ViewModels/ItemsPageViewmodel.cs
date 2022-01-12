using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GPInventory.Models;
using GPInventory.Repository;
using Xamarin.Forms;

namespace GPInventory.ViewModels
{
    public class ItemsPageViewmodel: BaseViewmodel
    {

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


        public ItemsPageViewmodel()
        {
            AddItemCommand = new Command<Frame>(async (Frame) => await AddItemAsync(Frame));
            LoadItems();
        }

        private Task LoadItems()
        {
            Task.Run(async () =>
            {
                try
                {
                    List<ItemsModel> data = await ItemsRepository.GetAllasync();
                    Items = new ObservableCollection<ItemsModel>(data);
                }
                catch (Exception ex)
                {

                }
            });
            return Task.CompletedTask;
        }

        public ICommand AddItemCommand { get; set; }

        private async Task AddItemAsync(Frame frame)
        {
            await SimulatePressingEffect(frame);
        }


        private async Task SimulatePressingEffect(Frame frame)
        {
            await frame.ScaleTo(0.97, 30, Easing.Linear);
            await frame.ScaleTo(1, 40, Easing.Linear);
        }
    }
}
