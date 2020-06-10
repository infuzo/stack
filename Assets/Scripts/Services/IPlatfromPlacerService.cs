using Stack.EntitiesBehaviour;
using UnityEngine;

namespace Stack.Services
{
    public interface IPlatfromPlacerService
    {
        void GetRandomStartPoint(
            Platform basePlatform,
            out Vector3 position,
            out Vector3 direction,
            out float maxDistance);
    }

}
