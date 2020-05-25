using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using Data.Context;
using Domain.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class BankAccountRepository : BaseRepository<BankAccount>
    {
        public override IEnumerable<BankAccount> Read(Expression<Func<BankAccount, bool>> expression)
        {
            List<BankAccount> accounts = _entity
                .Where(expression)
                .ToList();

            foreach (BankAccount account in accounts)
                account.Balance = getComputedBalance(account.Number.ToString());

            return _entity.Where(expression);
        }

        public BankAccount ReadByNumber(string accountId)
        {
            BankAccount account = _entity
                .Where(a => a.Number.ToString() == accountId)
                .FirstOrDefault();

            account.Balance = getComputedBalance(account.Number.ToString());

            return account;
        }

        private double getComputedBalance(string accountId)
        {
            double balance = 0;
            var currentAccountBalanceQuery = $@"
                select(
                  (select ISNULL(sum(d.amount), 0) from dbo.deposit d where targetaccountid = @accountId) - 
                  (select ISNULL(sum(w.amount), 0) from dbo.withdrawal w where targetaccountid = @accountId) - 
                  (select ISNULL(sum(t.amount), 0) from dbo.transfer t where sourceaccountid = @accountId) +
                  (select ISNULL(sum(t.amount), 0) from dbo.transfer t where targetaccountid = @accountId)
                );
            ";
            SqlConnection conn = new SqlConnection(BankDbContext.connectionString);
            SqlCommand command = new SqlCommand(currentAccountBalanceQuery, conn);
            command.Parameters.Add("@accountId", SqlDbType.VarChar);
            command.Parameters["@accountId"].Value = accountId;

            try
            {
                conn.Open();
                balance = (double)command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception("Error during balance calculation");
            }
            finally
            {
                conn.Close();
            }
            return balance;
        }
    }
}
