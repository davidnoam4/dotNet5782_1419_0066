﻿using System;
using System.Collections.Generic;
using DO;

namespace DalApi
{
    public interface IDal
    {
        // General
        double[] GetRequestPowerConsumption(); // Return the battery drop rates and charge rate.


        // Station
        void AddStation(Station newStation); // Adds a station to the list of stations.
        void RemoveStation(int stationId); // Removes a station from the list of stations.
        Station GetStation(int stationId); // Returning a station according to the id of the station.
        IEnumerable<Station> GetStations(Predicate<Station> stationPredicate); // Returning the list of stations.
        void UpdateStation(Station station); // Update station data according to user request.


        // Drone
        void AddDrone(Drone newDrone); // Adds a drone to the list of drones.
        void RemoveDrone(int droneId); // Removes a drone from the list of drones.
        Drone GetDrone(int droneId); // Returning a drone according to the id of the drone.
        IEnumerable<Drone> GetDrones(Predicate<Drone> dronePredicate); // Returning the list of drones.
        void UpdateDrone(Drone drone); // Update drone data according to user request.


        // Customer
        void AddCustomer(Customer newCustomer); // Adds a customer to the list of customers.
        void RemoveCustomer(int customerId); // Removes a customer from the list of customers.
        Customer GetCustomer(int customerId); // Returning a customer according to the id of the customer.
        IEnumerable<Customer> GetCustomers(Predicate<Customer> customerPredicate); // Returning the list of customers.
        void UpdateCustomer(Customer customer); // Update customer data according to user request.


        // Parcel
        int AddParcel(Parcel newParcel); // Adds a parcel to the list of parcels.
        void RemoveParcel(int parcelId); // Removes a parcel from the list of parcels.
        Parcel GetParcel(int parcelId); // Returning a parcel according to the id of the parcel.
        IEnumerable<Parcel> GetParcels(Predicate<Parcel> parcelPredicate); // Returning the list of parcels.
        void UpdateParcel(Parcel parcel); // Update parcel data according to user request.


        // DroneCharge
        void AddDroneCharge(DroneCharge newDroneCharge); // Adds a droneCharge to the list of dronesCharge.
        void RemoveDroneCharge(DroneCharge DroneCharge); // Remove a droneCharge from the list of droneCharges.
        IEnumerable<DroneCharge> GetDronesCharge(Predicate<DroneCharge> droneChargePredicate); // Returning the list of dronesCharge.
    }
}