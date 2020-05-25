using System;
using Domain.Enums;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IBankAccount
    {
        int Type { get; }
        Guid Number { get; }
        double Balance { get; }
        Deposit Deposit(double amount);
        Withdrawal Withdraw(double amount);
        Transfer Transfer(Guid targetAccountId, double amount);
    }
}