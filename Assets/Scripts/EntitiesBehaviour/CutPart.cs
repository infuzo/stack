using System.Collections;

using UnityEngine;

using Stack.Models;

using Zenject;

namespace Stack.EntitiesBehaviour
{
    public class CutPart : MonoBehaviour
    {
        float timeInSecondsToDestroyCutPart;
        
        public void Configure(float timeInSecondsToDestroyCutPart)
        {
            this.timeInSecondsToDestroyCutPart = timeInSecondsToDestroyCutPart;
        }

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(timeInSecondsToDestroyCutPart);
            Destroy(gameObject);
        }
    }
}

