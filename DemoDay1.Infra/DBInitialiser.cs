using System;
using System.Collections.Generic;
using System.Text;
using DemoDay1.Domain.Models;
using System.Linq;

namespace DemoDay1.Infra
{
    public static class DBInitialiser
    {
        public static void Initialize(ContosoContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
                return;

            var products = new Product[]
            {
                new Product { Name = "Wilson Tour Premier", Price = 200},
                new Product { Name = "Wilson RF 97", Price = 15200},
                new Product { Name = "Head Sonic Pro", Price = 900},
                new Product { Name = "Babolat Syntac feel", Price = 400},
                new Product { Name = "Head Graphene Pro", Price = 12400}
            };

            foreach (Product product in products)
            {
                context.Products.Add(product);
            }

            var customers = new Customer[]
            {
                new Customer {Name = "Nauzad Kapadia", Address = "Mumbai" },
                new Customer {Name = "Shashi Wazir", Address = "Hyderabad" },
                new Customer {Name = "Gunjan Gulati", Address = "Delhi" }
            };

            foreach (Customer customer in customers)
            {
                context.Customers.Add(customer);
            }

            context.SaveChanges();
        }
    }
}
