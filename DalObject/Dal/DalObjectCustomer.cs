﻿using System;
using System.Collections.Generic;
using System.Linq;
using DO;
using System.Runtime.CompilerServices;


namespace Dal
{
    partial class DalObject : DalApi.IDal
    {
        /// <summary>
        /// add a customer to the customers list
        /// </summary>
        /// <param Name="newCustomer"></the new customer the user whants to add to the customer's list>
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void AddCustomer(Customer newCustomer)
        {
            string check = IsExistCustomer(newCustomer.Id);
            if (check == "not exists")
                DataSource.Customers.Add(newCustomer);
            if(check == "exists")
                throw new IdExistException("ERROR: the customer is exist");
            if(check == "was exists")
                throw new IdExistException("ERROR: the customer was exist");
        }

        /// <summary>
        /// Removes a customer from the list of customers.
        /// </summary>
        /// <param name="customerId"></param>
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void RemoveCustomer(int customerId)
        {
            string check = IsExistCustomer(customerId);

            if (check == "not exists")
                throw new IdNotFoundException("ERROR: the customer is not found!\n");
            if (check == "was exists")
                throw new IdExistException("ERROR: the customer was exist");

            if (check == "exists")
            {
                for (int i = 0; i < DataSource.Customers.Count(); i++)
                {
                    Customer elementCustomer = DataSource.Customers[i];
                    if (elementCustomer.Id == customerId)
                    {
                        elementCustomer.Deleted = true;
                        DataSource.Customers[i] = elementCustomer;
                    }
                }
            }
        }

        /// <summary>
        /// return the specific customer the user ask for
        /// </summary>
        /// <param Name="customerId"></the Id of the customer the user ask for>
        /// <returns></returns>
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public Customer GetCustomer(int customerId)
        {
            string check = IsExistCustomer(customerId);
            Customer customer = new Customer();
            if (check == "exists")
            {
                customer = DataSource.Customers.Find(elementCustomer => elementCustomer.Id == customerId);
            }
            if (check == "not exists")
                throw new IdNotFoundException("ERROR: the coustomer is not exit.");
            if (check == "was exists")
                throw new IdExistException("ERROR: the customer was exist");

            return customer;
        }

        /// <summary>
        /// return all the customer list
        /// </summary>
        /// <returns></returns>
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public IEnumerable<Customer> GetCustomers(Predicate<Customer> customerPredicate)
        {
            IEnumerable<Customer> customers = DataSource.Customers.Where(customer => customerPredicate(customer)); 
            return customers;
        }

        /// <summary>
        /// the method update the customer data
        /// </summary>
        /// <param Name="customer"></the customer to updata>
        /// <returns></returns>
        
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void UpdateCustomer(Customer customer)
        {
            for (int i = 0; i < DataSource.Customers.Count(); i++)
                if (DataSource.Customers[i].Id == customer.Id)
                    DataSource.Customers[i] = customer;
        }

        /// <summary>
        /// the method not need exception because she use both sids(true and false)
        /// </summary>
        /// <param Name="customerId"></the id of the customer>
        /// <returns></returns>
        private string IsExistCustomer(int customerId)
        {
            foreach (Customer elementCustomer in DataSource.Customers)
            {

                if (elementCustomer.Id == customerId && elementCustomer.Deleted == false)
                    return "exists";
                if (elementCustomer.Id == customerId && elementCustomer.Deleted == true)
                    return "was exists";
            }
            return "not exists"; // the customer not exist
        }
    }
}