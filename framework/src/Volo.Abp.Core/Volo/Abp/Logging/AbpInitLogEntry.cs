using System;
using Microsoft.Extensions.Logging;

namespace Volo.Abp.Logging;
/// <summary>
/// 学到这里
/// </summary>

public class AbpInitLogEntry
{
    public LogLevel LogLevel { get; set; }

    public EventId EventId { get; set; }

    public object State { get; set; } = default!;

    public Exception? Exception { get; set; }

    public Func<object, Exception?, string> Formatter { get; set; } = default!;

    public string Message => Formatter(State, Exception);
}
