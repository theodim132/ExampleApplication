using Viva;
using Viva.Diagnostics;

namespace Example.App.Utility
{
    public class EventLogService : IEventLogService
    {
        public string CorrelationId => "Takis";

        public EventTypeId MinEventTypeId => EventTypeId.Warning;

        public IDisposable BeginScope(EventLogScope _)
            => new FakeDisposable();

        public IEventLogEntry Log(Event @event, Exception error, string message, object data)
            => null;

        public async Task<IEventLogEntry> LogAsync(Event @event, Exception error, string message, object data)
            => null;

        private class FakeDisposable : IDisposable
        {
            public void Dispose()
            {
                
            }
        }
    }
}
