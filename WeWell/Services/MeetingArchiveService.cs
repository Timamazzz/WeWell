using DataAccess;
using DataAccess.Enums;

public class MeetingArchiveService : IHostedService, IDisposable
{
    private readonly IServiceProvider _services;
    private readonly ILogger<MeetingArchiveService> _logger;
    private Timer? _timer;

    public MeetingArchiveService(IServiceProvider services, ILogger<MeetingArchiveService> logger)
    {
        _services = services;
        _logger = logger;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Meeting Archive Service is starting.");

        _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(5)); 

        return Task.CompletedTask;
    }

    private void DoWork(object state)
    {
        _logger.LogInformation("Meeting Archive Service is working.");

        using (var scope = _services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();

            var activeMeetings = dbContext.Meetings
                .Where(m =>
                        (!m.IsArchive.HasValue || (m.IsArchive.HasValue && !m.IsArchive.Value)) &&
                        (m.DateTimeEnd <= DateTime.UtcNow) &&                                
                        (m.Status != MeetingStatus.Cancelled.ToString())                     
                )
                .ToList();

            foreach (var meeting in activeMeetings)
            {
                meeting.IsArchive = true;
            }
            dbContext.SaveChanges();
        }
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Meeting Archive Service is stopping.");

        _timer?.Change(Timeout.Infinite, 0);

        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
