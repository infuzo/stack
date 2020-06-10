using UnityEngine;

using Stack.EntitiesBehaviour;

namespace Stack.Services
{
    public interface ICutPartsFactory
    {
        CutPart CreateCutPart(
            Vector3 position,
            Vector3 scale);
    }
}
