using DemoDay1.Domain.Models;
using DemoDay1.Infra;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DemoDay1.Commands
{
    public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand,int>
    {
        public readonly IUnitOfWork _unitofwork;

        public CreateOrderCommandHandler(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public async Task<int> Handle(CreateOrderCommand message, CancellationToken cancellationToken)
        {
            Order _order = new Order();
            _order.AddOrder(message.CustomerId);
            foreach (var item in message.OrderItems)
            {
                var product = _unitofwork.GetRepository<Product>().Query().Where(c => c.Id == item.ProductId).Single();
                _order.AddOrderItem(item.ProductId, item.Quantity, product.Price);
            }

            _unitofwork.GetRepository<Order>().Add(_order);
            return await _unitofwork.Commit();
        }
    }
}
