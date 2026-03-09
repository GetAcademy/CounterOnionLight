using System;
using System.Collections.Generic;

namespace CounterOnionLight.Infrastructure.Infrastructure.Entities;

public partial class Counter
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Value { get; set; }

    public string LastUpdatedBy { get; set; } = null!;

    public byte[] RowVersion { get; set; } = null!;

    public virtual ICollection<CounterHistory> CounterHistories { get; set; } = new List<CounterHistory>();
}
