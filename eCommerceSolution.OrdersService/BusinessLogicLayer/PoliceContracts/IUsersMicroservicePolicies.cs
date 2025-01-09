
using Polly;

namespace BusinessLogicLayer.PoliceContracts;

public interface IUsersMicroservicePolicies
{
    IAsyncPolicy<HttpResponseMessage> GetCombinedPolicy();
}
