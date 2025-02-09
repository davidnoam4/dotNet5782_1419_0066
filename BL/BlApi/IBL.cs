﻿using System;
using System.Collections.Generic;
using System.Linq;
using BO;


namespace BlApi
{
    public interface IBL
    {
        void CopyPropertiesTo<T, S>(S from, T to); // Copy properties
        double Distance(Location from, Location to); // Calculate distance between 2 locations

        //Simulator
        void SimulatorMod(int droneId, Action updateView, Func<bool> stopSimulator); //for the simulator mod

        // Station
        void AddStation(Station newStation); // Adds a station to the list of stations.
        void RemoveStation(int stationId); // Removes a parcel from the list of parcels.
        Station GetStation(int stationId); // Returning a station according to the id of the station.
        IEnumerable<StationToList> GetStations(); // Returning the list of stations in a special entity "Station to list".
        IEnumerable<StationToList> GetStationsWithAvailableCharge(); // Returning the list of stations with available charging position in a special entity "Station to list".
        IEnumerable<IGrouping<int, StationToList>> GetStationsByGroupAvailableStations(); // Returning the list of stations with grouping of available stations
        void UpdateDataStation(int id, string name, int chargeSlots); // Update station data according to user request.


        // Drone
        void AddDrone(Drone newDrone, int idStation); // Adds a drone to the list of drones.
        void RemoveDrone(int droneId); // Removes a drone from the list of drones.
        Drone GetDrone(int droneId); // Returning a drone according to the id of the drone.
        IEnumerable<DroneToList> GetDrones(); // Returning the list of drones in a special entity "Drone to list".
        IEnumerable<DroneToList> GetDronesByStatus(DroneStatuses status); // Returning the list of drones with filtering of status
        IEnumerable<DroneToList> GetDronesByMaxWeight(WeightCategories weight); // Returning the list of drones with filtering of max weight
        IEnumerable<IGrouping<DroneStatuses, DroneToList>> GetDronesByGroupStatus();// Returning the list of drones with grouping of status
        void UpdateDroneModel(int droneId, string newModel); // Update drone data according to user request.
        void SendDroneToDroneCharge(int droneId);// Sending a drone for charging at a vacant charging station.
        void ReleaseDroneFromDroneCharge(int droneId); // Release drone from charging station.
        void ConnectParcelToDrone(int droneId); // Selecting a parcel according to criteria and sending a drone according to the id that will be associated with the parcel.
        void CollectionParcelByDrone(int droneId); // Sending the desired drone (according to id) to collect the parcel from its place.
        void SupplyParcelByDrone(int droneId); // Sending the drone (according to id) deliver the parcel to the destination of the parcel.


        // Customer
        void AddCustomer(Customer newCustomer); // Adds a customer to the list of customers.
        void RemoveCustomer(int customerId); // Removes a customer from the list of customers.
        Customer GetCustomer(int customerId); // Returning a customer according to the id of the customer.
        IEnumerable<CustomerToList> GetCustomers(); // Returning the list of customers in a special entity "Customer to list".
        void UpdateDataCustomer(int id, string name, string phone); // Update customer data according to user request.
        int LastDigitId(int lessId); // Function for the digit check in id.


        // Parcel 
        int AddParcel(Parcel newParcel); // Adds a parcel to the list of parcels.
        void RemoveParcel(int parcelId); // Removes a parcel from the list of parcels.
        Parcel GetParcel(int parcelId); // Returning a parcel according to the id of the parcel.
        IEnumerable<ParcelToList> GetParcels(); // Returning the list of parcels in a special entity "Parcel to list".
        IEnumerable<ParcelToList> GetParcelsNoDrones(); // Returning the list of parcels with no drones in a special entity "Parcel to list".
        IEnumerable<ParcelToList> GetParcelsByStatus(ParcelStatuses status); // Returning the list of parcels in special status for show
        IEnumerable<IGrouping<string, ParcelToList>> GetParcelsByGroupCustomers(string typeCustomer); // Returning the list after groping


        //DroneInCharge
        void RemoveDroneInCharge(Station station, DroneInCharge droneInCharge);
    }
}
