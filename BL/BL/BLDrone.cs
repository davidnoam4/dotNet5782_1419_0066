﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using BO;


namespace BL
{
    partial class BL : BlApi.IBL
    {
        /// <summary>
        /// add a drone
        /// </summary>
        /// <returns></no returns, add a drone>
        public void AddDrone(Drone newDrone, int idStation)
        {
            try
            {
                CheckDrone(newDrone);// check the input of the user
            }
            catch (IdException e)
            {
                throw new IdException(e.Message, e);
            }
            catch(ModelException e)
            {
                throw new ModelException(e.Message, e);
            }
            DO.Drone drone = new DO.Drone();
            DroneToList newDroneToList = new DroneToList();

            drone.Id = newDrone.Id;
            drone.Model = newDrone.Model;
            drone.Weight = (DO.WeightCategories)newDrone.Weight;
            drone.deleted = false;


            newDroneToList.Id = newDrone.Id;
            newDroneToList.Model = newDrone.Model;
            newDroneToList.Weight = newDrone.Weight;
            newDroneToList.Battery = 20 * rand.NextDouble() + 20;
            newDroneToList.Battery = (double)System.Math.Round(newDroneToList.Battery, 3);
            newDroneToList.Status = DroneStatuses.Available; // for the charge after he will be in Maintenance.
            newDroneToList.IdParcel = 0;
            try
            {
                DO.Station station = dal.GetStation(idStation);// try to find the station the user want to connect the drone to and if the station the
                if (station.ChargeSlots == 0) // user ask have place for charge
                    throw new ChargeSlotsException("ERROR: The station you ask not have more place.");
            }
            catch (DO.IdNotFoundException e)
            {
                throw new IdException(e.Message, e);
            }

            newDroneToList.Location = new Location();
            newDroneToList.Location.Longitude = dal.GetStation(idStation).Longitude;
            newDroneToList.Location.Latitude = dal.GetStation(idStation).Latitude;

           
            int foundDrone = CheckDroneAndParcel(newDroneToList.Id, dal.GetParcels(parcel => true));//return the id of the parcel
            if(foundDrone!=0)
                 newDroneToList.IdParcel = foundDrone;
            

            try
            {
                dal.AddDrone(drone);// add the drone just if the drone not in the data center
            }
            catch (DO.IdExistException e)
            {
                throw new IdException(e.Message, e);
            }
            ListDrones.Add(newDroneToList);

            try
            {
                SendDroneToDroneCharge(newDroneToList.Id);
            }
            catch (IdException e)
            {
                throw new IdException(e.Message, e);
            }
            catch (StatusDroneException e)
            {
                throw new StatusDroneException(e.Message, e);
            }
            catch (BatteryDroneException e)
            {
                throw new BatteryDroneException(e.Message, e);
            }

        }

