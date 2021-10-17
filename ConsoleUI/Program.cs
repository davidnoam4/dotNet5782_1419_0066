﻿using System;


namespace ConsoleUI
{
    class Program
    {
        public enum Option { Add = 1, Update, View, ListView, Exit};
        public enum EntityOption { Station = 1, Drone, Customer, Parcel, Exit};

        public enum OptionListView { ListStations = 1, ListDrones, ListCustomers, ListParcels, ListParcelsNoDrones, ListStationsCharge, Exit };

        static void Main(string[] args)
        {
            int c;
            Option op;
            EntityOption ep;
            OptionListView olv;
            Console.WriteLine("HELLO\n" + "Choose one of the following:\n" + "1: Add\n" + "2: Update\n" + "3: View\n" + "4: List View\n" + "5: Exit\n");
            int.TryParse(Console.ReadLine(), out c);
            op = (Option)c;
            do
	        {
                switch (op)
	            {
		            case Option.Add:
                        Console.WriteLine("Choose one of the entity:\n" + "1: Station\n" + "2: Drone\n" + "3: Customer\n" + "4: Parcel\n" + "5: Exit\n");
                        int.TryParse(Console.ReadLine(), out c);
                        ep = (EntityOption)c;
                        switch (ep)
	                    {
		                    case EntityOption.Station:
                                DalObject.DalObject.AddStation();
                                break;
                            case EntityOption.Drone:
                                DalObject.DalObject.AddDrone();
                                break;
                            case EntityOption.Customer:
                                DalObject.DalObject.AddCustomer();
                                break;
                            case EntityOption.Parcel:
                                DalObject.DalObject.AddParcel();
                                break;
                            case EntityOption.Exit:
                                break;
                            default:
                                break;
	                    }
                        break;
                    case Option.Update:
                        break;
                    case Option.View:
                        Console.WriteLine("Choose one of the entity:\n" + "1: Station\n" + "2: Drone\n" + "3: Customer\n" + "4: Parcel\n" + "5: Exit\n");
                        int.TryParse(Console.ReadLine(), out c);
                        ep = (EntityOption)c;
                        int MyId;
                        Console.WriteLine("Enter Id of the entity:\n");
                        int.TryParse(Console.ReadLine(), out MyId);
                        switch (ep)
                        {
                            case EntityOption.Station:
                                DalObject.DalObject.GetStation(MyId).ToString();
                                break;
                            case EntityOption.Drone:
                                DalObject.DalObject.GetDrone(MyId).ToString();
                                break;
                            case EntityOption.Customer:
                                DalObject.DalObject.GetCustomer(MyId).ToString();
                                break;
                            case EntityOption.Parcel:
                                DalObject.DalObject.GetParcel(MyId).ToString();
                                break;
                            case EntityOption.Exit:
                                break;
                            default:
                                break;
                        }
                        break;
                    case Option.ListView:
                        Console.WriteLine("Choose one of the entity:\n" + "1:List Stations\n" + "2:List Drones" + "3:List Customers" + "4:List Parcels" + "List Parcels No Drones" + "5:List Stations Charge" + "6:Exit");
                        int.TryParse(Console.ReadLine(), out c);
                        olv = (OptionListView)c;
                        switch (olv)
                        {
                            case OptionListView.ListStations:
                                break;
                            case OptionListView.ListDrones:
                                break;
                            case OptionListView.ListCustomers:
                                break;
                            case OptionListView.ListParcels:
                                break;
                            case OptionListView.ListParcelsNoDrones:
                                break;
                            case OptionListView.ListStationsCharge:
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
	        } while (op != Option.Exit);
        }
    }
}
