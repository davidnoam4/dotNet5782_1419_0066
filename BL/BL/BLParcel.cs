﻿using System;
using System.Collections.Generic;
using System.Linq;
using BO;
using System.Runtime.CompilerServices;

namespace BL
{
    partial class BL : BlApi.IBL
    {
        /// <summary>
        /// add parcel with all fields to data source with checking 
        /// </summary>
        /// <param name="newParcel"></param>
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public int AddParcel(Parcel newParcel)
        {
            int idParcel;

            try
            {
                CheckParcel(newParcel);
            }
            catch (IdException e)
            {
                throw new IdException(e.Message, e);
            }

            lock (dal)
            {
                DO.Parcel parcel = new DO.Parcel();

                parcel.SenderId = newParcel.Sender.Id;
                parcel.TargetId = newParcel.Target.Id;
                parcel.Weight = (DO.WeightCategories) newParcel.Weight;
                parcel.Priority = (DO.Priorities) newParcel.Priority;
                parcel.DroneId = 0;
                parcel.Requested = DateTime.Now;
                parcel.Scheduled = null;
                parcel.PickedUp = null;
                parcel.Delivered = null;
                parcel.Deleted = false;

                try
                {
                    idParcel = dal.AddParcel(parcel); // add the parcel just if the parcel not in the dataSource
                }
                catch (DO.IdExistException e)
                {
                    throw new IdException(e.Message, e);
                }
            }

            return idParcel;
        }

        /// <summary>
        /// Removes a parcel from the list of parcels.
        /// </summary>
        /// <param name="parcelId"></param>
      
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveParcel(int parcelId)
        {
            if (GetParcel(parcelId).Scheduled != null)
                throw new ScheduledException("ERROR: The parcel already scheduled to drone, can't remove the parcel");

            lock (dal)
            {
                try
                {
                    dal.RemoveParcel(parcelId); // Remove the parcel
                }
                catch (DO.IdExistException e)
                {
                    throw new IdException(e.Message, e);
                }
                catch (DO.IdNotFoundException e)
                {
                    throw new IdException(e.Message, e);
                }
            }
        }

        /// <summary>
        /// send id of parcel and checking that it exist.
        /// make special entity and return it
        /// </summary>
        /// <param name="parcelId"></param>
        /// <returns></returns>
       
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Parcel GetParcel(int parcelId)
        {
            Parcel parcel = new Parcel();
            lock (dal)
            {
                DO.Parcel dalParcel = new DO.Parcel();
                try
                {
                    dalParcel = dal.GetParcel(parcelId);
                }
                catch (DO.IdNotFoundException e)
                {
                    throw new IdException(e.Message, e);
                }

                parcel.Id = dalParcel.Id;
                parcel.Sender = new CustomerInParcel()
                    {Id = dalParcel.SenderId, Name = dal.GetCustomer(dalParcel.SenderId).Name};
                parcel.Target = new CustomerInParcel()
                    {Id = dalParcel.TargetId, Name = dal.GetCustomer(dalParcel.TargetId).Name};
                parcel.Weight = (WeightCategories) dalParcel.Weight;
                parcel.Priority = (Priorities) dalParcel.Priority;

                foreach (var drone in listDrones)
                    if (drone.Id == dalParcel.DroneId)
                        parcel.DroneInParcel = new DroneInParcel()
                        {
                            Id = drone.Id,
                            Battery = drone.Battery,
                            Location = new Location()
                            {
                                Longitude = drone.Location.Longitude,
                                Latitude = drone.Location.Latitude
                            }
                        };

                parcel.Requested = dalParcel.Requested;
                parcel.Scheduled = dalParcel.Scheduled;
                parcel.PickedUp = dalParcel.PickedUp;
                parcel.Delivered = dalParcel.Delivered;
            }

            return parcel;
        }

        /// <summary>
        /// return the list of parcels in special entity for show
        /// </summary>
        /// <returns></returns>
       
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcels()
        {
            // Must use list and foreach because we need to find out what the status of the parcel is
            List<ParcelToList> parcels = new List<ParcelToList>();
            lock (dal)
            {
                foreach (var dalParcel in dal.GetParcels(parcel => parcel.Deleted == false))
                {
                    ParcelToList newParcel = new ParcelToList();

                    newParcel.Id = dalParcel.Id;
                    newParcel.SenderName = dal.GetCustomer(dalParcel.SenderId).Name;
                    newParcel.TargetName = dal.GetCustomer(dalParcel.TargetId).Name;
                    newParcel.Weight = (WeightCategories)dalParcel.Weight;
                    newParcel.Priority = (Priorities)dalParcel.Priority;

                    if (dalParcel.Requested != null)
                        newParcel.Status = ParcelStatuses.Requested;
                    if (dalParcel.Scheduled != null)
                        newParcel.Status = ParcelStatuses.Scheduled;
                    if (dalParcel.PickedUp != null)
                        newParcel.Status = ParcelStatuses.PickedUp;
                    if (dalParcel.Delivered != null)
                        newParcel.Status = ParcelStatuses.Delivered;

                    parcels.Add(newParcel);
                }
            }
            return parcels;
        }

        /// <summary>
        /// Returning the list of parcels with no drones in a special entity "Parcel to list".
        /// </summary>
        /// <returns></returns>
     
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsNoDrones()
        {
            return from parcel in GetParcels()
                   where parcel.Status == ParcelStatuses.Requested // just parcels that dont have them drone.
                   select parcel;
        }

        /// <summary>
        /// return the list of parcels in special status for show
        /// </summary>
        /// <returns></returns>
      
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<ParcelToList> GetParcelsByStatus(ParcelStatuses status)
        {
            IEnumerable<ParcelToList> parcels = GetParcels().Where(parcel => parcel.Status == status);
            return parcels;
        }

        /// <summary>
        /// return the list after groping
        /// </summary>
        /// <returns></returns>
     
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<IGrouping<string, ParcelToList>> GetParcelsByGroupCustomers(string typeCustomer)
        {
            if(typeCustomer == "System.Windows.Controls.ComboBoxItem: Sender")
                return GetParcels().GroupBy(parcel => parcel.SenderName);
            if(typeCustomer == "System.Windows.Controls.ComboBoxItem: Target")
                return GetParcels().GroupBy(parcel => parcel.TargetName);
            return null;
        }

        /// <summary>
        /// check the input in add parcel to list
        /// </summary>
        /// <param name="parcel"></param>
        private void CheckParcel(Parcel parcel)
        {
            lock (dal)
            {
                try
                {
                    dal.GetCustomer(parcel.Sender.Id);
                }
                catch (DO.IdNotFoundException)
                {
                    throw new IdException("ERROR: the sender customer not found! ");
                }

                try
                {
                    dal.GetCustomer(parcel.Target.Id);
                }
                catch (DO.IdNotFoundException)
                {
                    throw new IdException("ERROR: the target customer not found! ");
                }
            }

            if (parcel.Sender.Id == parcel.Target.Id)
                throw new IdException("ERROR: the sender customer and the target customer are same! ");
        }
    }
}