using System;
using Data.Repositories;
using Domain.Models;

namespace Core.Business
{
    public class DepositComponent : BaseComponent<Deposit>
    {
        public DepositComponent() : base(new DepositRepository()) { }
    }
}
