using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public ObservableCollection<City> Cities { get; set; }

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
                GetCurrentConditions();
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
                        Metric = new Units
                        {
                            Value = "20"
                        }
                    }
                };
            }

            SearchCommand = new SearchCommand(this);
            Cities = new ObservableCollection<City>();
        }
        private async void GetCurrentConditions()
        {
            Query = string.Empty;
            Cities.Clear();
            CurrentConditions = await AccuWeatherHelper.GetCurrentCondition(SelectedCity.Key);
        }
        public SearchCommand SearchCommand { get; set; }
        public async void MakeQuery()
        {
            var cities = await AccuWeatherHelper.GetCities(Query);

            Cities.Clear();
            foreach (var city in cities)
            {
                Cities.Add(city);
            }
        }
    }
}
