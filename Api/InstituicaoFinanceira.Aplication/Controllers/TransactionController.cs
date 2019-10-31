using InstituicaoFinanceira.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InstituicaoFinanceira.Aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private TransactionService<Transaction> serviceTransaction = new TransactionService<Transaction>();

        [HttpPost]
        public IActionResult Post([FromBody] Transaction transaction)
        {
            try
            {
                if (transaction.Value == 0)
                    throw new ArgumentException("O valor não pode ser 0.");
                if (transaction.Description == "" || transaction.Description == null)
                    throw new ArgumentException("Informe a descrição.");

                transaction.Created_at = DateTime.Now;

                serviceTransaction.Post(transaction);

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
        public IActionResult Get([FromQuery] string tipoRequisicao)
        {
            if (tipoRequisicao == "Extrato")
            {
                try
                {
                    return new ObjectResult(serviceTransaction.GetAll());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            if (tipoRequisicao == "Saldo")
            {
                try
                {
                    return new ObjectResult(serviceTransaction.SelectBalance());
                }
                catch (Exception ex)
                {
                    return BadRequest(ex);
                }
            }
            return Ok();
        }
    }
}