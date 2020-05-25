using System;
namespace Core.Services
{
    public static class OperationValidator
    {
        public static bool CanPerform(double amount, double currentBalance, double overdraft)
        {
            return (currentBalance + overdraft - amount) >= 0;
        }
    }
}
