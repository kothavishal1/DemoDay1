using DemoDay1.ViewModel;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoDay1.Commands
{
    public class CreateOrderCommand : IRequest<int>
    {
        //public DateTime OrderDate { get; private set; }
        public int CustomerId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }

        public CreateOrderCommand()
        {
            OrderItems = new List<OrderItemViewModel>();
        }

        public CreateOrderCommand(List<OrderItemViewModel> orderItems, DateTime orderDate, int customerId)
        {
            OrderItems = orderItems;
            //OrderDate = orderDate;
            CustomerId = customerId;
        }
    }
}
