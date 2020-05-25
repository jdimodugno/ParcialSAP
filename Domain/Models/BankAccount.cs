using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Models
{
    public class BankAccount : IBankAccount
    {
        public BankAccount()
        {
            Number = Guid.NewGuid();
        }

        public BankAccount(int _type, Guid _number, double _overdraft = 0)
        {
            Type = _type;
            Number = _number;
            Overdraft = _type == 0 ? 0 : _overdraft;
        }

        [Required]
        public int Type { get; }

        [Required]
        public Guid Number { get; }

        [Required]
        public double Overdraft { get; set; }

        public DateTime DateCreated { get; set; }

        [NotMapped]
        public double Balance { get; set; }

        public ICollection<Deposit> Deposits { get; set; }
        public ICollection<Withdrawal> Withdrawals { get; set; }
        public ICollection<Transfer> TransfersAsSource { get; set; }
        public ICollection<Transfer> TransfersAsTarget { get; set; }

        public Deposit Deposit(double amount) => new Deposit { Amount = amount, TargetAccountId = Number };

        public Transfer Transfer(Guid targetAccountId, double amount) => new Transfer
        {
            Amount = amount,
            SourceAccountId = Number,
            TargetAccountId = targetAccountId
        };

        public Withdrawal Withdraw(double amount) => new Withdrawal { Amount = amount, TargetAccountId = Number };
    }
}
