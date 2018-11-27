using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using labfix.netcore.api.mssql.Models;
using System.Text;

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

      // POST api/customers
      [HttpPost]
      public IActionResult Customers(Customers customers){

        if( ModelState.IsValid ){

          customers.CustomerId = GenerateAutoId(4);
          _dbContext.Customers.Add(customers);
          _dbContext.SaveChanges();

          return Ok();
        }else{
          return NotFound();
        }

      }

      public string GenerateAutoId(int length)
        {
            const string valid = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }

    }
}