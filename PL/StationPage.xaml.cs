﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using BO;
using Station = PO.Station;
using StationToList = PO.StationToList;

namespace PL
{
    /// <summary>
    /// Interaction logic for StationPage.xaml
    /// </summary>
    public partial class StationPage : Page
    {
        private BlApi.IBL bl = BlApi.BlFactory.GetBl();
        private ObservableCollection<StationToList> stations;
        private Station station = new Station();

        public StationPage(ObservableCollection<StationToList> stations)
        {
            InitializeComponent();
            this.stations = stations;
            UpdateStationButton.Visibility = Visibility.Hidden;
        }
        public StationPage(Station station, ObservableCollection<StationToList> stations)
        {
            InitializeComponent();
            this.stations = stations;
            this.station = station;

            DataStationGrid.DataContext = station;

            AddButton.Visibility = Visibility.Hidden;
        }
        private void ClosePageButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = "";
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            BO.Station boStation = new BO.Station();
            boStation.Id = int.Parse(IdTextBox.Text);
            boStation.Name = NameTextBox.Text;
            boStation.Location = new Location();
            boStation.Location.Longitude = double.Parse(LongitudeTextBox.Text);
            boStation.Location.Latitude = double.Parse(LatitudeTextBox.Text);
            boStation.AvailableChargeSlots = int.Parse(AvailableChargeSlotsTextBox.Text);

            try
            {
                bl.AddStation(boStation);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (BO.ChargeSlotsException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (BO.NameException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (BO.LocationException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("You have a new station!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            // Update the view
            StationToList newStation = new StationToList();
            BO.StationToList boStationToList = new BO.StationToList();
            boStationToList = bl.GetStations().First(station => station.Id == boStation.Id);
            bl.CopyPropertiesTo(boStationToList, newStation);
            stations.Add(newStation);
            this.Content = "";
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Content = "";
        }
        private void RemoveStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.RemoveStation(station.Id);
            }
            catch (BO.ScheduledException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Update the view
            stations.Remove(stations.Where(s => s.Id == station.Id).Single());
            this.Content = "";
        }
        private void UpdateStationButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bl.UpdateDataStation(station.Id, station.Name, station.AvailableChargeSlots);
            }
            catch (BO.IdException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            catch (BO.NameException ex)
            {
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("The update is success!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

            UpdateListStations(station);
        }
        private void UpdateListStations(Station updateStation)
        {
            for (int i = 0; i < stations.Count(); i++)
                if (stations[i].Id == updateStation.Id)
                {
                    StationToList newStation = stations[i];
                    bl.CopyPropertiesTo(updateStation, newStation);
                    stations[i] = newStation;
                }
        }
    }
}
