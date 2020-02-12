using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;

namespace DemoDay1.Domain.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class Order 
    {

        
        public int Id { get; private set; }
        public DateTime OrderDate { get; private set; }
        public string Status { get; private set; }
        public int CustomerId { get; private set; }
        public Customer Customer { get; private set; }
        private List<OrderItem> _orderItems;
        public IReadOnlyCollection<OrderItem> OrderItem{ get => _orderItems; }

        public void AddOrder(int customerId)
        {
            CustomerId = customerId;
            OrderDate = DateTime.Now;
            Status = "New";
            _orderItems = new List<OrderItem>();
        }
        public void AddOrderItem(int productId, int quantity, decimal rate)
        {
            var existingOrder = _orderItems.SingleOrDefault(o => o.ProductId == productId);
            if (existingOrder != null)
            {
                existingOrder.SetQuantity(existingOrder.Quantity);
                existingOrder.setRate(existingOrder.Rate);
            }
            else
            {
                var orderItem = new OrderItem(productId, quantity, rate);
                _orderItems.Add(orderItem);
            }
        }

        public void SetAwaitingValidationStatus()
        {
            if (Status == "Submitted")
            {
                Status = "AwaitingValidation";

                //initiate payment processing workflow
            }
        }

        public void SetPaidStatus()
        {
            if (Status == "StockConfirmed")
            {
                Status = "Paid";

                //send payment confirmation email
            }
        }

        public void SetShippedStatus()
        {
            if (Status != "Paid" || Status != "Cancelled")
            {
                throw new ArgumentException($"Is not possible to change the order status from {Status} to Shipped");
            }

            Status = "Shipped";
        }

        public void SetCancelledStatus()
        {
            if (Status == "Paid" || Status == "Shipped")
            {
                throw new ArgumentException($"Is not possible to change the order status from {Status} to Cancelled");
            }

            Status = "Cancelled";
        }

    }

    public class OrderItem
    {
        public OrderItem(int productId, int quantity, decimal rate)
        {
            ProductId = productId;
            Quantity = quantity;
            Rate = rate;
        }
        public int Id { get; private set; }
        public int OrderId { get; private set; }
        public int ProductId { get; private set; }
        public int Quantity { get; private set; }
        public decimal Rate { get; private set; }

        //public Order Order { get; private set; }
        public Product Product { get; private set; }
        public void SetQuantity(int quantityId)
        {
            Quantity += quantityId;
        }
        public void setRate(decimal rate)
        {
            Rate = rate;
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

}
