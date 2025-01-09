
using BusinessLogicLayer.PoliceContracts;
using Microsoft.Extensions.Logging;
using Polly.Wrap;
using Polly;
using BusinessLogicLayer.Utils;

namespace BusinessLogicLayer.Policies;

public class UsersMicroservicePolicies : IUsersMicroservicePolicies
{
    private readonly ILogger<UsersMicroservicePolicies> _logger;
    private readonly IPollyPolicies _pollyPolicies;

    public UsersMicroservicePolicies(ILogger<UsersMicroservicePolicies> logger, IPollyPolicies pollyPolicies)
    {
        _logger = logger;
        _pollyPolicies = pollyPolicies;
    }

    public IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy()
    {
        var retryPolicy = _pollyPolicies.GetRetryPolicy(SD.FaultRetryCount);
        var circuitBreakerPolicy = _pollyPolicies.GetCircuitBreakerPolicy(SD.FaultCircuitEventsAllaowedBeforeBreak,
            TimeSpan.FromMinutes(SD.FaultCircuitDurationOfBreak));
        var timeoutPolicy = _pollyPolicies.GetTimeoutPolicy(TimeSpan.FromSeconds(5));

        AsyncPolicyWrap<HttpResponseMessage> wrappedPolicy = Policy.WrapAsync(retryPolicy, circuitBreakerPolicy, timeoutPolicy);
        return wrappedPolicy;
    }
}