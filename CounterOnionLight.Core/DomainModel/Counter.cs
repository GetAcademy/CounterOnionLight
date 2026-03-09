namespace CounterOnionLight.Core.DomainModel
{
    public class Counter
    {
        public int Id { get; set; }

        public string Name { get; set; } = "";

        public int Value { get; set; }

        public string LastUpdatedBy { get; set; } = "";

        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
