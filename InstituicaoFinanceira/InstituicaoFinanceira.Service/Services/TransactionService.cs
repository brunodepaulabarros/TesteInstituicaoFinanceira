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

        public T InsertJuros(T obj)
        {
            var Month = DateTime.Now.Month - 1;
            var Day = DateTime.Now.Day;
            var Year = DateTime.Now.Year;

            if (Month == 0)
                Month = 12;

            var _date = Year.ToString() + "-" + Month.ToString() + "-" + Day.ToString();
            var _dateIni = Convert.ToDateTime(_date + " 00:00:00");
            var _dateFim = Convert.ToDateTime(_date + " 23:59:59");


            obj.Value = repository.SelectValueBirthday(_dateIni, _dateFim);
            obj.Description = "Juros";
            obj.Created_at = DateTime.Now;

            repository.Insert(obj);
            return obj;
        }
    }
}
