using InstituicaoFinanceira.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstituicaoFinanceira.Infra.Data.Repository
{
    public class TransactionRepository<T> : BaseRepository<T> where T : Transaction
    {
        private MySqlContext context = new MySqlContext();
        public double SelectBirthdayValue( DateTime date)
        {
            var query = context.Transaction
                      .Where(b =>  b.Created_at <= date)
                      .ToList().Sum((t => t.Value));

            return query;
        }
        public double SelectBalance()
        {
            var query = context.Transaction                      
                      .ToList().Sum((t => t.Value));

            return query;
        }
    }
}
