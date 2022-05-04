using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoMe.Common.Bank {
    interface IAccount {
        public Dictionary<ICurrency, float> Balances { get; }
        public IHolder Holder { get; set; }

        /// <summary>
        /// Withdraw from Accounts balance
        /// </summary>
        /// <param name="currency">ICurrency to withdraw from</param>
        /// <param name="amount">Amount to withdraw</param>
        /// <returns>Amount left in Account for currency</returns>
        public float Withdraw(ICurrency currency, float amount) {
            if (amount == 0.0f)
                return 0.0f;
            float accountAmount = 0.0f;
            float holderAmount = 0.0f;
            bool retrieved = Balances.TryGetValue(currency, out accountAmount);
            if (!retrieved || accountAmount < amount)
                return 0.0f;
            bool holderRetrieved = Holder.Balances.TryGetValue(currency, out holderAmount);
            if (!holderRetrieved)
                return 0.0f;
            accountAmount -= amount;
            holderAmount += amount;
            Balances[currency] = accountAmount;
            Holder.Balances[currency] = holderAmount;
            return accountAmount;
        }

        /// <summary>
        /// Deposit into Accounts balance
        /// </summary>
        /// <param name="currency">ICurrency to deposit into</param>
        /// <param name="amount">Amount to deposit</param>
        /// <returns>Amount in Account for currency</returns>
        public float Deposit(ICurrency currency, float amount) {
            if (amount == 0.0f)
                return 0.0f;
            float accountAmount = 0.0f;
            float holderAmount = 0.0f;
            bool retrieved = Balances.TryGetValue(currency, out accountAmount);
            if (!retrieved)
                return 0.0f;
            bool holderRetrieved = Holder.Balances.TryGetValue(currency, out holderAmount);
            if (!holderRetrieved || holderAmount < amount)
                return 0.0f;
            accountAmount += amount;
            holderAmount -= amount;
            Balances[currency] = accountAmount;
            Holder.Balances[currency] = holderAmount;
            return accountAmount;
        }
    }
}
