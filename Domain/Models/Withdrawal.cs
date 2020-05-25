using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Models
{
    public class Withdrawal
    {
        public Withdrawal()
        {
            Id = Guid.NewGuid();
            DateCreated = DateTime.Now;
        }

        public Withdrawal(Guid id)
        {
            Id = id;
        }

        [Required]
        public Guid Id { get; }

        [Required]
        public double Amount { get; set; }

        [Required]
        public Guid? TargetAccountId { get; set; }

        public virtual BankAccount TargetAccount { get; }

        public DateTime DateCreated { get; }
    }
}
