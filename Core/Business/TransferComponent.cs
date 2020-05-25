using System;
using Core.Services;
using Data.Repositories;
using Domain.Models;

namespace Core.Business
{
    public class TransferComponent : BaseComponent<Transfer>
    {
        public TransferComponent() : base(new TransferRepository()) { }

        public override Transfer Create(Transfer entity)
        {
            try
            {
                BankAccount account = new BankAccountComponent()
                    .ReadByNumber(entity.TargetAccountId.ToString());
                if (OperationValidator.CanPerform(entity.Amount, account.Balance, account.Overdraft))
                {
                    return _repository.Create(entity);
                }
                else
                {
                    throw new Exception("Unable to perform action - Insufficient Funds");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
