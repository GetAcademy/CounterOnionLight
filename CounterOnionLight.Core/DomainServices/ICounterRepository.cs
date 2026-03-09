using CounterOnionLight.Core.DomainModel;

namespace CounterOnionLight.Core.DomainServices
{
    public interface ICounterRepository
    {
        Task<Counter?> GetAsync(int id);

        Task SaveAsync(Counter counter);

        Task AddHistoryAsync(CounterHistoryEntry entry);
    }
}
