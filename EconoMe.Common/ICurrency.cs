namespace EconoMe.Common {
    interface ICurrency {
        // The name of the higher value of currency, such as Pound
        public string HigherName { get; }
        // The name of the lower value of currency, such as Pence
        public string LowerName { get; }
        // The highest that the lower value can reach before becoming a higher value
        public int MaxLowest { get; }
        public float ExchangeRate(ICurrency other);
    }
}