        /// <summary>
        /// get a drone
        /// </summary>
        /// <returns></return the drone>
        public Drone GetDrone(int id)
        {
            DO.Drone idalDrone = new DO.Drone();
            try
            {
                idalDrone = dal.GetDrone(id);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new IdException(e.Message, e);
            }

            Drone drone = new Drone();
            drone.Id = idalDrone.Id;
            drone.Model = idalDrone.Model;
            drone.Weight = (WeightCategories)idalDrone.Weight;

            foreach (var eleDroneToList in ListDrones)
            {
                if (eleDroneToList.Id == drone.Id)
                {
                    drone.Battery = eleDroneToList.Battery;
                    drone.Status = eleDroneToList.Status;
                    drone.Location = new Location()
                        {Longitude = eleDroneToList.Location.Longitude, Latitude = eleDroneToList.Location.Latitude};

                    DO.Parcel parcel = new DO.Parcel();
                    drone.ParcelByTransfer = new ParcelByTransfer();
                    try
                    {
                        parcel = dal.GetParcel(eleDroneToList.IdParcel);
                    }
                    catch (DO.IdNotFoundException )
                    {
                        drone.ParcelByTransfer.Status = false;
                        return drone;
                    }

                    if (parcel.DroneId == drone.Id)
                    {
                        drone.ParcelByTransfer.Id = parcel.Id;
                        drone.ParcelByTransfer.Weight = (WeightCategories)parcel.Weight;

                        if (parcel.Scheduled != null && parcel.PickedUp == null)
                            drone.ParcelByTransfer.Status = false;

                        if (parcel.PickedUp != null && parcel.Delivered == null)
                            drone.ParcelByTransfer.Status = true;

                        drone.ParcelByTransfer.Priority = (Priorities)parcel.Priority;

                        DO.Customer customer = dal.GetCustomer(parcel.SenderId);
                        drone.ParcelByTransfer.SenderInParcel = new CustomerInParcel()
                            {Id = customer.Id, NameCustomer = customer.Name};
                        drone.ParcelByTransfer.PickUpLocation = new Location()
                            {Longitude = customer.Longitude, Latitude = customer.Latitude};

                        customer = dal.GetCustomer(parcel.TargetId);
                        drone.ParcelByTransfer.ReceiverInParcel = new CustomerInParcel()
                            {Id = customer.Id, NameCustomer = customer.Name};
                        drone.ParcelByTransfer.TargetLocation = new Location()
                            {Longitude = customer.Longitude, Latitude = customer.Latitude};

                        drone.ParcelByTransfer.DistanceOfTransfer = Distance(drone.ParcelByTransfer.PickUpLocation,
                            drone.ParcelByTransfer.TargetLocation);
                        drone.ParcelByTransfer.DistanceOfTransfer = (double)System.Math.Round(drone.ParcelByTransfer.DistanceOfTransfer, 3);
                    }
                }
            }
            return drone;
        }

        /// <summary>
        /// get a drones
        /// </summary>
        /// <returns></return all drones>
        public IEnumerable<DroneToList> GetDrones()
        {
            IEnumerable<DroneToList> drones = ListDrones;
            return drones;
        }

        /// <summary>
        /// get a drones with filtering of status
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<DroneToList> GetDronesByStatus(DroneStatuses status)
        {
            IEnumerable<DroneToList> drones = ListDrones.FindAll(drone => drone.Status == status);
            return drones;
        }

        /// <summary>
        /// get a drones with filtering of max weight
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        public IEnumerable<DroneToList> GetDronesByMaxWeight(WeightCategories weight)
        {
            IEnumerable<DroneToList> drones = ListDrones.FindAll(drone => drone.Weight <= weight);
            return drones;
        }

        /// <summary>
        /// Update the model of the drone
        /// </summary>
        /// <returns></no returns, update the model of the drone>
        public void UpdateDroneModel(int droneId, string newModel)
        {
            DO.Drone updateDrone = new DO.Drone();
            try
            {
                updateDrone = dal.GetDrone(droneId);
            }
            catch (DO.IdNotFoundException e)
            {
                throw new IdException(e.Message, e);
            }
            
            if (newModel == "")
                throw new ModelException("ERROR: Model must have value");

            updateDrone.Model = newModel;

            for (int i = 0; i < ListDrones.Count(); i++)
            {
                if (ListDrones[i].Id == droneId)
                {
                    DroneToList updateDroneToList = ListDrones[i];
                    updateDroneToList.Model = newModel;
                    ListDrones[i] = updateDroneToList;
                }
            }

            dal.UpdateDrone(updateDrone);
        }

