namespace CounterOnionLight.API.DTOs
{
    public class IncrementRequest
    {
        public string Who { get; set; } = "";
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();

    }
}
