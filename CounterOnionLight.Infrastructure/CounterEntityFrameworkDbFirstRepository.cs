using CounterOnionLight.Core.DomainModel;
using CounterOnionLight.Core.DomainServices;
using CounterOnionLight.Infrastructure.Infrastructure.Entities;
using Counter = CounterOnionLight.Core.DomainModel.Counter;

namespace CounterOnionLight.Infrastructure
{
    public class CounterEntityFrameworkDbFirstRepository : ICounterRepository
    {
        private readonly CounterDbContext _context;

        public CounterEntityFrameworkDbFirstRepository(CounterDbContext context)
        {
            _context = context;
        }

        public async Task<Counter?> GetAsync(int id)
        {
            var entity = await _context.Counters.FindAsync(id);

            if (entity == null)
                return null;

            return new Counter
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
                LastUpdatedBy = entity.LastUpdatedBy,
                RowVersion = entity.RowVersion
            };
        }

        public async Task SaveAsync(Counter counter)
        {
            var entity = await _context.Counters.FindAsync(counter.Id);
            entity.LastUpdatedBy = counter.LastUpdatedBy;
            entity.Value = counter.Value;
            await _context.SaveChangesAsync();
            /*
             * Alternativ uten optimistic concurrency:
             * 1: les rad fra db
             * 2: oppdater felter
             * 3: lagre tilbake
             */
            //var entity = new CounterOnionLight.Infrastructure.Infrastructure.Entities.Counter
            //{
            //    Id = counter.Id,
            //    Value = counter.Value,
            //    LastUpdatedBy = counter.LastUpdatedBy,
            //    RowVersion = counter.RowVersion
            //};

            //_context.Attach(entity);

            //_context.Entry(entity).Property(e => e.Value).IsModified = true;
            //_context.Entry(entity).Property(e => e.LastUpdatedBy).IsModified = true;

            //await _context.SaveChangesAsync();
        }

        public async Task AddHistoryAsync(CounterHistoryEntry entry)
        {
            _context.CounterHistories.Add(new CounterHistory
            {
                CounterId = entry.CounterId,
                OldValue = entry.OldValue,
                NewValue = entry.NewValue,
                UpdatedBy = entry.UpdatedBy,
                CreatedUtc = entry.CreatedUtc
            });

            await _context.SaveChangesAsync();
        }
    }
}
