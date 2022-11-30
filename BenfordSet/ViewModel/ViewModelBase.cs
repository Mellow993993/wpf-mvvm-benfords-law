using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BenfordSet.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propname = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propname));
        }
    }
}
