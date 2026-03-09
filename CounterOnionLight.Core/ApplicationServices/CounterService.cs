using CounterOnionLight.Core.DomainModel;
using CounterOnionLight.Core.DomainServices;

namespace CounterOnionLight.Core.ApplicationServices
{
    public class CounterService 
    {
        private readonly ICounterRepository _repository;

        public CounterService(ICounterRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> IncrementAsync(int counterId, string who)
        {
            var counter = await _repository.GetAsync(counterId);

            if (counter == null)
                throw new Exception("Counter not found");

            var oldValue = counter.Value;

            counter.Value++;
            counter.LastUpdatedBy = who;

            var historyEntry = new CounterHistoryEntry
            {
                CounterId = counter.Id,
                OldValue = oldValue,
                NewValue = counter.Value,
                UpdatedBy = who,
                CreatedUtc = DateTime.UtcNow
            };

            await _repository.SaveAsync(counter);

            await _repository.AddHistoryAsync(historyEntry);

            return counter.Value;
        }
    }
}
