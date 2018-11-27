using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using labfix.netcore.api.mssql.Models;

namespace labfix.netcore.api.mssql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CustomersController : ControllerBase
    {
       private NORTHWNDContext _dbContext;
       public CustomersController(NORTHWNDContext dbContext){
         _dbContext = dbContext;
       }

      // GET api/customers
      [HttpGet]
      public IActionResult Customers(){
        var customers = _dbContext.Customers.ToList();

        return Ok(customers);
      }

    }
}