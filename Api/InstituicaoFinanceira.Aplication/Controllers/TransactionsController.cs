using InstituicaoFinanceira.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InstituicaoFinanceira.Aplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private TransactionService<Transaction> transactionService = new TransactionService<Transaction>();

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

                transactionService.Post(transaction);

                return new ObjectResult(transaction.Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string tipoRequisicao)
        {
            try
            {
                if (tipoRequisicao == "Extrato")
                {

                    return new ObjectResult(transactionService.GetAll());

                }
                if (tipoRequisicao == "Saldo")
                {

                    return new ObjectResult(transactionService.SelectBalance());
                }
                return BadRequest();
            }

            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}