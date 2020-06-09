using UnityEngine;
using Stack.Models;

namespace Stack.Services
{
    public interface IPlatformsFactory
    {
        Platform CreatePlatform(
                Vector3 position,
                Vector3 scale);
    }
}

