using Microsoft.AspNetCore.Mvc;
using POC.Domain.Entities;
using POC.Infrastructure.UnitOfWork.Interfaces;

namespace POC.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : GenericController<Category>
    {
        public CategoriesController(IGenericUnitOfWork<Category> unit) : base(unit)
        {
        }
    }
}