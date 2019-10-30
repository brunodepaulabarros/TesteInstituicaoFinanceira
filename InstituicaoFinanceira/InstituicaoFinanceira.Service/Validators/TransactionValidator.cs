using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace InstituicaoFinanceira.Service.Validators
{
    public class TransactionValidator : AbstractValidator<Transaction>
    {
        public bool ValidatorTransaction()
        {
            RuleFor(t => t.Value).NotNull().OnAnyFailure(x =>
            {
                throw new ArgumentNullException("Can't found the object.");
            });
            return true;
        }
    }
}