        /// <summary>
        /// Send the drone to drone charge
        /// </summary>
        /// <returns></no returns, just send the drone to drone charge>
        public void SendDroneToDroneCharge(int id)
        {
            Drone drone = new Drone();
            try
            {
                drone = GetDrone(id);
            }
            catch (IdException e)
            {
                throw new IdException(e.Message, e);
            }

            if (drone.Status != DroneStatuses.Available)
                throw new StatusDroneException("ERROR: the drone not available to charge ");

            Station nearStation = NearStationToDrone(GetDrone(drone.Id));
            double distance = Distance(drone.Location, nearStation.Location);
            if (distance * BatteryAvailable > drone.Battery || distance * BatteryAvailable > 100)
                throw new BatteryDroneException("ERROR: the drone not have battery to go to station charge ");

            for (int i = 0; i < ListDrones.Count; i++)
                if (ListDrones[i].Id == drone.Id)
                {
                    DroneToList newDrone = ListDrones[i];
                    newDrone.Battery -= distance * BatteryAvailable;
                    newDrone.Battery = (double)System.Math.Round(newDrone.Battery, 3);
                    newDrone.Location = nearStation.Location;
                    newDrone.Status = DroneStatuses.Maintenance;
                    ListDrones[i] = newDrone;
                }

            DO.Station updateStation = dal.GetStation(nearStation.Id);
            updateStation.ChargeSlots--;
            dal.UpdateStation(updateStation);

            DO.DroneCharge newDroneCharge = new DO.DroneCharge();
            newDroneCharge.StationId = nearStation.Id;
            newDroneCharge.DroneId = drone.Id;
            newDroneCharge.StartCharging = DateTime.Now;
            try
            {
                dal.AddDroneCharge(newDroneCharge);
            }
            catch (DO.IdExistException e)
            {
                throw new IdException(e.Message, e);
            }
        }

        /// <summary>
        /// Release the drone from the drone charge
        /// </summary>
        /// <returns></no returns, release the drone from the drone charge>
        public void ReleaseDroneFromDroneCharge(int id)
        {
            try
            {
                GetDrone(id);
            }
            catch (IdException e)
            {
                throw new IdException(e.Message, e);
            }

            if (GetDrone(id).Status != DroneStatuses.Maintenance)
                throw new StatusDroneException("The drone can not release because he is in maintenance status.\n");

            double batteryCharge = 0;
            foreach (var elementDroneCharge in dal.GetDronesCharge(droneCharge => true))
            {
                if (id == elementDroneCharge.DroneId)
                {
                    DO.Station updateStation = dal.GetStation(elementDroneCharge.StationId);
                    updateStation.ChargeSlots++;
                    dal.UpdateStation(updateStation);

                    TimeSpan? chargeTime = DateTime.Now - elementDroneCharge.StartCharging;
                    batteryCharge = chargeTime.Value.TotalSeconds * (ChargingRateOfDrone / 3600);

                    dal.RemoveDroneCharge(elementDroneCharge);
                }
            }

            foreach (var elementDrone in ListDrones)
            {
                if (id == elementDrone.Id)
                {
                    elementDrone.Battery += batteryCharge;
                    elementDrone.Battery = (double)System.Math.Round(elementDrone.Battery, 3);
                    if (elementDrone.Battery > 100)
                        elementDrone.Battery = 100;

                    elementDrone.Status = DroneStatuses.Available;
                }
            }
        }

        /// <summary>
        /// Check the input of the user
        /// </summary>
        /// <returns></no returns, just check the input of the user>
        private void CheckDrone(Drone drone)
        {
            if (drone.Id < 100000 || drone.Id > 999999)//Check that it's 6 digits
                throw new IdException("ERROR: the ID is illegal! ");
            if (drone.Model == "")
                throw new ModelException("ERROR: model must have value");
        }

        /// <summary>
        /// check if the drone have parcel for the DroneToList in bl
        /// </summary>
        /// <param name="droneId"></the drone we search for>
        /// <param name="parcels"></all the parcels in dal>
        /// <returns></return the parcel id if the drone coneccted to some parcel else -1 (not conected)>
        private int CheckDroneAndParcel(int droneId, IEnumerable<DO.Parcel> parcels)
        {
            foreach (DO.Parcel elementParcel in parcels)
                if (elementParcel.DroneId == droneId)
                    return elementParcel.Id;
            return 0;
        }
    }
}
