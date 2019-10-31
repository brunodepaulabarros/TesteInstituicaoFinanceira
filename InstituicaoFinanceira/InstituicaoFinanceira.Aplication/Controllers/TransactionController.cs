using InstituicaoFinanceira.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InstituicaoFinanceira.Aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private TransactionService<Transaction> service = new TransactionService<Transaction>();

        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            try
            {
                if (transaction.Value == 0)
                    throw new ArgumentException("O valor não pode ser 0.");
                if (transaction.Description == ""|| transaction.Description == null)
                    throw new ArgumentException("Informe a descrição.");

                transaction.Created_at = DateTime.Now;
                
                service.Post(transaction);

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
                return new ObjectResult(service.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}