using FluentAssertions;
using InstituicaoFinanceira.Aplication.Test.Fixtures;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;
using Newtonsoft.Json;
using System.Text;
using System;

namespace InstituicaoFinanceira.Aplication.Test.Scenarios
{
    public class TransactionTest
    {
        private readonly TestContext _testContext;
        private Transaction transaction = new Transaction();
        public TransactionTest()
        {
            _testContext = new TestContext();
        }

        [Fact]
        public async Task Transaction_GetSaldo_ReturnsOkResponse()
        {
            // Arrange

            // Act
            var response = await _testContext.Client.GetAsync("/api/Transactions?tipoRequisicao=Saldo");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Transaction_GetExtrato_ReturnsOkResponse()
        {
            // Arrange

            // Act
            var response = await _testContext.Client.GetAsync("/api/Transactions?tipoRequisicao=Extrato");
            response.EnsureSuccessStatusCode();

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Trasanction_Get_ReturnsBadRequestResponse()
        {
            // Arrange

            // Act
            var response = await _testContext.Client.GetAsync("/api/Transactions?xxx");
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task Trasanction_Post_ReturnsOkResponse()
        {
            // Arrange
            transaction.Description = "Deposito";
            transaction.Value = 10.00;
            transaction.Created_at = DateTime.Now;
            var json = JsonConvert.SerializeObject(transaction);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await _testContext.Client.PostAsync("/api/Transactions", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public async Task Trasanction_Post_ReturnsBadRequestResponse()
        {
            // Arrange
            transaction.Description = "";
            transaction.Value = 0;
            transaction.Created_at = DateTime.Now;
            var json = JsonConvert.SerializeObject(transaction);
            var stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

            // Act
            var response = await _testContext.Client.PostAsync("/api/Transactions", stringContent);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

    }
}
