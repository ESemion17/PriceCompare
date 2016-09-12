using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using PriceCompareLogic;

namespace PriceCompareUi
{
    public class MainViewModel: ObservableObejct
    {
        private string _searchText;
        private readonly List<List<Item>> _data;
        private readonly ObservableCollection<ItemListViewModel> _itemListViewModels;
        private object _selectedTab;
        private List<Store> _storyName;

        public MainViewModel()
        {
            Quantity = "1";
            _searchText = "";
            _data = BridgeToUi.GetItems();
            _storyName = BridgeToUi.GetStoreList();
            View = CollectionViewSource.GetDefaultView(_data.First().Select(i => i.ItemName));

            _itemListViewModels = new ObservableCollection<ItemListViewModel>();
            Tabs = new ObservableCollection<object>();
            foreach (var itemsList in _data)
            {
                var store = new ItemListViewModel(_storyName.First(s => s.Id.Equals(itemsList.First().ChainId)).ChainName);

                _itemListViewModels.Add(store);
                Tabs.Add(store);
            }
            SelectedTab = _itemListViewModels.First();
            CommandnAdd = new DelegateCommand(AddToCart);
        }

        private void AddToCart()
        {
            var itemToAdd = _data.First().Find(i => i.ItemName.Equals(Item));
            for (int index = 0; index < _data.Count; index++)
            {
                var items = _data[index];
                var indexOtAdd = items.Select(i => i.ItemName).ToList().IndexOf(itemToAdd.ItemName);
                if (indexOtAdd == -1)
                    indexOtAdd = items.Select(i => i.ItemId).ToList().IndexOf(itemToAdd.ItemId);
                _itemListViewModels[index].ItemName.Add(items[indexOtAdd].ItemName);
                _itemListViewModels[index].ItemQuantity.Add(float.Parse(Quantity));
                _itemListViewModels[index].ItemPrice.Add(items[indexOtAdd].ItemPrice);
                _itemListViewModels[index].AddingDone(float.Parse(Quantity), items[indexOtAdd].ItemPrice);
            }
            SelectedTab = _itemListViewModels.First(s => s.TotalPrice == (_itemListViewModels.Select(store=> store.TotalPrice).Min()));
        }

        private void ItemListOnItemOpened(ItemListViewModel contact)
        {
            TryOpenTab(contact);
        }

        private void TryOpenTab(object tab)
        {
            if (!Tabs.Contains(tab))
            {
                Tabs.Add(tab);
            }
            SelectedTab = tab;
        }
        
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (value == _searchText) return;
                _searchText = value;
                OnPropertyChanged();
                MyFilter();
            }
        }

        private void MyFilter()
        {
            View.Filter = delegate (object item)
            {
                var text = item as string;
                return text != null && text.Contains(SearchText);
            };
        }

        public ICollectionView View { get; set; }

        public ObservableCollection<object> Tabs { get; }

        public DelegateCommand CommandnAdd { get; }

        public object SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                if (Equals(value, _selectedTab)) return;
                _selectedTab = value;
                OnPropertyChanged();
            }
        }

        public int SeletedIndex { get; set; }

        public string Item { get; set; }

        public string Quantity { get; set; }
    }
}