using System;
using System.Collections.Generic;
using System.Linq;
using Domain.Models;

namespace Data.Repositories
{
    public class TransferRepository : BaseRepository<Transfer>
    {
        public List<Transfer> ReadBySourceAccountNumber(string accountId)
        {
            List<Transfer> transfers = _entity
                .Where(a => a.SourceAccountId.ToString() == accountId)
                .ToList();

            return transfers;
        }

        public List<Transfer> ReadByTargetAccountNumber(string accountId)
        {
            List<Transfer> transfers = _entity
                .Where(a => a.TargetAccountId.ToString() == accountId)
                .ToList();

            return transfers;
        }
    }
}
