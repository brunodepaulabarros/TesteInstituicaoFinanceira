using InstituicaoFinanceira.Infra.Data.Context;
using System;
using System.Linq;

namespace InstituicaoFinanceira.Infra.Data.Repository
{
    public class TransactionRepository<T> : BaseRepository<T> where T : Transaction
    {
        private MySqlContext context = new MySqlContext();
        public double SelectBirthdayValue(DateTime date)
        {
            var queryResult = context.Transaction
                      .Where(b => b.Created_at <= date)
                      .ToList().Sum((t => t.Value));

            return queryResult;
        }
        public double SelectBalance()
        {
            var queryResult = context.Transaction
                      .ToList().Sum((t => t.Value));

            return queryResult;
        }
    }
}
