using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace PriceCompareUi
{
    public class ItemListViewModel : ObservableObejct
    {
        private ObservableCollection<string> _itemName;
        private ObservableCollection<float> _itemQuantity;
        private ObservableCollection<float> _itemPrice;
        private double _totalPrice;
        private string _header;
        private int _selected;
        private readonly string _storeName;

        public ItemListViewModel(string storeName)
        {
            _storeName = storeName;
            _header = storeName;
            _totalPrice = 0;
            _itemName = new ObservableCollection<string>();
            _itemQuantity = new ObservableCollection<float>();
            _itemPrice = new ObservableCollection<float>();
        }

        public string Header
        {
            get { return _header; }
            set
            {
                if (value == _header) return;
                _header = value;
                OnPropertyChanged();
            }
        }

        public int Selected
        {
            get { return _selected; }
            set
            {
                if (value == _selected) return;
                _selected = value;
                OnPropertyChanged();
            }
        }

        public double TotalPrice
        {
            get { return _totalPrice; }
            private set { _totalPrice = value; }
        }

        public ObservableCollection<string> ItemName
        {
            get { return _itemName; }
            set
            {
                if (Equals(value, _itemName)) return;
                _itemName = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<float> ItemQuantity
        {
            get { return _itemQuantity; }
            set
            {
                if (Equals(value, _itemQuantity)) return;
                _itemQuantity = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<float> ItemPrice
        {
            get { return _itemPrice; }
            set
            {
                if (Equals(value, _itemPrice)) return;
                _itemPrice = value;
                OnPropertyChanged();
            }
        }

        public void AddingDone(float quantity, float price)
        {
            _totalPrice += quantity*price;
            Header = $"{_storeName} - {_totalPrice:C}";
            SortingItems();
        }

        private void SortingItems()
        {
            var newItemPrice = new ObservableCollection<float>(ItemPrice.ToList().OrderBy(p => p));
            var flagsPrice = new bool[newItemPrice.Count];
            var indexes = new int[newItemPrice.Count];
            for (int i = 0; i < newItemPrice.Count; i++)
            {
                for (int j = 0; j < ItemPrice.Count; j++)
                {
                    if (flagsPrice[j]==false)
                        if (newItemPrice[i].Equals(ItemPrice[j]))
                        {
                            indexes[i] = j;
                            flagsPrice[j] = true;
                            break;
                        }
                }
            }
            var newItemQuantity = new ObservableCollection<float>();
            var newItemName = new ObservableCollection<string>();
            foreach (var i in indexes)
            {
                newItemQuantity.Add(ItemQuantity[i]);
                newItemName.Add(ItemName[i]);
            }

            ItemName = newItemName;
            ItemQuantity = newItemQuantity;
            ItemPrice = newItemPrice;
        }
    }
}