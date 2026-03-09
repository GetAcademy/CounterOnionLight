using CounterOnionLight.Core.DomainModel;
using CounterOnionLight.Core.DomainServices;

namespace CounterOnionLight.Infrastructure
{
    internal class CounterEntityFrameworkDbFirstRepository : ICounterRepository
    {
        public Task<Counter?> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync(Counter counter)
        {
            throw new NotImplementedException();
        }

        public Task AddHistoryAsync(CounterHistoryEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
