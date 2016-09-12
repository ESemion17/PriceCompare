using System.ComponentModel;
using System.Runtime.CompilerServices;
using PriceCompareUi.Annotations;

namespace PriceCompareUi
{
    public class ObservableObejct: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}