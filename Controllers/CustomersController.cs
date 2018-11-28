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

      // GET api/customers ==>  Select
      [HttpGet]
      public IActionResult Customers(){
        var customers = _dbContext.Customers.ToList();
        return Ok(customers);
      }

      // POST api/customers ==> Insert
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

      // PUT /api/customer/{id} ==> Update
      [HttpPut("{id}")]
      public IActionResult Customers(string id, Customers customers){
        var _customers = _dbContext.Customers.FirstOrDefault( m => m.CustomerId == id);
        if(_customers == null ){return NotFound(); }

        if(ModelState.IsValid){
          _customers.ContactName = customers.ContactName;
          _customers.CompanyName = customers.CompanyName;
          _customers.ContactName = customers.ContactName;
          _customers.ContactTitle = customers.ContactTitle;
          _customers.PostalCode = customers.PostalCode;
          _customers.Country = customers.Country;

          _dbContext.Customers.Update(_customers);
          _dbContext.SaveChanges();

          return NoContent();

        }else{
          return NotFound();
        }

      }

      [HttpDelete("{id}")]
      public IActionResult Customers(string id){

        var _customers = _dbContext.Customers.FirstOrDefault(m => m.CustomerId == id);

        if(_customers == null){ return NotFound();}

        _dbContext.Customers.Remove(_customers);
        _dbContext.SaveChanges();

        return Ok();

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