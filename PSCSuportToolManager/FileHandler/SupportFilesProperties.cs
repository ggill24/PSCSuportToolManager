using System.ComponentModel;

namespace PSCSuportToolManager.FileHandler
{
    partial class SupportFiles : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public string AppSelected { get; set; }

        string appSelectedDisplaytxt;
        public string AppSelectedDisplaytxt
        {
            get { return appSelectedDisplaytxt; }
            set
            {
                appSelectedDisplaytxt = value;
                OnPropertyChanged("AppSelectedDisplaytxt");
            }
        }
    }
}
