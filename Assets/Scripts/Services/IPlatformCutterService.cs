using Stack.EntitiesBehaviour;
using Stack.Models;

namespace Stack.Services
{
    public interface IPlatformCutterService
    {
        /// <summary>
        /// Returns null if target platform is out of base platform.
        /// </summary>
        PlatformCutterResultModel GetNewPlatformAndRemainsPart(
            Platform basePlatform,
            Platform targetPlatform);
    }
}

