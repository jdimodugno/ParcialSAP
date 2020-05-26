using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Data.Repositories
{
    public class WithdrawalRepository : BaseRepository<Withdrawal>
    {
        public List<Withdrawal> ReadByAccountNumber(string accountId)
        {
            List<Withdrawal> transfers = _entity
                .Where(a => a.TargetAccountId.ToString() == accountId)
                .ToList();

            return transfers;
        }
    }
}
