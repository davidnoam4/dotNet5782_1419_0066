﻿using System;
using System.Collections.Generic;
using BO;

namespace ConsoleBL
{
    internal class Program
    {
        private enum Option
        {
            Add = 1,
            Update,
            View,
            ListView,
            Exit
        };

        private enum EntityOption
        {
            Station = 1,
            Drone,
            Customer,
            Parcel,
            Exit
        };

        private enum OptionListView
        {
            ListStations = 1,
            ListDrones,
            ListCustomers,
            ListParcels,
            ListParcelsNoDrones,
            ListStationsCharge,
            Exit
        };

        private enum OptionUpdate
        {
            ModelDrone = 1,
            DataStation,
            DataCustomer,
            SendDroneToDroneCharge,
            ReleaseDroneFromDroneCharge,
            ConnectParcelToDrone,
            CollectionParcelByDrone,
            SupplyParcelByDrone,
            Exit
        };

        private static void Main(string[] args)
        {
            BlApi.IBL bl = BlApi.BlFactory.GetBl();
            int c, myId;
            Option op;
            EntityOption ep;
            OptionListView olv;
            OptionUpdate ou;
            do
            {
                do
                {
                    Console.WriteLine("HELLO\n" + "Choose one of the following:\n" + "1: Add\n" + "2: Update\n" +
                                      "3: View\n" + "4: List View\n" + "5: Exit\n");
                } while (!int.TryParse(Console.ReadLine(), out c));

                op = (Option)c;
                switch (op)
                {
                    case Option.Add:
                        do
                        {
                            Console.WriteLine("Choose one of the entity:\n" + "1: Station\n" + "2: Drone\n" +
                                              "3: Customer\n" + "4: Parcel\n" + "5: Exit\n");
                        } while (!int.TryParse(Console.ReadLine(), out c));

                        ep = (EntityOption)c;
                        switch (ep)
                        {
                            case EntityOption.Station:
                                AddStation(bl);
                                break;
                            case EntityOption.Drone:
                                AddDrone(bl);
                                break;
                            case EntityOption.Customer:
                                AddCustomer(bl);
                                break;
                            case EntityOption.Parcel:
                                AddParcel(bl);
                                break;
                            case EntityOption.Exit:
                                break;
                            default:
                                break;
                        }

                        break;
                    case Option.Update:
                        do
                        {
                            Console.WriteLine("Choose one of to update:\n" + "1: ModelDrone:\n" +
                                              "2: DataStation\n" + "3: DataCustomer\n" +
                                              "4: Send Drone To Drone Charge\n" +
                                              "5: Release Drone From Drone Charge\n" + "6: ConnectParcelToDrone\n" +
                                              "7: CollectionParcelByDrone\n" + "8: SupplyParcelByDrone\n" +
                                              "9: Exit\n");
                        } while (!int.TryParse(Console.ReadLine(), out c));

                        ou = (OptionUpdate)c;
                        switch (ou)
                        {
                            case OptionUpdate.ModelDrone:
                                UpdateDroneModel(bl);
                                break;
                            case OptionUpdate.DataStation:
                                UpdateDataStation(bl);
                                break;
                            case OptionUpdate.DataCustomer:
                                UpdateDataCustomer(bl);
                                break;
                            case OptionUpdate.SendDroneToDroneCharge:
                                SendDroneToCharge(bl);
                                break;
                            case OptionUpdate.ReleaseDroneFromDroneCharge:
                                ReleaseDroneFromCharge(bl);
                                break;
                            case OptionUpdate.ConnectParcelToDrone:
                                ConnectParcelToDrone(bl);
                                break;
                            case OptionUpdate.CollectionParcelByDrone:
                                CollectionParcelByDrone(bl);
                                break;
                            case OptionUpdate.SupplyParcelByDrone:
                                SupplyParcelByDrone(bl);
                                break;
                            case OptionUpdate.Exit:
                                break;
                            default:
                                break;
                        }

                        break;
                    case Option.View:
                        do
                        {
                            Console.WriteLine("Choose one of the entity:\n" + "1: Station\n" + "2: Drone\n" +
                                              "3: Customer\n" + "4: Parcel\n" + "5: Exit\n");
                        } while (!int.TryParse(Console.ReadLine(), out c));

                        ep = (EntityOption)c;
                        myId = 0;
                        if (c != 5)
                            do
                            {
                                Console.WriteLine("Enter Id of the entity:\n");
                            } while (!int.TryParse(Console.ReadLine(), out myId));

                        switch (ep)
                        {
                            case EntityOption.Station:
                                try
                                {
                                    Console.WriteLine(bl.GetStation(myId));
                                }
                                catch (IdException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case EntityOption.Drone:
                                try
                                {
                                    Console.WriteLine(bl.GetDrone(myId));
                                }
                                catch (IdException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case EntityOption.Customer:
                                try
                                {
                                    Console.WriteLine(bl.GetCustomer(myId));
                                }
                                catch (IdException e)
                                {
                                    Console.WriteLine(e.Message);

                                }
                                break;
                            case EntityOption.Parcel:
                                try
                                {
                                    Console.WriteLine(bl.GetParcel(myId));
                                }
                                catch (IdException e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case EntityOption.Exit:
                                break;
                            default:
                                break;
                        }

                        break;
                    case Option.ListView:
                        do
                        {
                            Console.WriteLine("Choose one of the entity:\n" + "1: List Stations\n" +
                                              "2: List Drones\n" + "3: List Customers\n" + "4: List Parcels\n" +
                                              "5: List Parcels No Drones\n" + "6: List Stations Charge\n" +
                                              "7:Exit\n");
                        } while (!int.TryParse(Console.ReadLine(), out c));

                        olv = (OptionListView)c;
                        switch (olv)
                        {
                            case OptionListView.ListStations:
                                foreach (var elementStation in bl.GetStations())
                                    Console.WriteLine(elementStation);
                                break;
                            case OptionListView.ListDrones:
                                foreach (var elementDrone in bl.GetDrones())
                                    Console.WriteLine(elementDrone);
                                break;
                            case OptionListView.ListCustomers:
                                foreach (var elementCustomer in bl.GetCustomers())
                                    Console.WriteLine(elementCustomer);
                                break;
                            case OptionListView.ListParcels:
                                foreach (var elementParcel in bl.GetParcels())
                                    Console.WriteLine(elementParcel);
                                break;
                            case OptionListView.ListParcelsNoDrones:
                                foreach (var elementParcelsNoDrone in bl.GetParcelsNoDrones())
                                    Console.WriteLine(elementParcelsNoDrone);
                                break;
                            case OptionListView.ListStationsCharge:
                                foreach (var elementStationCharge in bl.GetStationsWithAvailableCharge())
                                    Console.WriteLine(elementStationCharge);
                                break;
                            case OptionListView.Exit:
                                break;
                            default:
                                break;
                        }

                        break;
                    case Option.Exit:
                        break;
                    default:
                        break;
                }
            }
            while (op != Option.Exit);
        }


        /// <summary>
        /// read from the user station to insert to list
        /// </summary>
        /// <returns></no returns, just read from user>
        private static void AddStation(BlApi.IBL bl)
        {
            int num1;
            double num2;
            Location stationLocation = new Location();
            Station newStation = new Station();
            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id with 6 digits of Station: ");
                } while (!int.TryParse(Console.ReadLine(), out num1));

                newStation.Id = num1;

                Console.WriteLine("Enter Name Station: ");
                newStation.Name = Console.ReadLine();

                do
                {
                    Console.WriteLine("Enter longitude between -1 to 1 to Station: ");

                } while (!double.TryParse(Console.ReadLine(), out num2));

                stationLocation.Longitude = num2;

                do
                {
                    Console.WriteLine("Enter Latitude between -1 to 1 to Station: ");

                } while (!double.TryParse(Console.ReadLine(), out num2));

                stationLocation.Latitude = num2;
                newStation.Location = stationLocation;

                do
                {
                    Console.WriteLine("Enter Available Charge Slots  Station: ");

                } while (!int.TryParse(Console.ReadLine(), out num1));

                newStation.AvailableChargeSlots = num1;

                newStation.DronesInCharges = new List<DroneInCharge>();

                try
                {
                    bl.AddStation(newStation);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ChargeSlotsException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (LocationException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to add station? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }

        /// <summary>
        /// read fron the user drone to insert to list
        /// </summary>
        /// <returns></no returns, just read from user>
        private static void AddDrone(BlApi.IBL bl)
        {
            int num;
            Drone newDrone = new Drone();
            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id with 6 digits of Drone: ");
                } while (!int.TryParse(Console.ReadLine(), out num));

                newDrone.Id = num;

                Console.WriteLine("Enter Model Drone: ");
                newDrone.Model = Console.ReadLine();

                do
                {
                    Console.WriteLine("Enter Weight Drone:\n" + "1: Light\n" + "2: Medium\n" + "3: Heavy\n");
                    int.TryParse(Console.ReadLine(), out num);
                } while (num != 1 && num != 2 && num != 3);

                switch (num)
                {
                    case 1:
                        newDrone.Weight = WeightCategories.Light;
                        break;
                    case 2:
                        newDrone.Weight = WeightCategories.Medium;
                        break;
                    case 3:
                        newDrone.Weight = WeightCategories.Heavy;
                        break;
                    default:
                        break;
                }

                do
                {
                    Console.WriteLine("Enter id of station to put the drone in: ");
                } while (!int.TryParse(Console.ReadLine(), out num));

                try
                {
                    bl.AddDrone(newDrone, num);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ModelException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (StatusDroneException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to add drone? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }

        /// <summary>
        /// read fron the user customer to insert to list
        /// </summary>
        /// <returns></no returns, just read from user>
        private static void AddCustomer(BlApi.IBL bl)
        {
            int num1;
            double num2;
            Location customerLocation = new Location();
            Customer newCustomer = new Customer();

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id with 8 digits of Customer: ");
                } while (!int.TryParse(Console.ReadLine(), out num1));

                newCustomer.Id = num1;

                Console.WriteLine("Enter Name Customer: ");
                newCustomer.Name = Console.ReadLine();

                Console.WriteLine("Enter Phone Customer: ");
                newCustomer.Phone = Console.ReadLine();

                do
                {
                    Console.WriteLine("Enter Longitude between -1 to 1 to Customer: ");
                } while (!double.TryParse(Console.ReadLine(), out num2));

                customerLocation.Longitude = num2;

                do
                {
                    Console.WriteLine("Enter Latitude between -1 to 1 to Customer: ");
                } while (!double.TryParse(Console.ReadLine(), out num2));

                customerLocation.Latitude = num2;
                newCustomer.Location = customerLocation;

                try
                {
                    bl.AddCustomer(newCustomer);
                    Console.WriteLine("Success! :)\n");
                    break;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Do you want to add customer? please enter yes or no.");
                    string choice = Console.ReadLine();
                    if (choice != "yes")
                        break;
                }
            }
        }

        /// <summary>
        /// read from the user parcel to insert to list
        /// </summary>
        /// <returns></no returns, just read from user>
        private static void AddParcel(BlApi.IBL bl)
        {
            int num;
            Parcel newParcel = new Parcel();

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Sender Id Parcel: ");
                } while (!int.TryParse(Console.ReadLine(), out num));

                newParcel.Sender = new CustomerInParcel();
                newParcel.Sender.Id = num;

                do
                {
                    Console.WriteLine("Enter Target Id Parcel: ");
                } while (!int.TryParse(Console.ReadLine(), out num));

                newParcel.Target = new CustomerInParcel();
                newParcel.Target.Id = num;

                do
                {
                    Console.WriteLine("Enter Weight Parcel:\n" + "1: Light\n" + "2: Medium\n" + "3: Heavy\n");
                    int.TryParse(Console.ReadLine(), out num);
                } while (num != 1 && num != 2 && num != 3);

                switch (num)
                {
                    case 1:
                        newParcel.Weight = WeightCategories.Light;
                        break;
                    case 2:
                        newParcel.Weight = WeightCategories.Medium;
                        break;
                    case 3:
                        newParcel.Weight = WeightCategories.Heavy;
                        break;
                    default:
                        break;
                }

                do
                {

                    Console.WriteLine("Enter Priority Parcel:\n" + "1: Normal\n" + "2: Fast\n" + "3: Emergency\n");
                    int.TryParse(Console.ReadLine(), out num);
                } while (num != 1 && num != 2 && num != 3);

                switch (num)
                {
                    case 1:
                        newParcel.Priority = Priorities.Normal;
                        break;
                    case 2:
                        newParcel.Priority = Priorities.Fast;
                        break;
                    case 3:
                        newParcel.Priority = Priorities.Emergency;
                        break;
                    default:
                        break;
                }

                try
                {
                    bl.AddParcel(newParcel);
                    Console.WriteLine("Success! :)\n");
                    break;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine("Do you want to add parcel? please enter yes or no.");
                    string choice = Console.ReadLine();
                    if (choice != "yes")
                        break;
                }
            }
        }
        /// <summary>
        /// update the drone model 
        /// </summary>
        /// <returns></no returns, just update the model>
        private static void UpdateDroneModel(BlApi.IBL bl)
        {
            int id;
            string model;
            while (true)
            {
                do
                {
                    Console.WriteLine("Enter id drone: ");
                } while (!int.TryParse(Console.ReadLine(), out id));

                Console.WriteLine("Enter the new model drone: ");
                model = Console.ReadLine();

                try
                {
                    bl.UpdateDroneModel(id, model);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (ModelException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to update the model of the drone? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }
        /// <summary>
        /// update the data of the station the user ask 
        /// </summary>
        /// <returns></no returns, just update the station>
        private static void UpdateDataStation(BlApi.IBL bl)
        {
            int id, chargeSlots;
            string name;

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id Station: ");
                } while (!int.TryParse(Console.ReadLine(), out id));

                Console.WriteLine("Note! Enter at least one of the following data:\n");

                Console.WriteLine(
                        "If you want to update the name of the station, Enter name:\n if you don't want, press Enter: \n");
                name = Console.ReadLine();
                do
                {
                    Console.WriteLine(
                        "If you want to update the number of charge slots in the station, Enter the amount:\n if you don't want, Enter -1: \n");
                } while (!int.TryParse(Console.ReadLine(), out chargeSlots));

                try
                {
                    bl.UpdateDataStation(id, name, chargeSlots);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (NameException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to update the data of the station? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }
        /// <summary>
        /// update the data of the customer the user ask 
        /// </summary>
        /// <returns></no returns, just update the customer>
        private static void UpdateDataCustomer(BlApi.IBL bl)
        {
            int id;
            string name, phone;

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id Customer: ");
                } while (!int.TryParse(Console.ReadLine(), out id));

                Console.WriteLine("Note! Enter at least one of the following data:\n");

                Console.WriteLine(
                    "If you want to update the name of the customer, Enter name:\n if you don't want, press Enter: \n");
                name = Console.ReadLine();

                Console.WriteLine(
                    "If you want to update the phone of the customer, Enter phone:\n if you don't want, press Enter: \n");
                phone = Console.ReadLine();

                try
                {
                    bl.UpdateDataCustomer(id, name, phone);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (NameException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (PhoneException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to update the data of the customer? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }

        /// <summary>
        /// send the drone to station with available drone charge
        /// </summary>
        /// <returns></no returns, just send the drone to station with available drone charge>
        private static void SendDroneToCharge(BlApi.IBL bl)
        {
            int id;

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id Drone: ");
                } while (!int.TryParse(Console.ReadLine(), out id));

                try
                {
                    bl.SendDroneToDroneCharge(id);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (StatusDroneException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to send the drone to charge? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }

        /// <summary>
        /// Release the drone from the charge spot in the station 
        /// </summary>
        /// <returns></no returns, just release the drone from the charge spot in the station>
        private static void ReleaseDroneFromCharge(BlApi.IBL bl)
        {
            int id;

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id Drone: ");
                } while (!int.TryParse(Console.ReadLine(), out id));

                try
                {
                    bl.ReleaseDroneFromDroneCharge(id);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (StatusDroneException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to release the drone from charge? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }

        /// <summary>
        /// connect the parcel to available drone
        /// </summary>
        /// <returns></no returns, just release connect the parcel to available drone>
        private static void ConnectParcelToDrone(BlApi.IBL bl)
        {
            int droneId;

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id Drone: ");
                } while (!int.TryParse(Console.ReadLine(), out droneId));

                try
                {
                    bl.ConnectParcelToDrone(droneId);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (StatusDroneException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (NoParcelsToDroneException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to connect parcel to drone or do know about drone that can do any connect to some parcel?\n" +
                                      "if you do please enter yes, else no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }
        /// <summary>
        /// Collect a parcel by drone
        /// </summary>
        /// <returns></no returns, just collect a parcel by drone>
        private static void CollectionParcelByDrone(BlApi.IBL bl)
        {
            int droneId;

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id Drone: ");
                } while (!int.TryParse(Console.ReadLine(), out droneId));

                try
                {
                    bl.CollectionParcelByDrone(droneId);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (StatusDroneException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("Do you want to collect the parcel by drone? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }
        /// <summary>
        /// Supply a parcel by drone
        /// </summary>
        /// <returns></no returns, supply a parcel by drone>
        private static void SupplyParcelByDrone(BlApi.IBL bl)
        {
            int droneId;

            while (true)
            {
                do
                {
                    Console.WriteLine("Enter Id Drone: ");
                } while (!int.TryParse(Console.ReadLine(), out droneId));

                try
                {
                    bl.SupplyParcelByDrone(droneId);
                    Console.WriteLine("Success! :)\n");
                    return;
                }
                catch (IdException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (StatusDroneException e)
                {
                    Console.WriteLine(e.Message);
                }
                Console.WriteLine("You need to choose drone that pick-up some parcel.\n" +
                                  "so do you want to supply parcel by drone? please enter yes or no.");
                string choice = Console.ReadLine();
                if (choice != "yes")
                    break;
            }
        }
    }
}