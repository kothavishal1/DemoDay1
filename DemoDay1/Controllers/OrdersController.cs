using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoDay1.Commands;
using DemoDay1.Domain.Models;
using DemoDay1.Infra;
using DemoDay1.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DemoDay1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public readonly IUnitOfWork _unitofwork;
        public readonly IMediator Mediator;
        public OrdersController(IUnitOfWork unitofwork, IMediator mediator)
        {
            _unitofwork = unitofwork;
            Mediator = mediator;
        }
        [HttpGet("Get")]
        public IActionResult Get()
        {
            var orders = _unitofwork.GetRepository<Order>().Query().Include("Customer").ToList();
            var viewmOdel = orders.Select(o => {
                return new OrderViewModel
                {
                    Id = o.Id,
                OrderDate = o.OrderDate,
                CustomerId = o.CustomerId,
                CustomerName = o.Customer.Name
                };
            });
            return Ok(viewmOdel);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var order = _unitofwork.GetRepository<Order>().Query().Where(c => c.CustomerId == id).Single();
            return Ok(order);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAsync(CreateOrderCommand orderCommand)
        {
            int retValue = await Mediator.Send(orderCommand);
            return Created("", retValue);
        }
    }
}