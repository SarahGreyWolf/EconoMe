using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EconoMe.Common {
    interface IHolder {
        public Dictionary<ICurrency, float> Balances { get; }
        public Guid Guid { get; set; }

        /// <summary>
        /// Transfer the value of once currency in the holders balance to another holders balance
        /// </summary>
        /// <param name="other">Other IHolder</param>
        /// <param name="currency">ICurrency to trasnfer from</param>
        /// <param name="amount">Amount to transfer</param>
        /// <returns>Amount left in current IHolder's balance for the currency</returns>
        public float TransferTo(IHolder other, ICurrency currency, float amount) {
            // Short circuit, no reason to retrieve balances if nothing is changing hands
            if (amount == 0.0f)
                return 0.0f;
            float otherAmount = 0.0f;
            float holderAmount = 0.0f;
            bool retrievedOther = Balances.TryGetValue(currency, out otherAmount);
            if (!retrievedOther)
                return 0.0f;
            bool retrievedHolder = other.Balances.TryGetValue(currency, out holderAmount);
            if (!retrievedHolder)
                return 0.0f;
            otherAmount += amount;
            holderAmount -= amount;
            other.Balances[currency] = otherAmount;
            Balances[currency] = holderAmount;
            return holderAmount;
        }
    }
}
