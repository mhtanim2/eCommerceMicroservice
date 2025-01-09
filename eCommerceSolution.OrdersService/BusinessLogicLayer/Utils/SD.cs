
namespace BusinessLogicLayer.Utils;

public static class SD
{
    public static int FaultRetryCount = 5;
    public static int FaultCircuitEventsAllaowedBeforeBreak = 3;
    public static int FaultCircuitDurationOfBreak = 2;
}
