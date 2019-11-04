using InstituicaoFinanceira.Service.Services;
using Microsoft.AspNetCore.Mvc;
using System;

namespace InstituicaoFinanceira.Aplication.Controllers
{
    class Balance
    {
        public double value { get; set; }
    }
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
                    Balance balance = new Balance();
                    var result = transactionService.SelectBalance();
                    balance.value = result;
                    return new ObjectResult(balance);
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