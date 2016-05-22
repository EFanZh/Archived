using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SearchFiles
{
    public class Model : INotifyPropertyChanged
    {
        private string folder = string.Empty;
        private string keyword = string.Empty;
        private string state = string.Empty;

        public event PropertyChangedEventHandler PropertyChanged;

        public Model()
        {
            Result = new ObservableCollection<string>();
        }

        public string Folder
        {
            get
            {
                return folder;
            }
            set
            {
                folder = value;

                NotifyPropertyChanged();
            }
        }

        public string Keyword
        {
            get
            {
                return keyword;
            }
            set
            {
                keyword = value;

                NotifyPropertyChanged();
            }
        }

        public ObservableCollection<string> Result
        {
            get;
            set;
        }

        public string State
        {
            get
            {
                return state;
            }
            set
            {
                state = value;

                NotifyPropertyChanged();
            }
        }

        private void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
