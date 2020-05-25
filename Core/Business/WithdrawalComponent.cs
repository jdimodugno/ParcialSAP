using System;
using Core.Services;
using Data.Repositories;
using Domain.Models;

namespace Core.Business
{
    public class WithdrawalComponent : BaseComponent<Withdrawal>
    {
        public WithdrawalComponent() : base(new WithdrawalRepository()) { }

        public override Withdrawal Create(Withdrawal entity)
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
