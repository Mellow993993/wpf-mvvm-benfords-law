using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BenfordSet.ViewModel
{
    internal class ViewModelBase : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler? PropertyChanged;
        #endregion

        #region Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string propname = "")
        {
            PropertyChanged?.Invoke(this,new PropertyChangedEventArgs(propname));
        }
        #endregion
    }
}
