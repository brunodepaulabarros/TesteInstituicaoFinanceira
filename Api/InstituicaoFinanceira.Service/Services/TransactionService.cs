using InstituicaoFinanceira.Infra.Data.Context;
using InstituicaoFinanceira.Infra.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InstituicaoFinanceira.Service.Services
{
    public class TransactionService<T> : BaseService<T> where T : Transaction
    {
        private TransactionRepository<T> repository = new TransactionRepository<T>();

        public bool InsertJuros(T transaction)
        {
            try
            {
                var date = DateTime.Now.AddMonths(-1);
                var juros = repository.SelectBirthdayValue(date);

                if (juros > 0 && juros <= 12000.00)
                {
                    juros = (juros * 0.8) / 100;
                }
                else if (juros > 12000.00 && juros <= 21000.00)
                {
                    juros = (juros * 0.65) / 100;
                }
                else if (juros > 21000.00 && juros <= 36750.00)
                {
                    juros = (juros * 0.5) / 100;
                }
                else
                {
                    juros = (juros * 0.389) / 100;
                }

                if (juros < 0)
                {
                    transaction.Value = 0;
                }
                else
                {
                    transaction.Value = juros;
                }
                transaction.Description = "Juros";
                transaction.Created_at = DateTime.Now;

                repository.Insert(transaction);
                return true;
            }
            catch
            {
                return false;
            }

        }
        public double SelectBalance()
        {
            return repository.SelectBalance();
        }
    }
}
