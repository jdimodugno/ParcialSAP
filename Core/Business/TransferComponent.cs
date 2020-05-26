using System;
using System.Collections.Generic;
using Core.Services;
using Data.Repositories;
using Domain.Models;

namespace Core.Business
{
    public class TransferComponent : BaseComponent<Transfer>
    {
        public TransferComponent() : base(new TransferRepository(), (Transfer entity) =>
        {
            Tuple<bool, string> validation;
            BankAccount account = new BankAccountComponent().ReadByNumber(entity.SourceAccountId.ToString());
            if (OperationValidator.CanPerform(
                entity.Amount,
                account.Balance,
                account.Overdraft)
            ) validation = Tuple.Create<bool, string>(true, null);
            else validation = Tuple.Create(false, "Fondos Insuficientes");
            return validation;
        })
        { }

        public List<Transfer> ReadBySourceAccountNumber(string accountId)
        {
            return ((TransferRepository)_repository).ReadBySourceAccountNumber(accountId);
        }

        public List<Transfer> ReadByTargetAccountNumber(string accountId)
        {
            return ((TransferRepository)_repository).ReadByTargetAccountNumber(accountId);
        }
    }
}
