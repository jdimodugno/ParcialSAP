using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Data.Repositories
{
    public class DepositRepository : BaseRepository<Deposit>
    {
        public List<Deposit> ReadByAccountNumber(string accountId)
        {
            List<Deposit> deposits = _entity
                .Where(a => a.TargetAccountId.ToString() == accountId)
                .ToList();

            return deposits;
        }
    }
}
