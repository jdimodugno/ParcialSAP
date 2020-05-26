using System;
using System.Collections.Generic;
using Data.Repositories;
using Domain.Models;

namespace Core.Business
{
    public class DepositComponent : BaseComponent<Deposit>
    {
        public DepositComponent() : base(new DepositRepository()) { }

        public List<Deposit> ReadByAccountNumber(string accountId)
        {
            return ((DepositRepository)_repository).ReadByAccountNumber(accountId);
        }
    }
}
