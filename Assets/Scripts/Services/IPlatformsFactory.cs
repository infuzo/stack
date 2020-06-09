using UnityEngine;
using Stack.EntitiesBehaviour;

namespace Stack.Services
{
    public interface IPlatformsFactory
    {
        Platform CreatePlatform(
                Vector3 position,
                Vector3 scale);
    }
}

