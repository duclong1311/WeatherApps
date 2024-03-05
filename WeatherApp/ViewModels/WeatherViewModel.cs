using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private string query;
        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
                OnPropertyChanged("Query");
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private CurrentCondition currentConditions;

        public CurrentCondition CurrentConditions
        {
            get
            {
                return currentConditions;
            }
            set
            {
                currentConditions = value;
                OnPropertyChanged("CurrentConditions");
            }
        }
    }

}
