using InstituicaoFinanceira.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstituicaoFinanceira.Infra.Data.Repository
{
    public class TransactionRepository<T> : BaseRepository<T> where T : Transaction
    {
        private MySqlContext context = new MySqlContext();
        public double SelectValueBirthday(DateTime dateTimeIni, DateTime dateTimeEnd)
        {
            var query = context.Transaction
                      .Where(b => b.Created_at >= dateTimeIni && b.Created_at <= dateTimeEnd)
                      .ToList().Sum((t=>t.Value));

            return query;
        }
    }
}
