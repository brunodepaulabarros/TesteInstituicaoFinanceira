using InstituicaoFinanceira.Service.Services;
using InstituicaoFinanceira.Service.Validators;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InstituicaoFinanceira.Aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private BaseService<Transaction> service = new BaseService<Transaction>();

        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            try
            {
                service.Post<TransactionValidator>(transaction);

                return new ObjectResult(transaction.Id);
            }
            catch (ArgumentNullException ex)
            {
                return NotFound(ex);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok();
                //return new ObjectResult(service.Get());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}