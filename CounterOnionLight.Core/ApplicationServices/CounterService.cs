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
            CounterHistoryEntry historyEntry;
            if (counter == null)
            {

                historyEntry = new CounterHistoryEntry
                {
                    CounterId = counterId,
                    OldValue = 0,
                    NewValue = 1,
                    UpdatedBy = who,
                    CreatedUtc = DateTime.UtcNow
                };
            }
            else
            {
                var oldValue = counter.Value;

                counter.Value++;
                counter.LastUpdatedBy = who;

                historyEntry = new CounterHistoryEntry
                {
                    CounterId = counter.Id,
                    OldValue = oldValue,
                    NewValue = counter.Value,
                    UpdatedBy = who,
                    CreatedUtc = DateTime.UtcNow
                };
            }

            await _repository.SaveAsync(counter);

            await _repository.AddHistoryAsync(historyEntry);

            return counter.Value;
        }
    }
}
