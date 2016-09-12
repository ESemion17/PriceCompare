using System;
using System.Collections.ObjectModel;

namespace PriceCompareUi
{
    public class ContactListViewModel
    {
        public ContactListViewModel()
        {
            Contacts = new ObservableCollection<ItemListViewModel>();
        }

        public string Header { get; set; }

        public ObservableCollection<ItemListViewModel> Contacts { get; }

        public event Action<ItemListViewModel> ContactOpened;

        public void OnContactOpened(ItemListViewModel contact)
        {
            ContactOpened?.Invoke(contact);
        }
    }
}