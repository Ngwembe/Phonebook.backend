using System.Net.NetworkInformation;
using System.Threading;
using System.Threading.Tasks;
using Absa.Phonebook.Gateway.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace Absa.Phonebook.Gateway.Services
{
    public class UserServiceEndpointHealthCheck : IHealthCheck
    {
        private readonly ServiceSettings _settings;

        public UserServiceEndpointHealthCheck(IOptions<ServiceSettings> options)
        {
            _settings = options.Value;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            Ping ping = new();

            var reply = await ping.SendPingAsync($"{_settings.UserServiceHost}:{_settings.UserServicePort}");

            return reply.Status != IPStatus.Success ? HealthCheckResult.Unhealthy() : HealthCheckResult.Healthy();
        }
    }

}