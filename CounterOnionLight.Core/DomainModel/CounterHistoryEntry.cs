namespace CounterOnionLight.Core.DomainModel
{
    public class CounterHistoryEntry
    {
        public int CounterId { get; set; }

        public int OldValue { get; set; }

        public int NewValue { get; set; }

        public string UpdatedBy { get; set; } = "";

        public DateTime CreatedUtc { get; set; }
    }
}
