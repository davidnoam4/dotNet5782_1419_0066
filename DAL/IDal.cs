﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IDAL;
using IDAL.DO;

namespace IDAL
{
    public interface IDal
    {
        // General
        double[] GetRequestPowerConsumption();


        // Station
        void AddStation(Station newStation);
        Station GetStation(int stationId);
        IEnumerable<Station> GetStations(Predicate<Station> stationPredicate);
        void UpdateStation(Station station);


        // Drone
        void AddDrone(Drone newDrone);
        Drone GetDrone(int droneId);
        IEnumerable<Drone> GetDrones();
        void UpdateDrone(Drone drone);


        // Customer
        void AddCustomer(Customer newCustomer);
        Customer GetCustomer(int customerId);
        IEnumerable<Customer> GetCustomers();
        void UpdateCustomer(Customer customer);


        // Parcel
        int AddParcel(Parcel newParcel);
        Parcel GetParcel(int parcelId);
        IEnumerable<Parcel> GetParcels(Predicate<Parcel> parcelPredicate);
        void UpdateParcel(Parcel parcel);


        // DroneCharge
        void AddDroneCharge(DroneCharge newDroneCharge);
        void RemoveDroneCharge(DroneCharge DroneCharge);
        IEnumerable<DroneCharge> GetDronesCharge();
    }
}