using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace newJson
{
    class Program
    {
        static void Main(string[] args)
        {
            //main list of lists with string objects
            List<List<string>> objectToTxt = new List<List<string>>();

            var customers = new List<Customer>
            {
                new Customer
                {
                    CustomerId = 1,
                    CustomerName = "Tom",
                    Email = "tom@gmail.com",
                    Age = 29,
                    Money = 23545.32,
                    CountryCode = "USA"
                },

                new Customer
                {
                    CustomerId = 2,
                    CustomerName = "Jerry",
                    Email = "jerry@gmail.com",
                    Age = 24,
                    Money = 18523.97,
                    CountryCode = "USA"
                },

                new Customer
                {
                    CustomerId = 3,
                    CustomerName = "Monica",
                    Email = "monica@gmail.com",
                    Age = 45,
                    Money = 4613589.12,
                    CountryCode = "CAN"
                }
            };

            //serialize C# object to Json object
            var customersJson = JsonConvert.SerializeObject(customers, Formatting.Indented);
            Console.WriteLine(customersJson);

            //deserialize Json object to C# object
            var customersList = JsonConvert.DeserializeObject<List<Customer>>(customersJson);

            //group by chosen property
            var groupedCustomers = customersList.GroupBy(x => x.CountryCode);

            // Get properties names:
            List<string> propNames = new List<string>();
            var propertiesNames = typeof(Customer).GetProperties();
            foreach (var item in propertiesNames)
            {
                //add property name to list
                propNames.Add(item.Name.ToString());
            }

            //then add properties list to main list
            objectToTxt.Add(propNames);

            //add customer object string list to main list for every customer in groupedCustomers
            foreach (var cust in groupedCustomers)
            {
                objectToTxt.Add(new List<string>
                {
                    //get actual customer properties values by calling First() on current customer object from deserialized list
                    //best time to sum values for same objects: First() -> Sum(and some property here)
                    cust.First().CustomerId.ToString(),
                    cust.First().CustomerName.ToString(),
                    cust.First().Email.ToString(),
                    cust.First().Age.ToString(),
                    cust.First().Money.ToString(),
                    cust.First().CountryCode.ToString(),
                });
            }

            //write to txt file iterating over whole list of lists of strings
            using (var writer = new StreamWriter("customers.txt"))
            {
                foreach (var obj in objectToTxt)
                {
                    foreach (var item in obj)
                    {
                        writer.Write(item + ",");
                    }
                    writer.WriteLine();
                }
            }
        }

        [Serializable]
        public class Customer
        {
            public int CustomerId { get; set; }
            public string CustomerName { get; set; }
            public string Email { get; set; }
            public int Age { get; set; }
            public double Money { get; set; }
            public string CountryCode { get; set; }
        }
    }
}
