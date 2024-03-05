using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;
using WeatherApp.ViewModels.Command;
using WeatherApp.ViewModels.Helpers;

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
        private City selectedCity;
        public City SelectedCity
        {
            get
            {
                return selectedCity;
            }
            set
            {
                selectedCity = value;
                OnPropertyChanged("selectedCity");
            }
        }
        public WeatherViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()))
            {
                SelectedCity = new City()
                {
                    LocalizedName = "Hanoi City"
                };
                CurrentConditions = new CurrentCondition
                {
                    WeatherText = "Partly Sunny",
                    Temperature = new Temperature
                    {
                        Metric = new Metric
                        {
                            Value = 20
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
        }
        public SearchCommand SearchCommand { get; set; }
        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);
        }
    }
}
