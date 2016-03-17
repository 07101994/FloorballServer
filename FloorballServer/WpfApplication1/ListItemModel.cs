using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1
{
    public class ListItemModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        public ListItemModel()
        {

        }

        public ListItemModel(/*string name, int id,*/ bool isChecked)
        {
            this.isChecked = isChecked;
        }

        public string Name { get; set; }

        public int Id { get; set; }

        private bool isChecked;

        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                isChecked = value;
                OnPropertyChanged("IsChecked");
                if (MainWindow.allowAddingId)
                {
                    if (MainWindow.Ids.ContainsKey(Id))
                    {
                        MainWindow.Ids.Remove(Id);
                    }
                    else
                    {
                        MainWindow.Ids.Add(Id, IsChecked);
                    }
                }
            }
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
