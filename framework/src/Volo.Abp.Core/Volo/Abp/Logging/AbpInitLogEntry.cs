using System;
using Microsoft.Extensions.Logging;

namespace Volo.Abp.Logging;

public class AbpInitLogEntry
{
    public LogLevel LogLevel { get; set; }

    public EventId EventId { get; set; }

    public object State { get; set; } = default!;

    public Exception? Exception { get; set; }

    /// <summary>
    /// STUDYCODE:default! 告诉编译器此属性不可为空
    /// </summary>
    public Func<object, Exception?, string> Formatter { get; set; } = default!;

    public string Message => Formatter(State, Exception);
}
