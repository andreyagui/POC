using Microsoft.AspNetCore.Mvc;
using POC.API.Controllers;
using POC.Domain.Entities;
using POC.Infrastructure.UnitOfWork.Interfaces;

namespace POC.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountriesController : GenericController<Country>
    {
        public CountriesController(IGenericUnitOfWork<Country> unit) : base(unit)
        {
        }
    }

}