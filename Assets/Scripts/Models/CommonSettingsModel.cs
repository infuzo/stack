using System;
using UnityEngine;

namespace Stack.Models
{
    [Serializable]
    public class CommonSettingsModel
    {
        public Vector3 FirstPlatformPosition;
        public Vector3 FirstPlatformScale;
        public float PlatformStartSpeed;
        public float MaxOffsetToSetAsNotOverlapped;
        public float TimeInSecondsToDestroyCutPart;
    }
}

