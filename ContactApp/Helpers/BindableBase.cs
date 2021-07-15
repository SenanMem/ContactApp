using System.ComponentModel;
using System.Runtime.CompilerServices;
using ContactApp.Annotations;

namespace ContactApp.Helpers
{
    public class BindableBase:INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetProperty<T>(ref T target, T value, [CallerMemberName] string propertyName = null)
        {
            if (object.Equals(target, value))
                return;

            target = value;
            OnPropertyChanged(propertyName);
        }
    }
}