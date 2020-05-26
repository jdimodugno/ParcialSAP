using System;
using System.Collections.Generic;
using Core.Services;
using Data.Repositories;
using Domain.Models;

namespace Core.Business
{
    public class WithdrawalComponent : BaseComponent<Withdrawal>
    {
        public WithdrawalComponent()
            : base(new WithdrawalRepository(), (Withdrawal entity) =>
            {
                Tuple<bool, string> validation;
                BankAccount account = new BankAccountComponent().ReadByNumber(entity.TargetAccountId.ToString());
                if (OperationValidator.CanPerform(
                    entity.Amount,
                    account.Balance,
                    account.Overdraft)
                ) validation = Tuple.Create<bool, string>(true, null);
                else validation = Tuple.Create(false, "Fondos Insuficientes");
                return validation;
            })
        { }

        public List<Withdrawal> ReadByAccountNumber(string accountId)
        {
            return ((WithdrawalRepository)_repository).ReadByAccountNumber(accountId);
        }
    }
}
