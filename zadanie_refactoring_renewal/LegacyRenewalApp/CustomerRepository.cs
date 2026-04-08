using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyRenewalApp
{
    public class CustomerRepository
    {
        public static readonly Dictionary<int, Customer> Database = new Dictionary<int, Customer>
        {
            { 1, new Customer(1, "Anna Kowalska", "anna.kowalska@example.com", "Standard", "Poland", 1, 20, true) },
            { 2, new Customer(2, "Piotr Lis", "piotr.lis@example.com", "Gold", "Poland", 4, 140, true) },
            { 3, new Customer(3, "John Smith", "john.smith@example.com", "Platinum", "Germany", 7, 260, true) },
            { 4, new Customer(4, "School Lab", "it-admin@school.example.com", "Education", "Czech Republic", 3, 90, true) },
            { 5, new Customer(5, "Nordic Ventures", "finance@nordic.example.com", "Silver", "Norway", 2, 30, true) }
        };

        public Customer GetById(int customerId)
        {
            int randomWaitTime = new Random().Next(500);
            Thread.Sleep(randomWaitTime);

            if (Database.ContainsKey(customerId))
            {
                return Database[customerId];
            }

            throw new ArgumentException($"Customer with id {customerId} does not exist");
        }
    }
}