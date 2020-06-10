using Stack.Signals;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Stack.Controllers
{
    public class InputController : MonoBehaviour
    {
        [Inject]
        protected SignalBus signalBus;

        //TODO: by ui
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                signalBus.Fire<PlatformPlacedSignal>();
            }
        }
    }

}
