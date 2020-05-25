using System;
using Data.Repositories;
using Domain.Models;

namespace Core.Business
{
    public class BankAccountComponent : BaseComponent<BankAccount>
    {
        public BankAccountComponent() : base(new BankAccountRepository())
        {

        }
    }
}
