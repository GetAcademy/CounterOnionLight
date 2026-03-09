using System;
using System.Collections.Generic;

namespace CounterOnionLight.Infrastructure.Infrastructure.Entities;

public partial class CounterHistory
{
    public int Id { get; set; }

    public int CounterId { get; set; }

    public int OldValue { get; set; }

    public int NewValue { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime CreatedUtc { get; set; }

    public virtual Counter Counter { get; set; } = null!;
}
