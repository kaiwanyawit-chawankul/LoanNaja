using System;

namespace LoanNaja.Core.Test
{
    public class LoanBuilder
    {
        string account = Guid.NewGuid().ToString();
        int amount = 100;
        DateTime? startDate = DateTime.Now;
        int durationInDays = 10;

        public Loan build()
        {
            return new Loan(account, amount, startDate, durationInDays);
        }

        public LoanBuilder withAmount(int amount)
        {
            this.amount = amount;
            return this;
        }

        public LoanBuilder withDurationInDays(int durationInDays)
        {
            this.durationInDays = durationInDays;
            return this;
        }

        public LoanBuilder withAccount(string account)
        {
            this.account = account;
            return this;
        }

        public LoanBuilder withTakenOn(DateTime? date)
        {
            this.startDate = date;
            return this;
        }
    }
}
