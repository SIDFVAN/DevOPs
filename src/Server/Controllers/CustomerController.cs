using Blanche.Domain.Customers;
using Blanche.Domain.Customers.Mappers;
using Blanche.Shared.Customers;
using Blanche.Shared.Reservations;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace Blanche.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public CustomerController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public IActionResult Index()
	{
        throw new NotImplementedException();
    }

	[HttpGet("{id}")]
	public ActionResult Get(int id)
	{
        throw new NotImplementedException();
    }

	[HttpGet("List")]
	public ActionResult List()
	{
		return Ok(new());
	}

	[HttpPost]
	public ActionResult Create(CustomerDto customerDto)
	{
		EmailAddress emailAddress = new(customerDto.Email.ToString());

        var mapper = new CustomerMapper();
        Customer customer = mapper.ToCustomer(customerDto);
         
		Console.WriteLine(customer.ToString());
		return Ok(customer);
	}
}