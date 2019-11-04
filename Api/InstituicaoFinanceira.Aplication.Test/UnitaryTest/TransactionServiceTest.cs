using InstituicaoFinanceira.Service.Services;
using Xunit;

namespace InstituicaoFinanceira.Aplication.Test.UnitaryTest
{
    public class TransactionServiceTest
    {
        private TransactionService<Transaction> service = new TransactionService<Transaction>();
        private Transaction transaction = new Transaction();

        [Fact]
        public void TransactionService_InsertJuros()
        {
            // Arrange

            // Act
            var result = service.InsertJuros(transaction);
            // Assert
            Assert.True(result);
        }
    }
}
